//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibHutech_DoAnLapTrinhWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SinhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SinhVien()
        {
            this.LichSus = new HashSet<LichSu>();
        }
    
        public int MaSV { get; set; }
        public string HoTen { get; set; }
        public string Nganh { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string ImgSv { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> Role { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSu> LichSus { get; set; }
    }
}
