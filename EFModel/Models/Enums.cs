namespace EFModel.Models
{
    /// <summary>
    /// License type for a customer/client
    /// </summary>
    public enum LicenseType
    {
        Academic,
        Professional,
        Gov
    }

    /// <summary>
    /// User role in the system
    /// </summary>
    public enum UserRole
    {
        SuperAdmin,
        Admin,
        Student
    }

    /// <summary>
    /// Operating system type for virtual machines
    /// </summary>
    public enum VmOsType
    {
        Linux,
        Windows
    }

    /// <summary>
    /// Azure VM type/size
    /// </summary>
    public enum VmType
    {
        Standard_B2s,
        Standard_B4ms,
        Standard_D2s_v3,
        Standard_D4s_v3,
        Standard_E2s_v3,
        Standard_E4s_v3
    }

    /// <summary>
    /// Status of a virtual machine
    /// </summary>
    public enum VmStatus
    {
        NotCreated,
        Created,
        Active,
        Stopped,
        Unknown
    }
}
