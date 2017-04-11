using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FireChatCore
{
    public class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.messages = new HashSet<Message>();
            this.user_chat = new HashSet<UserChat>();
        }

        public int user_id { get; set; } 
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public Nullable<bool> is_admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> messages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserChat> user_chat { get; set; }
    }
}
