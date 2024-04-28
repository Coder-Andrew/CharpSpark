using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

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
            // Load the template and resume HTML into HtmlDocument objects
            HtmlDocument templateDoc = new();
            templateDoc.LoadHtml(System.Net.WebUtility.UrlDecode(template.HtmlContent));
            HtmlDocument resumeDoc = new();
            resumeDoc.LoadHtml(System.Net.WebUtility.UrlDecode(resume.HtmlContent));

            // Replace the template with the user's information
            ReplaceUserInfo(templateDoc, resumeDoc, currUser);

            // Check if the resume contains the sections for the template
            var sections = new[] { "Summary", "Experience", "Employment History", "Education", "Achievements", "Skills", "Projects", "Reference", "Profile"};

            // Create a dictionary to store the sections and their content
            Dictionary<string, List<HtmlNode>> sectionContent = new Dictionary<string, List<HtmlNode>>();

            // Check if the first node is a section title
            HtmlNode firstNode = resumeDoc.DocumentNode.FirstChild;
            HtmlNode currentNode = firstNode.NextSibling;

            string currUserSummary = string.Empty;
            bool firstNodeIsSectionTitle = ExtractCurrUserSummary(resumeDoc, sections, firstNode);
            
            if (!firstNodeIsSectionTitle)
            {
                List<HtmlNode> summaryNodes = ExtractSummaryNodes(firstNode, sections);
                currUserSummary = string.Join(" ", summaryNodes.Select(node => node.InnerText));
            }
            
            string[] sectionsWithDegree = sections.Concat(new[] { "degree" }).ToArray();
            sectionContent = ExtractSectionContent(resumeDoc, sectionsWithDegree, out currUserSummary);

            if(currUserSummary.IsNullOrEmpty())
            {
                currUserSummary = currUser.Summary ?? string.Empty;
            }

            // Templates 3 and 5 have a different format for the "Summary" section
            if (template.ResumeId == 3 || template.ResumeId == 5)
            {
                string pattern = @"Lorem.*?\.\.\.";

                Regex regex = new Regex(pattern);
                Match match = regex.Match(templateDoc.DocumentNode.OuterHtml);

                if (match.Success)
                {
                    string personalSummary = match.Value;
                    var nodes = templateDoc.DocumentNode.DescendantsAndSelf().Where(n => n.OuterHtml.Contains(personalSummary));
                    foreach (var node in nodes)
                    {
                        node.InnerHtml = node.InnerHtml.Replace(personalSummary, currUserSummary);
                        break;
                    }
                }
            }

            foreach (string section in sections)
            {
                string lowerSection = section.ToLower();

                HtmlNode templateSection = templateDoc.DocumentNode.SelectSingleNode($"//descendant::*[contains(translate(., 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '{lowerSection}')]");

                if (templateSection != null)
                {
                    if (!sectionContent.ContainsKey(section))
                    {
                        if (!string.IsNullOrEmpty(currUserSummary) && (section.ToLower() == "summary" || section.ToLower() == "profile"))
                        {
                            ReplaceSectionContentWithString(templateSection, currUserSummary, sections);
                            
                        }
                        else
                        {
                            RemoveSectionAndContent(templateSection, sections, section);
                        }
                    }
                    else
                    {
                        if(section.ToLower() == "skills")
                        {
                            ReplaceSkills(templateSection, sectionContent, section, template.ResumeId, sections);
                        }
                        else if (section.ToLower() == "projects")
                        {
                            HtmlNode titleNode = templateDoc.DocumentNode.SelectSingleNode($"//descendant::*[contains(translate(., 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'project title')]");

                            if (sectionContent.ContainsKey(section) && sectionContent[section].Count > 0)
                            {
                                ReplaceProjects(sectionContent, section, titleNode);
                            }
                            else
                            {
                                ReplaceSectionContent(templateSection, sectionContent[section], sections, section);
                            }

                            string modifiedTemplate = templateSection.OwnerDocument.DocumentNode.OuterHtml;
                        }
                        else if (section.ToLower() == "achievements")
                        {
                            if (!ReplaceAchievements(templateSection, sectionContent, section, template.ResumeId))
                            {
                                ReplaceSectionContent(templateSection, sectionContent[section], sections, section);
                            }
                        }
                        else
                        {
                            ReplaceSectionContent(templateSection, sectionContent[section], sections, section);
                        }
                    }
                }
            }

            template.HtmlContent = templateDoc.DocumentNode.OuterHtml;
            template.HtmlContent += resumeDoc.DocumentNode.OuterHtml;

            return template;
        }
        
        private bool ExtractCurrUserSummary(HtmlDocument resumeDoc, string[] sections, HtmlNode firstNode)
        {
            bool firstNodeIsSectionTitle = true;

            while(firstNode != null)
            {
                if(firstNode.Name == "hr" || firstNode.Name == "p")
                {
                    firstNodeIsSectionTitle = false;
                    break;
                }
                if(sections.Any(s => firstNode.InnerText.ToLower().Contains(s.ToLower())))
                {
                    break;
                }
                firstNode = firstNode.NextSibling;
            }
            
            return firstNodeIsSectionTitle;
        }
        private List<HtmlNode> ExtractSummaryNodes(HtmlNode firstNode, string[] sections)
        {
            List<HtmlNode> summaryNodes = new List<HtmlNode>();

            HtmlNode currentSummaryNode = firstNode;

            if (firstNode.Name == "hr")
            {
                currentSummaryNode = firstNode.NextSibling;
                firstNode.Remove();
            }

            while (currentSummaryNode != null)
            {
                string trim = currentSummaryNode.InnerText.Trim();
                if (currentSummaryNode.Name == "hr" || sections.Any(s => currentSummaryNode.InnerText.Trim().Equals(s, StringComparison.OrdinalIgnoreCase)) || trim == "Work Experience")
                {
                    break;
                }

                summaryNodes.Add(currentSummaryNode);

                HtmlNode nodeToRemove = currentSummaryNode;

                currentSummaryNode = currentSummaryNode.NextSibling;

                nodeToRemove.Remove();
            }

            return summaryNodes;
        }

        private Dictionary<string, List<HtmlNode>> ExtractSectionContent(HtmlDocument resumeDoc, string[] sections, out string currUserSummary)
        {
            Dictionary<string, List<HtmlNode>> sectionContent = new Dictionary<string, List<HtmlNode>>();
            currUserSummary = string.Empty;

            foreach (string section in sections)
            {
                string lowerSection = section.ToLower();

                HtmlNode resumeSection = resumeDoc.DocumentNode.SelectSingleNode($"//descendant::*[contains(translate(., 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '{lowerSection}')]");

                if (resumeSection != null)
                {
                    List<HtmlNode> contentNodes = new List<HtmlNode>();

                    HtmlNode currentNode = resumeSection.NextSibling;

                    while (currentNode != null)
                    {
                        if (currentNode.Name == "hr")
                        {
                            currentNode = currentNode.NextSibling;
                            continue;
                        }

                        string trim = currentNode.InnerText.Trim();
                        if (sections.Any(s => currentNode.InnerText.Trim().Equals(s, StringComparison.OrdinalIgnoreCase)) || trim == "Work Experience")
                        {
                            break;
                        }

                        contentNodes.Add(currentNode);

                        HtmlNode nextNode = currentNode.NextSibling;

                        currentNode.Remove();

                        currentNode = nextNode;
                    }

                    sectionContent[section] = contentNodes;
                    

                    if (section.ToLower() == "summary" || section.ToLower() == "profile")
                    {
                        currUserSummary = string.Join(" ", contentNodes.Select(node => node.InnerText));
                    }

                    resumeSection.Remove();
                }
            }

            if (sectionContent.ContainsKey("degree") && sectionContent.ContainsKey("Education"))
            {
                sectionContent["Education"].AddRange(sectionContent["degree"]);
                sectionContent.Remove("degree");
            }

            if(sectionContent.ContainsKey("Employment History"))
            {
                sectionContent["Experience"] = sectionContent["Employment History"];
                sectionContent.Remove("Employment History");
            }

            return sectionContent;
        }
        private void ReplaceUserInfo(HtmlDocument templateDoc, HtmlDocument resumeDoc, UserInfo currUser)
        {
            var replacements = new Dictionary<string, string>
            {
                { "Jasmine Patel", $"{currUser.FirstName} {currUser.LastName}" },
                { "JASMINE", currUser.FirstName.ToUpper() },
                { "PATEL", currUser.LastName.ToUpper() },
                { "555-794-4847", currUser.PhoneNumber },
                { "patelj@mail.com", currUser.Email }
            };

            foreach (var replacement in replacements)
            {
                ReplaceTextInDocument(templateDoc, resumeDoc, replacement.Key, replacement.Value);
            }
        }

        private void ReplaceSkills(HtmlNode templateSection, Dictionary<string, List<HtmlNode>> sectionContent, string section, int? templateId, string[] sections)
        {
            var nodeContent = sectionContent[section][0].InnerHtml;

            if (!nodeContent.Trim().StartsWith("<ul>") || !nodeContent.Trim().EndsWith("</ul>"))
            {
                if (templateId == 2)
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(nodeContent);

                    var skills = htmlDoc.DocumentNode.SelectNodes("//li")
                        .Select(node => node.InnerText.Trim())
                        .ToArray();

                    var formattedSkills = skills
                        .Select(skill => $"<strong><u>{skill}</u></strong>&nbsp;")
                        .Aggregate((a, b) => a + b);

                    formattedSkills = formattedSkills.TrimEnd(new[] { '&', 'n', 'b', 's', 'p', ';' });

                    var newNode = HtmlNode.CreateNode($"<div>{formattedSkills}</div>");

                    sectionContent[section][0] = newNode;
                }
                else
                {
                    var skills = nodeContent.Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    var skillsList = skills
                        .Select(skill => $"<li>{skill.Trim()}</li>\n")
                        .Aggregate((a, b) => a + b);

                    skillsList = "<ul>\n" + skillsList + "</ul>";

                    sectionContent[section][0].InnerHtml = skillsList;
                }

                ReplaceSectionContent(templateSection, sectionContent[section], sections, section);
            }
            else
            {
                ReplaceSectionContent(templateSection, sectionContent[section], sections, section);
            }
        }

        private void ReplaceProjects(Dictionary<string, List<HtmlNode>> sectionContent, string section, HtmlNode titleNode)
        {
            var projectTitle = sectionContent[section][0].InnerHtml;
            var projectContent = sectionContent[section].Skip(1).Select(node => node.InnerHtml);
            var contentNode = titleNode.NextSibling;

            if (contentNode != null)
            {
                contentNode.RemoveAllChildren();

                foreach (var content in projectContent)
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml("<li>" + content + "</li>");

                    foreach (var node in doc.DocumentNode.ChildNodes)
                    {
                        contentNode.AppendChild(node);
                    }
                }
            }
            
            if (titleNode != null)
            {
                var textNode = titleNode.DescendantsAndSelf()
                    .FirstOrDefault(n => n.NodeType == HtmlNodeType.Text && n.InnerText.Trim().ToLower() == "project title");

                if (textNode != null)
                {
                    if (textNode.InnerText.Trim() == textNode.InnerText.Trim().ToUpper())
                    {
                        textNode.InnerHtml = projectTitle.ToUpper();
                    }
                    else
                    {
                        textNode.InnerHtml = projectTitle;
                    }
                }
            }
        }

        private bool ReplaceAchievements(HtmlNode templateSection, Dictionary<string, List<HtmlNode>> sectionContent, string section, int? templateId)
        {
            var nextNode = templateSection.NextSibling;
            nextNode = nextNode.NextSibling;
            var achievements = nextNode.SelectNodes(".//li");
            List<string> awardTitles = new List<string>();
            List<string> awardDescriptions = new List<string>();
            
            if (!sectionContent[section][0].InnerHtml.Contains("<li>"))
            {
                return false;
            }

            foreach(var content in sectionContent[section])
            {
                if(content.InnerHtml.Contains("<li>"))
                {
                    var contentParts = content.InnerText.Split(" - ");
                    if (contentParts.Length == 2)
                    {
                        awardTitles.Add(contentParts[0]);
                        awardDescriptions.Add(contentParts[1]);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            var lastNode = templateSection.NextSibling;

            if (achievements != null)
            {
                int count = Math.Min(awardTitles.Count, awardDescriptions.Count);

                for (int i = 0; i < count; i++)
                {
                    if (templateId == 4)
                    {
                        var currentNode = templateSection.NextSibling;
                        // For the fourth template, the title and description are in separate tags
                        var titleNode = currentNode.SelectSingleNode($"//span[@class='award-title']");
                        
                        if (titleNode != null)
                        {
                            titleNode.InnerHtml = awardTitles[i].ToUpper();
                        }

                        var descriptionNode = achievements[i].SelectSingleNode(".//span[@class='award-description']");
                        if (descriptionNode != null)
                        {
                            descriptionNode.InnerHtml = awardDescriptions[i];
                        }
                        if(i == count - 1)
                        {
                            lastNode = currentNode.NextSibling.NextSibling.NextSibling;
                        }
                    }
                    else
                    {
                        var achievement = achievements[i];

                        var titleNode = achievement.SelectSingleNode(".//span[@class='award-title']");
                        if (titleNode != null)
                        {
                            titleNode.InnerHtml = awardTitles[i];
                        }

                        var descriptionNode = achievement.SelectSingleNode(".//span[@class='award-description']");
                        if (descriptionNode != null)
                        {
                            descriptionNode.InnerHtml = awardDescriptions[i];
                        }
                    }
                }

                if(templateId == 4)
                {
                    var nextSiblingNode = lastNode.NextSibling;
                    for(int i = 0; i < 5; i++)
                    {
                        lastNode.Remove();
                        lastNode = nextSiblingNode;
                        nextSiblingNode = nextSiblingNode.NextSibling;
                    }
                }
                else
                {
                    for (int i = count; i < achievements.Count; i++)
                    {
                        achievements[i].Remove();
                    }
                }
            }
            return true;
        }

        private void RemoveSectionAndContent(HtmlNode section, string[] sections, string sectionName)
        {
            HtmlNode prevHr = section.PreviousSibling;
            if (prevHr != null && prevHr.Name == "hr")
            {
                prevHr.Remove();
            }

            HtmlNode nextHr = section.NextSibling;
            if (nextHr != null && nextHr.Name == "hr")
            {
                nextHr.Remove();
            }
            string trim = section.NextSibling.InnerText.Trim();
            while (section.NextSibling != null && !(section.NextSibling.Name == "hr" || (sections.Any(s => section.NextSibling.InnerText.Trim().Equals(s, StringComparison.OrdinalIgnoreCase)) || trim == "Work Experience")))
            {
                section.NextSibling.Remove();
            }

            if(section.InnerText.ToLower().Contains("reference"))
            {
                while (section.NextSibling != null)
                {
                    section.NextSibling.Remove();
                }
            }

            if (section.NextSibling != null && section.NextSibling.Name == "hr" && (section.NextSibling.NextSibling == null || !sections.Any(s => section.NextSibling.NextSibling.InnerText.ToLower().Contains(s.ToLower()))))
            {
                section.NextSibling.Remove();
            }

            section.Remove();
            
        }

        private void ReplaceSectionContent(HtmlNode section, List<HtmlNode> contentNodes, string[] sections, string sectionName)
        {
            if(sectionName.ToLower() == "summary" || sectionName.ToLower() == "profile" || sectionName.ToLower() == "skills")
            {
                HtmlNode firstSibling = section.NextSibling;
                if(firstSibling.Name == "hr")
                {
                    section = firstSibling;
                }

                while (section.NextSibling != null && !(section.NextSibling.Name == "hr" || sections.Any(s => section.NextSibling.InnerText.ToLower().Contains(s.ToLower()))))
                {
                    section.NextSibling.Remove();
                }

                HtmlNode insertAfterNode = section;
                while (insertAfterNode.NextSibling != null && !(insertAfterNode.NextSibling.Name == "hr" || sections.Any(s => insertAfterNode.NextSibling.InnerText.ToLower().Contains(s.ToLower()))))
                {
                    if (insertAfterNode.NextSibling.Name != "hr")
                    {
                        insertAfterNode = insertAfterNode.NextSibling;
                    }
                    else
                    {
                        break;
                    }
                }

                foreach (HtmlNode contentNode in contentNodes)
                {
                    section.ParentNode.InsertAfter(contentNode, insertAfterNode);
                    insertAfterNode = contentNode;
                }

                HtmlNode brTag = HtmlNode.CreateNode("<br />");

                section.ParentNode.InsertAfter(brTag, insertAfterNode);
                
            }
            else
            {
                HtmlNode insertAfterNode = section;

                if (insertAfterNode.NextSibling != null && insertAfterNode.NextSibling.Name == "hr")
                {
                    insertAfterNode = insertAfterNode.NextSibling;
                }

                while (insertAfterNode.NextSibling != null && !(insertAfterNode.NextSibling.Name == "hr" || sections.Any(s => insertAfterNode.NextSibling.InnerText.ToLower().Contains(s.ToLower()))))
                {
                    if (insertAfterNode.NextSibling.Name != "hr")
                    {
                        insertAfterNode = insertAfterNode.NextSibling;
                    }
                    else
                    {
                        break;
                    }
                }

                // Create a <span> node with a style that sets the color and opacity
                HtmlNode spanNode = HtmlNode.CreateNode("<span style='color: rgba(169, 169, 169, 0.7);'></span>");

                /// Create a new HtmlNode with the clarifying text and formatting
                HtmlNode boldNode = HtmlNode.CreateNode("<b>This is the old content. Please reformat it to match the above.</b>");

                // Create a <br> node
                HtmlNode brNode = HtmlNode.CreateNode("<br/>");

                // Append the bold node and the <br> node to the span node
                spanNode.AppendChild(boldNode);
                spanNode.AppendChild(brNode);

                // Append the new content to the span node
                foreach (HtmlNode contentNode in contentNodes)
                {
                    spanNode.AppendChild(contentNode);
                }

                if (sectionName.ToLower() == "reference")
                {
                    HtmlNode lastSibling = section;
                    while (lastSibling.NextSibling != null)
                    {
                        lastSibling = lastSibling.NextSibling;
                    }
                    section.ParentNode.InsertAfter(spanNode, lastSibling);
                }
                else
                {
                    section.ParentNode.InsertAfter(spanNode, insertAfterNode);
                }

            }
        }

        private void ReplaceSectionContentWithString(HtmlNode section, string currUserSummary, string[] sections)
        {
            HtmlNode firstSibling = section.NextSibling;

            if(firstSibling.Name == "hr")
            {
                section = firstSibling;
            }

            while (section.NextSibling != null && !(section.NextSibling.Name == "hr" || sections.Any(s => section.NextSibling.InnerText.ToLower().Contains(s.ToLower()))))
            {
                section.NextSibling.Remove();
            }

            HtmlNode insertAfterNode = section;
            if (section.NextSibling != null && section.NextSibling.Name == "hr")
            {
                insertAfterNode = section.NextSibling;
            }

            
            HtmlNode newNode = HtmlNode.CreateNode(currUserSummary);
            section.ParentNode.InsertAfter(newNode, insertAfterNode);
            insertAfterNode = newNode;

            HtmlNode brTag1 = HtmlNode.CreateNode("<br />");
            section.ParentNode.InsertAfter(brTag1, insertAfterNode);

            HtmlNode brTag2 = HtmlNode.CreateNode("<br />");
            section.ParentNode.InsertAfter(brTag2, brTag1); 
        }

        private void ReplaceTextInDocument(HtmlDocument templateDoc, HtmlDocument resumeDoc, string oldText, string newText)
        {
            foreach (HtmlNode node in templateDoc.DocumentNode.DescendantsAndSelf())
            {
                if (node.NodeType == HtmlNodeType.Text)
                {
                    node.InnerHtml = node.InnerHtml.Replace(oldText, newText);
                }
            }

            foreach (HtmlNode node in resumeDoc.DocumentNode.DescendantsAndSelf())
            {
                if (node.NodeType == HtmlNodeType.Text)
                {
                    if (newText == null)
                    {
                        newText = " ";
                    }
                    node.InnerHtml = node.InnerHtml.Replace(newText, "");
                }
            }
        }
    }
}