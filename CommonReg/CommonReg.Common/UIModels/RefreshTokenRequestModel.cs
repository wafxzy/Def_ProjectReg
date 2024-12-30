using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.Common.UIModels
{
    public class RefreshTokenRequestModel
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
