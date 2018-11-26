using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AskAbout.Models
{
    public class Topic : BaseEntity
    {
//        [Required(ErrorMessageResourceName = "Select",
//            ErrorMessageResourceType = typeof(Resources.Models.Topic))]
        public string Name { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
    }
}