namespace Shared.Dtos
{
    /// <summary>
    /// Data Transfer Object for Virtual Machine
    /// </summary>
    public class MachineVirtuelleDto
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Name of the virtual machine
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Owner ID - the student user who owns this VM
        /// </summary>
        public int OwnerId { get; set; }
        
        /// <summary>
        /// Operating system type: Linux, Windows
        /// </summary>
        public string VmOsType { get; set; }
        
        /// <summary>
        /// Azure VM type/size: Standard_B2s, Standard_B4ms, etc.
        /// </summary>
        public string VmType { get; set; }
        
        /// <summary>
        /// Azure VM Machine ID (unique identifier in Azure)
        /// </summary>
        public string? VmMachineId { get; set; }
        
        /// <summary>
        /// ID/reference to ISO stored in Blob Storage
        /// </summary>
        public string? VmISO { get; set; }
        
        /// <summary>
        /// Azure SKU
        /// </summary>
        public string Sku { get; set; }
        
        /// <summary>
        /// Azure Offer
        /// </summary>
        public string Offer { get; set; }
        
        /// <summary>
        /// Azure Version
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// RDP file content for connection
        /// </summary>
        public string? RdpInfo { get; set; }
        
        /// <summary>
        /// Public IP address for connection
        /// </summary>
        public string? PublicIp { get; set; }
        
        /// <summary>
        /// Current status: NotCreated, Created, Active, Stopped, Unknown
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Marketing name for the VM
        /// </summary>
        public string? NomMarketing { get; set; }
    }
}
