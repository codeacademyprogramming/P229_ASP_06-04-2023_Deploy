using Microsoft.AspNetCore.Identity;

namespace P229Allup.Extentions
{
    public class IdentityErrorDescriberAZ : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Code = nameof(PasswordRequiresDigit),Description="Reqem Mutleqdir" };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Kicik Herif Mutleqdir" };
        }
    }
}
