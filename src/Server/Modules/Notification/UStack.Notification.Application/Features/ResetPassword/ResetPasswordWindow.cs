namespace UStack.Notification.Application.Features.ResetPassword;

public static class ResetPasswordWindow
{
    public const string Body = @"
        <!DOCTYPE html>
        <html lang='en'>
        <head>
          <meta charset='UTF-8' />
          <meta name='viewport' content='width=device-width, initial-scale=1.0' />
          <title>Reset Password</title>
        </head>
        <body style='margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif;'>

          <table width='100%' cellpadding='0' cellspacing='0'>
            <tr>
              <td align='center' style='padding:40px 0;'>
        
                <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff; border-radius:8px; box-shadow:0 4px 12px rgba(0,0,0,0.08);'>
          
                  <tr>
                    <td style='padding:24px 32px; border-bottom:1px solid #eaeaea;'>
                      <h2 style='margin:0; color:#111827;'>Reset your password</h2>
                    </td>
                  </tr>

                  <tr>
                    <td style='padding:32px; color:#374151; font-size:15px; line-height:1.6;'>
                      <p>Hello,</p>

                      <p>
                        We received a request to reset your password.<br />
                        Use the verification code below to continue:
                      </p>

                      <div style='margin:24px 0; text-align:center;'>
                        <span style='display:inline-block; padding:14px 24px; font-size:24px; letter-spacing:6px; font-weight:bold; background-color:#f3f4f6; color:#111827; border-radius:6px;'>
                          {{CODE}}
                        </span>
                      </div>

                      <p>
                        This code will expire in <strong>{{EXPIRATION_MINUTES}} minutes</strong>.
                      </p>

                      <p style='color:#6b7280; font-size:13px;'>
                        If you didn’t request a password reset, you can safely ignore this email.
                      </p>
                    </td>
                  </tr>

                  <tr>
                    <td style='padding:20px 32px; background-color:#f9fafb; color:#9ca3af; font-size:12px; border-top:1px solid #eaeaea;'>
                      <p style='margin:0;'>
                        © {{YEAR}} UStack. All rights reserved.
                      </p>
                    </td>
                  </tr>

                </table>

              </td>
            </tr>
          </table>

        </body>
        </html>
        ";
}
