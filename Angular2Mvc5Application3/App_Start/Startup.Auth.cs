using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
//using Microsoft.Owin.Security.Google;
using Owin;
using Schemes.Models;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Security;



namespace Schemes
{
    public partial class Startup
    {
        // Дополнительные сведения о настройке проверки подлинности см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Настройка контекста базы данных, диспетчера пользователей и диспетчера входа для использования одного экземпляра на запрос
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Включение использования файла cookie, в котором приложение может хранить информацию для пользователя, выполнившего вход,
            // и использование файла cookie для временного хранения информации о входах пользователя с помощью стороннего поставщика входа
            // Настройка файла cookie для входа
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Позволяет приложению проверять метку безопасности при входе пользователя.
                    // Эта функция безопасности используется, когда вы меняете пароль или добавляете внешнее имя входа в свою учетную запись.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Позволяет приложению временно хранить информацию о пользователе, пока проверяется второй фактор двухфакторной проверки подлинности.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Позволяет приложению запомнить второй фактор проверки имени входа. Например, это может быть телефон или почта.
            // Если выбрать этот параметр, то на устройстве, с помощью которого вы входите, будет сохранен второй шаг проверки при входе.
            // Точно так же действует параметр RememberMe при входе.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Раскомментируйте приведенные далее строки, чтобы включить вход с помощью сторонних поставщиков входа
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = "G1rYEP5u9WRpnYWBAsCc9tftj",
                ConsumerSecret = "hZFbpAilpv4PI7emrcAG8iOlwsbDIoGTxbJ8rpzq8lQW0ijhjj",
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(new[]
                {
                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2 
                    "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3 
                    "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5 
                    "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4 
                    "add53f6680fe66e383cbac3e60922e3b4c412bed", // Symantec Class 3 EV SSL CA - G3 
                    "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5 
                    "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server CA 
                    "B13EC36903F8BF4701D498261A0802EF63642BC3" // DigiCert High Assurance EV Root CA 
})
            });

            app.UseFacebookAuthentication(
               appId: "870014633130666",
               appSecret: "489de32988b433334553e0e6841962c3");

            app.UseVkontakteAuthentication("5502300", "caIRMZqp5thapITKvEjc", "email");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}