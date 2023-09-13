using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EmailLibrary
{
    public class EmailDriver:IEmailDriver
    {
        private readonly IConfiguration _configuration;
        public EmailDriver(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("EmailConfiguration");
        }

        public async Task SendEmail(string reciever, string body, string subject)
        {
            try
            {
                string fromMail = _configuration["EmailId"];
                string fromPassword = _configuration["Password"];
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = subject;
                message.To.Add(new MailAddress(reciever));
                message.Body = body;
                message.IsBodyHtml = true;
                var smtpClient = new SmtpClient(_configuration["Host"])
                {
                    UseDefaultCredentials = false,
                    Port = Convert.ToInt32(_configuration["Port"]),
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = Convert.ToBoolean(_configuration["UseSSL"]),
                };
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public async Task PasswordReset(string email, string resetLink)
        {
            try
            {
                var template = GenerateEmailBody("", "", resetLink);



                string fromMail = _configuration["EmailId"];
                /*                await SendEmail(email,template,"Password Reset");
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private string GenerateEmailBodyForAccountVerification(string firstName, string lastName, string verificationLink)
        {
            return $@"<!DOCTYPE html>
                    <html>

                    <head>
                        <meta charset=""utf-8"" />
                        <title></title>
                    </head>

                    <body>
                        <div class="""">
                            <div class=""aHl""></div>
                            <div id="":1a4"" tabindex=""-1""></div>
                            <div id="":19t"" class=""ii gt""
                                jslog=""20277; u014N:xr6bB; 1:WyIjdGhyZWFkLWE6cjI1OTAwMTcyNzIyMzMxNjcxMzQiLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLFtdXQ..; 4:WyIjbXNnLWE6ci01Mzc1MjQ4MTMxNzkwNjAxNDkiLG51bGwsW11d"">
                                <div id="":19s"" class=""a3s aiL "">
                                    <div dir=""ltr"">
                                        <table bgcolor=""#f2f4fd"" cellpadding=""0"" cellspacing=""0"" width=""100%"" align=""center""
                                            role=""presentation""
                                            style=""color: rgb(0,0,0); font-family: &quot; Helvetica Neue&quot;,Helvetica,Arial,sans-serif; font-size: medium; min-width:480px; width: 650px; border-collapse: collapse; border-width: 10px; border-color:#f2f4fd; border-style: solid;"">
                                            <tbody>
                                                <tr>
                                                    <td align=""left"" valign=""top"" bgcolor=""#3260f7"" dir=""ltr""
                                                        style=""padding:30px 40px;direction:ltr"">
                                                        <center><img
                                                                src=""https://ci6.googleusercontent.com/proxy/XO9XwiTKNGbDm__IGQ5UvnfsfRMk4iikhBzcMigxPVZC1N0jzDRYm97Okyr3yjXD7luolt-NqrIG6ZrsBihxkuCFVLGTQYncPJTx6Q=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/logo.png""
                                                                alt=""Logo"" style=""width:150.238px"" class=""CToWUd"" data-bit=""iit""></center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=""left"" valign=""top"" bgcolor=""#f2f4fd"" dir=""ltr""
                                                        style=""padding:37px;direction:ltr;color:rgb(48,48,48);font-size:14px"">
                                                        <div style=""background-color:rgb(255,255,255);padding:30px"">
                                                            <p>&nbsp;</p>
                                                            <p> Hi {firstName},</p>
                                                            <p>Welcome to our platform! To verify your account, please click the button below:</p>
                                                            <p><a href='{verificationLink}' class='button'>Verify Account</a></p>
                                                            <p>If you did not sign up for an account, you can ignore this email.</p>
                                                            <p>Best regards,<br>Activio</p>
                                                            <p>
                                                                Best regards,
                                                            </p>
                                                            <p>&nbsp;</p>

                                                            <p> The [Company Name] Team</p>


                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=""center"" valign=""top"" bgcolor=""#ffffff"" dir=""ltr""></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p
                                                            style=""color:rgb(0,0,0);font-family:&quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size:medium;text-align:-webkit-center"">
                                                            <img src=""https://ci3.googleusercontent.com/proxy/yf5hPllGSGAnVXAFMr7TagrsqD92ustDZ0sQFb_4NpVvfZDQyC3_N8B-A2kFshI5iJNR5s7OqNM0WRBTvVC_opscL-jOnazL8mulntinKBX0zIpGxbmJArxLbl-I57j1qaM3PBZh85tQkF66=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/7859d62f-505b-4fca-95da-238e7dbb02ff.png""
                                                                alt=""Logo"" style=""width:156.812px"" class=""CToWUd"" data-bit=""iit""></p>
                                                    </td>
                                                </tr>
                                                <tr></tr>
                                                <tr></tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align=""left"" valign=""top"" bgcolor=""#3260f7"" dir=""ltr""
                                                        style=""padding:30px 40px 10px;direction:ltr;width:500.8px;height:129.8px;background-color:rgb(50,96,247);border-top-left-radius:100%;border-top-right-radius:100%"">
                                                        <p>&nbsp;</p>
                                                        <p>&nbsp;</p>
                                                        <center>
                                                            <p><a href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci5.googleusercontent.com/proxy/MhfMiq8oZFN8M1F5v6EQjlq3mkg_eL0zb69PuGzG6v9LRS69RM9jjIwLic3VdUUbT8VE0L2VywkB1gKIIH88g10dSEO4p7MZFQU1crWDQAgChcLL2Kxb70kwoJ__23jPN9HQo0kwZ1fKBF5f=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/b02f7ba7-a25e-4063-9540-a2ae641c3019.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit""></a>&nbsp;&nbsp;<a
                                                                    href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci3.googleusercontent.com/proxy/B6N6HcYxnnRWhlppM3ruGTpgwMe7k1l4jlzNhZqXnmT_-KfLHBtAm7H9np6ah4k56xBtI5PZqc_SHd63QzBoR2Mr5MBngLWwvA7I9iFEfKGrGOhdFAhDaB5TQ5DdNZOlBT6vDbixvtxkVUjI=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/8dacde0b-7e5f-40e6-8a1d-1411dfd297d8.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit"">&nbsp;</a>&nbsp;&nbsp;<a
                                                                    href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci4.googleusercontent.com/proxy/ZRa7T8s-qDinZnpMlG4y73PDFzpkUnNMUP5rcIWXISnFz6thR34GkB_cI8fREFaaZRhxxenaNh7Rm9lDTmGlJzuGqwtv6aavZ1w9VhLjmxyGbYL048Qu5C0MHoxsZccfs884Vn8B6fYEJAkX=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/f597fdcc-87df-43a8-ab59-be7c106d89ad.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit"">&nbsp;</a>&nbsp;&nbsp;<a
                                                                    href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci5.googleusercontent.com/proxy/CzvAXYmxAcOdSnM-94P6jVQNbjflHWuLvro_54bBTNOt2sDYzVzHT77liC0N7qSyE4DnUnP9ff1I2iGYgL55YK6LcpoCDeAM0hJhkJ2TEWdha9OC6GDe1lhuseMTEq7mjR0xYF5CZno2HCzn=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/05e0242b-0502-4afe-8a0d-160577e1b118.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit"">&nbsp;</a>&nbsp;&nbsp;<a
                                                                    href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci4.googleusercontent.com/proxy/SeQr-D00121LObcj1QE166GJIzQgHVAja7zmsSg-_BzjLl4_bs8x0z_uEr0ltJWYJx5Ahg99SpgZBML18O4c7IrijSHDZ22gnkj4YcCxUh0PkJdOlze4ANRUvR2CA_MoHtK_JbBG1P2mLUMY=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/86da0bd9-dd50-4fd9-a1f6-70b8d1a8a5e9.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit""></a></p>
                                                        </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=""center"" valign=""top"" style=""padding:0px""></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class=""yj6qo""></div>
                                    <div class=""adL""></div>
                                </div>
                            </div>
                            <div id="":1a8"" class=""ii gt"" style=""display:none"">
                                <div id="":1a9"" class=""a3s aiL ""></div>
                            </div>
                            <div class=""hi""></div>
                        </div>

                    </body>

                    </html>

                    <!DOCTYPE html>";
        }
        private string GenerateEmailBody(string firstName, string lastName, string resetLink)
        {
            return $@"
                <!DOCTYPE html>
                    <html>

                    <head>
                        <meta charset=""utf-8"" />
                        <title></title>
                    </head>

                    <body>
                        <div class="""">
                            <div class=""aHl""></div>
                            <div id="":1a4"" tabindex=""-1""></div>
                            <div id="":19t"" class=""ii gt""
                                jslog=""20277; u014N:xr6bB; 1:WyIjdGhyZWFkLWE6cjI1OTAwMTcyNzIyMzMxNjcxMzQiLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLFtdXQ..; 4:WyIjbXNnLWE6ci01Mzc1MjQ4MTMxNzkwNjAxNDkiLG51bGwsW11d"">
                                <div id="":19s"" class=""a3s aiL "">
                                    <div dir=""ltr"">
                                        <table bgcolor=""#f2f4fd"" cellpadding=""0"" cellspacing=""0"" width=""100%"" align=""center""
                                            role=""presentation""
                                            style=""color: rgb(0,0,0); font-family: &quot; Helvetica Neue&quot;,Helvetica,Arial,sans-serif; font-size: medium; min-width:480px; width: 650px; border-collapse: collapse; border-width: 10px; border-color:#f2f4fd; border-style: solid;"">
                                            <tbody>
                                                <tr>
                                                    <td align=""left"" valign=""top"" bgcolor=""#3260f7"" dir=""ltr""
                                                        style=""padding:30px 40px;direction:ltr"">
                                                        <center><img
                                                                src=""https://ci6.googleusercontent.com/proxy/XO9XwiTKNGbDm__IGQ5UvnfsfRMk4iikhBzcMigxPVZC1N0jzDRYm97Okyr3yjXD7luolt-NqrIG6ZrsBihxkuCFVLGTQYncPJTx6Q=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/logo.png""
                                                                alt=""Logo"" style=""width:150.238px"" class=""CToWUd"" data-bit=""iit""></center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=""left"" valign=""top"" bgcolor=""#f2f4fd"" dir=""ltr""
                                                        style=""padding:37px;direction:ltr;color:rgb(48,48,48);font-size:14px"">
                                                        <div style=""background-color:rgb(255,255,255);padding:30px"">
                                                            <p>&nbsp;</p>
                                                            <p> Hi {firstName},</p>

                                                            <p>
                                                                We have received a request to reset the password for your account.
                                                                If you did not make this request, please disregard this
                                                                email.
                                                            </p>
                                                            <p>
                                                                To reset your password, please click the link below and follow the
                                                                instructions provided:
                                                            </p>
                                                            <p>

                                                                <a href='{resetLink}'>[Password Reset Link]</a>
                                                            </p>
                                                            <p>
                                                                If the link above does not work, please copy and paste the following URL
                                                                into your browser:
                                                            </p>
                                                            <p>
                                                                <a href=""#"">{resetLink}</a>

                                                            </p>
                                                            <p>
                                                                If you have any questions or concerns, please contact our customer support
                                                                team for assistance.
                                                            </p>
                                                            <p>
                                                                Best regards,
                                                            </p>
                                                            <p>&nbsp;</p>

                                                            <p> The [Company Name] Team</p>


                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=""center"" valign=""top"" bgcolor=""#ffffff"" dir=""ltr""></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p
                                                            style=""color:rgb(0,0,0);font-family:&quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size:medium;text-align:-webkit-center"">
                                                            <img src=""https://ci3.googleusercontent.com/proxy/yf5hPllGSGAnVXAFMr7TagrsqD92ustDZ0sQFb_4NpVvfZDQyC3_N8B-A2kFshI5iJNR5s7OqNM0WRBTvVC_opscL-jOnazL8mulntinKBX0zIpGxbmJArxLbl-I57j1qaM3PBZh85tQkF66=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/7859d62f-505b-4fca-95da-238e7dbb02ff.png""
                                                                alt=""Logo"" style=""width:156.812px"" class=""CToWUd"" data-bit=""iit""></p>
                                                    </td>
                                                </tr>
                                                <tr></tr>
                                                <tr></tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align=""left"" valign=""top"" bgcolor=""#3260f7"" dir=""ltr""
                                                        style=""padding:30px 40px 10px;direction:ltr;width:500.8px;height:129.8px;background-color:rgb(50,96,247);border-top-left-radius:100%;border-top-right-radius:100%"">
                                                        <p>&nbsp;</p>
                                                        <p>&nbsp;</p>
                                                        <center>
                                                            <p><a href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci5.googleusercontent.com/proxy/MhfMiq8oZFN8M1F5v6EQjlq3mkg_eL0zb69PuGzG6v9LRS69RM9jjIwLic3VdUUbT8VE0L2VywkB1gKIIH88g10dSEO4p7MZFQU1crWDQAgChcLL2Kxb70kwoJ__23jPN9HQo0kwZ1fKBF5f=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/b02f7ba7-a25e-4063-9540-a2ae641c3019.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit""></a>&nbsp;&nbsp;<a
                                                                    href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci3.googleusercontent.com/proxy/B6N6HcYxnnRWhlppM3ruGTpgwMe7k1l4jlzNhZqXnmT_-KfLHBtAm7H9np6ah4k56xBtI5PZqc_SHd63QzBoR2Mr5MBngLWwvA7I9iFEfKGrGOhdFAhDaB5TQ5DdNZOlBT6vDbixvtxkVUjI=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/8dacde0b-7e5f-40e6-8a1d-1411dfd297d8.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit"">&nbsp;</a>&nbsp;&nbsp;<a
                                                                    href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci4.googleusercontent.com/proxy/ZRa7T8s-qDinZnpMlG4y73PDFzpkUnNMUP5rcIWXISnFz6thR34GkB_cI8fREFaaZRhxxenaNh7Rm9lDTmGlJzuGqwtv6aavZ1w9VhLjmxyGbYL048Qu5C0MHoxsZccfs884Vn8B6fYEJAkX=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/f597fdcc-87df-43a8-ab59-be7c106d89ad.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit"">&nbsp;</a>&nbsp;&nbsp;<a
                                                                    href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci5.googleusercontent.com/proxy/CzvAXYmxAcOdSnM-94P6jVQNbjflHWuLvro_54bBTNOt2sDYzVzHT77liC0N7qSyE4DnUnP9ff1I2iGYgL55YK6LcpoCDeAM0hJhkJ2TEWdha9OC6GDe1lhuseMTEq7mjR0xYF5CZno2HCzn=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/05e0242b-0502-4afe-8a0d-160577e1b118.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit"">&nbsp;</a>&nbsp;&nbsp;<a
                                                                    href=""#m_-6013500286260698281_""><img
                                                                        src=""https://ci4.googleusercontent.com/proxy/SeQr-D00121LObcj1QE166GJIzQgHVAja7zmsSg-_BzjLl4_bs8x0z_uEr0ltJWYJx5Ahg99SpgZBML18O4c7IrijSHDZ22gnkj4YcCxUh0PkJdOlze4ANRUvR2CA_MoHtK_JbBG1P2mLUMY=s0-d-e1-ft#https://activiodev.blob.core.windows.net/activio/86da0bd9-dd50-4fd9-a1f6-70b8d1a8a5e9.png""
                                                                        width=""25"" class=""CToWUd"" data-bit=""iit""></a></p>
                                                        </center>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align=""center"" valign=""top"" style=""padding:0px""></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class=""yj6qo""></div>
                                    <div class=""adL""></div>
                                </div>
                            </div>
                            <div id="":1a8"" class=""ii gt"" style=""display:none"">
                                <div id="":1a9"" class=""a3s aiL ""></div>
                            </div>
                            <div class=""hi""></div>
                        </div>

                    </body>

                    </html>
               <!DOCTYPE html>";
        }

        public Task AccountVerified(string email, string resetLink)
        {
            throw new NotImplementedException();
        }

        public async Task VerifyEmail(string email, string resetLink)
        {
            try
            {
                var template = $@"
                <!DOCTYPE html>
                    <html>

                    <head>
                        <meta charset=""utf-8"" />
                        <title></title>
                    </head>

                    <body>
                        <div class="""">
                           <h3>Please confirm Email</p>
                        <p>
                             <a href=""#"">{resetLink}</a>

                         </p>
                                                          
                        </div>

                    </body>

                    </html>
               <!DOCTYPE html>";



                string fromMail = _configuration["EmailId"];
                await SendEmail(email,template,"Confirmation Mail");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
