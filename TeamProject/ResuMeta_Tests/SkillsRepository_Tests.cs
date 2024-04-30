using ResuMeta.DAL.Abstract;
using ResuMeta.DAL.Concrete;
using ResuMeta.Models;
using System.Web;

namespace ResuMeta_Tests;

public class SkillsRepository_Tests
{
    // NOTE: Windows use line 11, Mac use line 12
    // private static readonly string _seedFile = @"..\..\..\Data\SeedSkills.sql";
    private static readonly string _seedFile = "../../../Data/SeedSkills.sql";
    private InMemoryDbHelper<ResuMetaDbContext> _dbHelper = new InMemoryDbHelper<ResuMetaDbContext>(_seedFile, DbPersistence.OneDbPerTest);

    // These tests feel wrong... 
    [TestCase("Lua", new[] {"Lua"})]
    [TestCase("Int", new[] { "International Risk Management", "International Intellectual Property", "International Business Compliance", "International Marketing" })]
    [TestCase("et", new[] { "Poetry Writing", "International Marketing", "Network Security" })]
    public void GetSkillsBySubString_Returns_SkillsThatContainSubstring_Test(string substring, string[] expected)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ISkillsRepository repo = new SkillsRepository(context);

        List<Skill> skills = repo.GetSkillsBySubstring(substring).ToList();

        Assert.That(skills.Count, Is.EqualTo(expected.Length));

        foreach (Skill actualSkill in skills)
        {
            Assert.Contains(actualSkill.SkillName, expected);
        }
    }


    [TestCase("NotARealEntry")]
    [TestCase("Not A Real Entry")]
    [TestCase("")]
    public void GetSkillsBySubString_Returns_EmptyList_Given_NoMatchingSkills_Test(string substring)
    {
        using ResuMetaDbContext context = _dbHelper.GetContext();
        ISkillsRepository repo = new SkillsRepository(context);

        List<Skill> skills = repo.GetSkillsBySubstring(substring).ToList();

        Assert.That(skills.Count, Is.EqualTo(0));
        Assert.That(skills, Is.Empty);
    }
}