using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;
using System.Text.RegularExpressions;

namespace ResuMeta.DAL.Concrete
{
    public class ResumeTemplateRepository : Repository<ResumeTemplate>, IResumeTemplateRepository
    {
        private readonly DbSet<ResumeTemplate> _resumeTemplates;
        public ResumeTemplateRepository(ResuMetaDbContext context) : base(context)
        {
            _resumeTemplates = context.ResumeTemplates;
        }

        public ResumeVM GetResumeTemplateHtml(int templateId)
        {
            ResumeTemplate? resumeTemplate = _resumeTemplates.FirstOrDefault(r => r.Id == templateId);
            if(resumeTemplate == null)
            {
                throw new Exception("Resume Template not found");
            }

            ResumeVM resumeVM = new ResumeVM
            {
                ResumeId = resumeTemplate.Id,
                Title = resumeTemplate.Title,
                HtmlContent = resumeTemplate.Template1
            };
            return resumeVM;
        }

        public List<ResumeVM> GetAllResumeTemplates()
        {
            var resumeTemplateList = _resumeTemplates
                .Where(x => x.Template1 != null)
                .Select(x => new ResumeVM
                {
                    ResumeId = x.Id,
                    Title = x.Title,
                    HtmlContent = x.Template1
                })
                .ToList();

            return resumeTemplateList;
        }

        public ResumeVM ConvertResumeToTemplate(ResumeVM template, ResumeVM resume, UserInfo currUser)
        {
            // Decode the content
            string decodedTemplateContent = System.Net.WebUtility.UrlDecode(template.HtmlContent);
            string decodedResumeContent = System.Net.WebUtility.UrlDecode(resume.HtmlContent);

            // Replace the template with the user's information
            decodedTemplateContent = decodedTemplateContent.Replace("Jasmine Patel", currUser.FirstName + " " + currUser.LastName);
            decodedTemplateContent = decodedTemplateContent.Replace("JASMINE", currUser.FirstName.ToUpper());
            decodedTemplateContent = decodedTemplateContent.Replace("PATEL", currUser.LastName.ToUpper());
            decodedTemplateContent = decodedTemplateContent.Replace("555-794-4847", currUser.PhoneNumber);
            decodedTemplateContent = decodedTemplateContent.Replace("patelj@mail.com", currUser.Email);

            // Check if the resume contains the sections for the template
            string[] sections = { "Summary", "Work Experience", "Experience", "Education", "Achievements", "Skills", "Projects", "Reference", "Profile"};
            string joinedSections = string.Join("|", sections.Select(Regex.Escape));
            foreach (string section in sections)
            {
                // Check if the section is present in the resume
                if (!Regex.IsMatch(decodedResumeContent, section, RegexOptions.IgnoreCase))
                {
                    // If the section is not present in the resume, check if it is present in the template
                    if (Regex.IsMatch(decodedTemplateContent, section, RegexOptions.IgnoreCase))
                    {
                        // If the section is present in the template, remove it
                        string pattern = $@"{section}[\s\S]*?(?={joinedSections}|$)";
                        decodedTemplateContent = Regex.Replace(decodedTemplateContent, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    }
                }
                else if (Regex.IsMatch(decodedTemplateContent, section, RegexOptions.IgnoreCase) && section == "Skills")
                {
                    // Check if the resume section for skills is a list
                    string skillsPattern = @"<ul>([\s\S]*?)</ul>|<ol>([\s\S]*?)</ol>";
                    if (Regex.IsMatch(decodedResumeContent, skillsPattern, RegexOptions.IgnoreCase))
                    {
                        // If the section is present in the resume and the template, replace the template section with the resume section
                        string pattern = $@"{section}[\s\S]*?(?={joinedSections}|$)";
                        string resumeSection = Regex.Match(decodedResumeContent, pattern, RegexOptions.IgnoreCase).Value;

                        // Extract the content within the list tags
                        string resumeListContent = Regex.Match(resumeSection, skillsPattern, RegexOptions.IgnoreCase).Groups[1].Value;

                        // if (Regex.IsMatch(decodedTemplateContent, @"<ul>|<ol>"))
                        if(false)
                        {
                            // Check if the template uses ul or ol for the list
                            string templateListTag = Regex.IsMatch(decodedTemplateContent, @"<ul>") ? "ul" : "ol";

                            // Replace the content within the template's list tags with the resume's list content
                            string templatePattern = $@"<{templateListTag}>([\s\S]*?)</{templateListTag}>";
                            decodedTemplateContent = Regex.Replace(decodedTemplateContent, templatePattern, $"<{templateListTag}>{resumeListContent}</{templateListTag}>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        }
                        else
                        {
                            // Template 2: each skill is contained in a <u> tag with the words 'Lorem Ipsum'
                            // Extract the list items from the resume section
                            List<string> resumeListItems = new List<string> { "Meow", "Woof", "nya"};

                            // Replace each 'Lorem Ipsum' with each line from the list in the resume section
                            var templateSkills = Regex.Matches(decodedTemplateContent, @"<u>Lorem Ipsum</u>", RegexOptions.IgnoreCase)
                                .Cast<Match>()
                                .ToList();

                            for (int i = 0; i < templateSkills.Count; i++)
                            {
                                if (i < resumeListItems.Count)
                                {
                                    // Replace 'Lorem Ipsum' with the corresponding resume list item
                                   decodedTemplateContent = Regex.Replace(decodedTemplateContent, templateSkills[i].Value, $"<u>{resumeListItems[i]}</u>", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(1));
                                }
                                else
                                {
                                    // Remove any extra 'Lorem Ipsum'
                                    decodedTemplateContent = decodedTemplateContent.Replace(templateSkills[i].Value, "");
                                }
                            }
                        }
                    }
                }
            }

            string summaryContent = "";

            // Find the personal summary on the resume and save it to a string
            string summaryPattern = $@"((Personal Summary|Summary|Profile|{Regex.Escape(currUser.Email)}|{Regex.Escape(currUser.PhoneNumber)}|{Regex.Escape(currUser.FirstName + " " + currUser.LastName)}|{Regex.Escape(currUser.FirstName)}|{Regex.Escape(currUser.LastName)}|{Regex.Escape(currUser.FirstName.ToUpper())}|{Regex.Escape(currUser.LastName.ToUpper())}))";
            if (Regex.IsMatch(decodedTemplateContent, summaryPattern, RegexOptions.IgnoreCase))
            {
                // If the pattern is found, save the summary content
                summaryContent = Regex.Match(decodedTemplateContent, summaryPattern, RegexOptions.IgnoreCase).Groups[2].Value;
            }

            // Check if the template contains a line starting with 'Lorem' and ending with '...' 
            // and is preceded by either the two lines '555-628-1234' and '<hr />', or the line currUser.FirstName + " " + currUser.LastName
            // This is template3 and template5's summary locations
            string template3Summary = $@"({Regex.Escape(currUser.FirstName + " " + currUser.LastName)}</strong></h1>\s*<p>)(Lorem[\s\S]*?\.\.\.)";
            string template5Summary = $@"(<strong>{Regex.Escape(currUser.PhoneNumber)}</strong></h3><hr>\s*<p>)(Lorem[\s\S]*?\.\.\.)";
            string loremPattern = $@"{template3Summary}|{template5Summary}";

            if (Regex.IsMatch(decodedTemplateContent, loremPattern))
            {
                // If the pattern is found, replace only the content starting at 'Lorem' with 'meow'
                decodedTemplateContent = Regex.Replace(decodedTemplateContent, loremPattern, m => m.Groups[1].Value + (m.Groups[2].Success ? summaryContent : m.Groups[3].Value + summaryContent));
            }

            //Attach any extra content from the resume to the end of the template

            template.HtmlContent = decodedTemplateContent;

            return template;
        }
    }
}