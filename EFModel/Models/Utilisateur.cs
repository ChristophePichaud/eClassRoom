using EFModel.Models;

/// <summary>
/// User in the eClassRoom system
/// </summary>
public class Utilisateur
{
    public int Id { get; set; }
    
    /// <summary>
    /// Email address (used for login)
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Last name
    /// </summary>
    public string Nom { get; set; }
    
    /// <summary>
    /// First name
    /// </summary>
    public string Prenom { get; set; }
    
    /// <summary>
    /// Hashed password
    /// </summary>
    public string MotDePasse { get; set; }
    
    /// <summary>
    /// User role (SuperAdmin, Admin, Student)
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Company/Client ID this user belongs to
    /// </summary>
    public int ClientId { get; set; }
    
    // Navigation property
    public Client Client { get; set; }
}