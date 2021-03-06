using System.ComponentModel.DataAnnotations;

namespace Buddies.API.Entities;

/// <summary>
/// Profile entity.
/// </summary>
public class Profile
{
    /// <summary>
    /// Initializes a new profile entity.
    /// </summary>
    /// <param name="firstName">First name on the profile.</param>
    /// <param name="lastName">Last name on the profile.</param>
    public Profile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Headline = "Project Enthusiast";
        AboutMe = "Hi, welcome to my profile.";
        Skills = new List<Skill>();
    }

    /// <summary>
    /// First name on the profile.
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Last name on the profile.
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// ID of the user the profile belongs to.
    /// </summary>
    [Key]
    public int UserId { get; init; }

    /// <summary>
    /// BuddyScore of the user the profile belongs to.
    /// </summary>
    public float BuddyScore { get; set; }

    /// <summary>
    /// Number of the projects this user has been a part of.
    /// </summary>
    public int ProjectCount { get; set; }

    /// <summary>
    /// headline on the profile
    /// </summary>
    [MaxLength(50)]
    public string Headline { get; set; }

    /// <summary>
    /// about me section on the profile
    /// </summary>
    [MaxLength(500)]
    public string AboutMe { get; set; }

    /// <summary>
    /// Users skills on the profile
    /// </summary>
    public List<Skill> Skills { get; set; }

    /// <summary>
    /// User the profile belongs to.
    /// </summary>
    public User User { get; init; } = null!; // populated by EF 
}