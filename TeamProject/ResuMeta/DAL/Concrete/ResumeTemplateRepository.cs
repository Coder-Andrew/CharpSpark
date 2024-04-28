using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.IdentityModel.Tokens;

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
            HtmlDocument templateDoc = new HtmlDocument();
            templateDoc.LoadHtml(System.Net.WebUtility.UrlDecode(template.HtmlContent));
            HtmlDocument resumeDoc = new HtmlDocument();
            resumeDoc.LoadHtml(System.Net.WebUtility.UrlDecode(resume.HtmlContent));

            // Replace the template with the user's information
            ReplaceTextInDocument(templateDoc, resumeDoc, "Jasmine Patel", currUser.FirstName + " " + currUser.LastName);
            ReplaceTextInDocument(templateDoc, resumeDoc, "JASMINE", currUser.FirstName.ToUpper());
            ReplaceTextInDocument(templateDoc, resumeDoc, "PATEL", currUser.LastName.ToUpper());
            ReplaceTextInDocument(templateDoc, resumeDoc, "555-794-4847", currUser.PhoneNumber);
            ReplaceTextInDocument(templateDoc, resumeDoc, "patelj@mail.com", currUser.Email);

            // Check if the resume contains the sections for the template
            string[] sections = { "Summary", "Work Experience", "Experience", "Education", "Achievements", "Skills", "Projects", "Reference", "Profile"};

            // Create a dictionary to store the sections and their content
            Dictionary<string, List<HtmlNode>> sectionContent = new Dictionary<string, List<HtmlNode>>();
            string currUserSummary = string.Empty;

            // Check if the first node is a section title
            HtmlNode firstNode = resumeDoc.DocumentNode.FirstChild;
            bool firstNodeIsSectionTitle = false;

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
            
            if (!firstNodeIsSectionTitle)
            {
                // Create a list to store the content nodes
                List<HtmlNode> summaryNodes = new List<HtmlNode>();

                // Start with the node following the <hr> tag
                HtmlNode currentSummaryNode = firstNode;

                //Check if the first node is an <hr> tag and if so then remove it 
                if (firstNode.Name == "hr")
                {
                    currentSummaryNode = firstNode.NextSibling;
                    firstNode.Remove();
                }

                // Iterate over the following nodes
                while (currentSummaryNode != null)
                {
                    // If the current node is an <hr> tag or contains a section title, stop iterating
                    if (currentSummaryNode.Name == "hr" || sections.Any(s => currentSummaryNode.InnerText.ToLower().Contains(s.ToLower())))
                    {
                        break;
                    }

                    // Add the current node to the content nodes list
                    summaryNodes.Add(currentSummaryNode);

                    // Store the current node for removal
                    HtmlNode nodeToRemove = currentSummaryNode;

                    // Move to the next node
                    currentSummaryNode = currentSummaryNode.NextSibling;

                    // Remove the node from the document
                    nodeToRemove.Remove();
                }

                currUserSummary = string.Join(" ", summaryNodes.Select(node => node.InnerText));
            }

            // Iterate over the sections
            foreach (string section in sections)
            {
                // Convert the section name to lowercase
                string lowerSection = section.ToLower();

                // Check if the section is present in the resume
                HtmlNode resumeSection = resumeDoc.DocumentNode.SelectSingleNode($"//descendant::*[contains(translate(., 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '{lowerSection}')]");

                // If the section is present in the resume, store its content
                if (resumeSection != null)
                {

                    // Create a list to store the content nodes
                    List<HtmlNode> contentNodes = new List<HtmlNode>();

                    // Start with the node following the section title
                    HtmlNode currentNode = resumeSection.NextSibling;

                    // Iterate over the following nodes
                    while (currentNode != null)
                    {
                        // If the current node is an <hr> tag or contains a section title, stop iterating
                        if (currentNode.Name == "hr" || sections.Any(s => currentNode.InnerText.ToLower().Contains(s.ToLower())))
                        {
                            break;
                        }

                        // Add the current node to the content nodes list
                        contentNodes.Add(currentNode);

                        // Store the next node before removing the current node
                        HtmlNode nextNode = currentNode.NextSibling;

                        // Remove the current node from its parent
                        currentNode.Remove();

                        // Move to the next node
                        currentNode = nextNode;
                    }

                    // Store the content nodes in the dictionary
                    sectionContent[section] = contentNodes;

                    // If the section is "Summary" or "Profile", store its content in currUserSummary
                    if (section.ToLower() == "summary" || section.ToLower() == "profile")
                    {
                        currUserSummary = string.Join(" ", contentNodes.Select(node => node.InnerText));
                    }

                    // Remove the section title from the resume
                    resumeSection.Remove();
                }
            }

            if(currUserSummary.IsNullOrEmpty())
            {
                currUserSummary = currUser.Summary;
            }

            if (template.ResumeId == 3 || template.ResumeId == 5)
            {
                // Define the regex pattern to match the personal summary
                string pattern = @"Lorem.*?\.\.\.";

                // Find the personal summary in the template
                Regex regex = new Regex(pattern);
                Match match = regex.Match(templateDoc.DocumentNode.OuterHtml);

                // If the personal summary is found, replace it with the currUser's first name
                if (match.Success)
                {
                    string personalSummary = match.Value;
                    var nodes = templateDoc.DocumentNode.DescendantsAndSelf().Where(n => n.OuterHtml.Contains(personalSummary));
                    foreach (var node in nodes)
                    {
                        node.InnerHtml = node.InnerHtml.Replace(personalSummary, currUserSummary);
                        break; // replace only the first occurrence
                    }
                }
            }

            // Iterate over the sections in the template
            foreach (string section in sections)
            {
                // Convert the section name to lowercase
                string lowerSection = section.ToLower();

                // Check if the section is present in the template
                HtmlNode templateSection = templateDoc.DocumentNode.SelectSingleNode($"//descendant::*[contains(translate(., 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '{lowerSection}')]");

                // If the section is present in the template
                if (templateSection != null)
                {
                    // If the section is not present in the resume, remove it from the template
                    if (!sectionContent.ContainsKey(section))
                    {
                        // If currUserSummary is not empty and the current section is "Summary" or "Profile", replace the section content
                        if (!string.IsNullOrEmpty(currUserSummary) && (section.ToLower() == "summary" || section.ToLower() == "profile"))
                        {
                            ReplaceSectionContentWithString(templateSection, currUserSummary, sections);
                        }
                        else
                        {
                            RemoveSectionAndContent(templateSection, sections);
                        }
                    }
                    else // If the section is present in the resume, replace the template's content with the resume's content
                    {
                        ReplaceSectionContent(templateSection, sectionContent[section], sections);
                    }
                }
            }

            // Convert the modified HtmlDocument back to a string
            template.HtmlContent = templateDoc.DocumentNode.OuterHtml;

            // Append the remaining content from the resume to the end of the template
            template.HtmlContent += resumeDoc.DocumentNode.OuterHtml;

            // Return the modified template
            return template;
        }

        private void RemoveSectionAndContent(HtmlNode section, string[] sections)
        {
            // Remove the <hr> tags before and after the section
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

            // Remove the content of the section
            while (section.NextSibling != null && !(section.NextSibling.Name == "hr" || sections.Any(s => section.NextSibling.InnerText.ToLower().Contains(s.ToLower()))))
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

            // Remove the <hr> tag after the section if the next section is not immediately following
            if (section.NextSibling != null && section.NextSibling.Name == "hr" && (section.NextSibling.NextSibling == null || !sections.Any(s => section.NextSibling.NextSibling.InnerText.ToLower().Contains(s.ToLower()))))
            {
                section.NextSibling.Remove();
            }

            // Remove the section
            section.Remove();
            
        }

        private void ReplaceSectionContent(HtmlNode section, List<HtmlNode> contentNodes, string[] sections)
        {
            HtmlNode firstSibling = section.NextSibling;

            //Skip the <hr> tag if it is the first sibling
            if(firstSibling.Name == "hr")
            {
                section = firstSibling;
            }

            // Remove the existing content of the section
            while (section.NextSibling != null && !(section.NextSibling.Name == "hr" || sections.Any(s => section.NextSibling.InnerText.ToLower().Contains(s.ToLower()))))
            {
                section.NextSibling.Remove();
            }

            // Determine where to insert the new content
            HtmlNode insertAfterNode = section;
            if (section.NextSibling != null && section.NextSibling.Name == "hr")
            {
                insertAfterNode = section.NextSibling;
            }

            // Add the new content to the section
            foreach (HtmlNode contentNode in contentNodes)
            {
                section.ParentNode.InsertAfter(contentNode, insertAfterNode);
                insertAfterNode = contentNode; // Update the node to insert after
            }

            // Create a new <br> tag
            HtmlNode brTag = HtmlNode.CreateNode("<br />");

            // Insert the <br> tag after the last content node
            section.ParentNode.InsertAfter(brTag, insertAfterNode);
        }

        private void ReplaceSectionContentWithString(HtmlNode section, string currUserSummary, string[] sections)
        {
            HtmlNode firstSibling = section.NextSibling;

            //Skip the <hr> tag if it is the first sibling
            if(firstSibling.Name == "hr")
            {
                section = firstSibling;
            }

            // Remove the existing content of the section
            while (section.NextSibling != null && !(section.NextSibling.Name == "hr" || sections.Any(s => section.NextSibling.InnerText.ToLower().Contains(s.ToLower()))))
            {
                section.NextSibling.Remove();
            }

            // Determine where to insert the new content
            HtmlNode insertAfterNode = section;
            if (section.NextSibling != null && section.NextSibling.Name == "hr")
            {
                insertAfterNode = section.NextSibling;
            }

            // Add the new content to the section
            HtmlNode newNode = HtmlNode.CreateNode(currUserSummary);
            section.ParentNode.InsertAfter(newNode, insertAfterNode);
            insertAfterNode = newNode; // Update the node to insert after

            // Create a new <br> tag
            HtmlNode brTag = HtmlNode.CreateNode("<br />");

            // Insert the <br> tag after the last content node
            section.ParentNode.InsertAfter(brTag, insertAfterNode);
        }

        private void ReplaceTextInDocument(HtmlDocument templateDoc, HtmlDocument resumeDoc, string oldText, string newText)
        {
            foreach (HtmlNode node in templateDoc.DocumentNode.DescendantsAndSelf())
            {
                if (node.NodeType == HtmlNodeType.Text)
                {
                    // Replace the text in the node
                    node.InnerHtml = node.InnerHtml.Replace(oldText, newText);
                }
            }

            foreach (HtmlNode node in resumeDoc.DocumentNode.DescendantsAndSelf())
            {
                if (node.NodeType == HtmlNodeType.Text)
                {
                    // Remove the replaced text from the resume
                    node.InnerHtml = node.InnerHtml.Replace(newText, "");
                }
            }
        }
    }
}