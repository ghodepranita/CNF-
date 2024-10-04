using System;
using System.IO;
using System.Net.Mail;

namespace CNF.Business.BusinessConstant
{
    public class EmailNotification
    {
        #region Send Notification To Picker For Picklist Add
        /// <summary>
        /// For Notification related to picklist allotment
        /// </summary>
        /// <param name="ToEmail"></param>
        /// <param name="CCEmail"></param>
        /// <param name="Subject"></param>
        /// <param name="PicklistNo"></param>
        /// <param name="MailFilePath"></param>
        /// <returns></returns>
        public static bool SendEmails(string ToEmail, string CCEmail, string Subject, string PicklistNo, string MailFilePath)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            // Create the mail message
            MailMessage mailMessage = null;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();
                if (!string.IsNullOrWhiteSpace(ToEmail))//Recipient Email
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(CCEmail))//Recipient Email cc
                {
                    string CCEmailId = CCEmail.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(CCEmail))
                {
                    string messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                    // Replace Notification Table
                    messageformat = messageformat.Replace("<!--SchedulerTableString-->", PicklistNo);
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = messageformat;
                    mailClient.Send(mailMessage);

                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmails", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            return bResult;
        }
        #endregion

        #region Send Emails To Stockist For New Cheque
        public bool SendEmailsToStockist(string ToEmail, string StockistName, string EmailCC, string BCCEmail, string Subject, string BodyText)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            MailMessage mailMessage = null;
            string MailFilePath = string.Empty, MappedFilePath = string.Empty, messageformat = string.Empty;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();

                if (!string.IsNullOrWhiteSpace(ToEmail))
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId.Trim());
                    }
                }
                if (!string.IsNullOrWhiteSpace(EmailCC))
                {
                    string CCEmailId = EmailCC.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }
                MappedFilePath = AppDomain.CurrentDomain.BaseDirectory;
                MailFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailFile\\SendEmailForCheque.html");
                messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                messageformat = messageformat.Replace("<!--StockiestName-->", StockistName);
                messageformat = messageformat.Replace("<!--SchedulerTableString-->", BodyText);
                mailMessage.Subject = Subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = messageformat;
                mailClient.Send(mailMessage);
                bResult = true;
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailsToStockist", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            finally
            {
                if (mailClient != null)
                    mailClient.Dispose();

                if (mailMessage != null)
                    mailMessage.Dispose();
            }
            return bResult;
        }
        #endregion

        #region Send Email To Stockiest For Dispatch Done
        //Send Email To Stockiest For Dispatch Done
        public static bool SendEmailForDispatchDone(string ToEmail, string CCEmail, string Subject, string StockistName, string TransporterName, string CompanyName, string MailFilePath)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            // Create the mail message
            MailMessage mailMessage = null;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();
                if (!string.IsNullOrWhiteSpace(ToEmail))//Recipient Email
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(CCEmail))//Recipient Email cc
                {
                    string CCEmailId = CCEmail.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(CCEmail))
                {
                    string messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                    // Replace Notification Table
                    messageformat = messageformat.Replace("<!--StockiestName-->", StockistName);
                    messageformat = messageformat.Replace("<!--DatedOn-->", DateTime.Today.Date.ToString("dd-MM-yyyy"));
                    messageformat = messageformat.Replace("<!--TransportName-->", TransporterName);
                    messageformat = messageformat.Replace("<!--companyName-->", CompanyName);
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = messageformat;
                    mailClient.Send(mailMessage);

                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailForDispatchDone", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            return bResult;
        }
        #endregion

        #region Send Email To Stockiest For Outstanding Alert
        public bool SendEmailForOutstanding(string ToEmail, string CCEmail, string BCCEmail, string Subject, decimal TotOverdueAmt, string StockistName, string OSDate, string MailFilePath)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            // Create the mail message
            MailMessage mailMessage = null;
            string messageformat = string.Empty;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();

                if (!string.IsNullOrWhiteSpace(ToEmail))//Recipient Email
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(CCEmail))//Recipient Email cc
                {
                    string CCEmailId = CCEmail.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(CCEmail) && !string.IsNullOrEmpty(BCCEmail))
                {
                    messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                    // Replace Notification Table
                    messageformat = messageformat.Replace("<!--StockiestName-->", StockistName);
                    messageformat = messageformat.Replace("<!--TotOverdueAmt-->", Convert.ToString(TotOverdueAmt));
                    messageformat = messageformat.Replace("<!--Date-->", OSDate);
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = messageformat;
                    mailClient.Send(mailMessage);
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailForOutstanding", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return bResult;
        }
        #endregion

        #region Send Emails To Stockist For Cheque Deposited
        public bool SendEmailsForChqDeposit(string ToEmail, string StockistName, string EmailCC, string BCCEmail, string Subject, string ChqNo, string InvNo,decimal ChqAmount)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            MailMessage mailMessage = null;
            string MailFilePath = string.Empty;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();

                if (!string.IsNullOrWhiteSpace(ToEmail))
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId.Trim());
                    }
                }
                if (!string.IsNullOrWhiteSpace(EmailCC))
                {
                    string CCEmailId = EmailCC.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }

                string MappedFilePath = AppDomain.CurrentDomain.BaseDirectory;
                MailFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailFile\\SendEmailForChqDeposit.html");
                string messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                messageformat = messageformat.Replace("<!--StockiestName-->", StockistName);
                messageformat = messageformat.Replace("<!--ChqNo-->", ChqNo);
                messageformat = messageformat.Replace("<!--ChqAmount-->", Convert.ToString(ChqAmount));
                messageformat = messageformat.Replace("<!--InvNo-->", InvNo);
                messageformat = messageformat.Replace("<!--Date-->", DateTime.Today.Date.ToString("dd-MM-yyyy"));
                mailMessage.Subject = Subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = messageformat;
                mailClient.Send(mailMessage);
                bResult = true;
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailsForChqDeposit", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            finally
            {
                if (mailClient != null)
                    mailClient.Dispose();

                if (mailMessage != null)
                    mailMessage.Dispose();
            }

            return bResult;
        }
        #endregion

        #region Send Email For Alert for Internal Audit
        public bool SendEmailForInternalAudit(string ToEmail, string CCEmail, string BCCEmail, string BodyText, string Subject)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            // Create the mail message
            MailMessage mailMessage = null;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();
                if (!string.IsNullOrWhiteSpace(ToEmail))//Recipient Email
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(CCEmail))//Recipient Email cc
                {
                    string CCEmailId = CCEmail.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(CCEmail) && !string.IsNullOrEmpty(BCCEmail))
                {
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = BodyText;
                    mailClient.Send(mailMessage);
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailForInternalAudit", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return bResult;
        }
        #endregion

        #region Send Email To Stockist For LR Import
        public bool SendLRImportEmailToStockist(string ToEmail, string EmailCC, string BCCEmail, string Subject, string StockistName, string TransporterName, string LRNo, string MailFilePath)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            MailMessage mailMessage = null;
            string messageformat = string.Empty;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();
                if (!string.IsNullOrWhiteSpace(ToEmail))//Recipient Email
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(EmailCC))//Recipient Email cc
                {
                    string CCEmailId = EmailCC.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(EmailCC) && !string.IsNullOrEmpty(BCCEmail))
                {
                    messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                    messageformat = messageformat.Replace("<!--StockiestName-->", StockistName);
                    messageformat = messageformat.Replace("<!--DatedOn-->", DateTime.Today.Date.ToString("dd-MM-yyyy"));
                    messageformat = messageformat.Replace("<!--TransportName-->", TransporterName);
                    messageformat = messageformat.Replace("<!--LRNo-->", LRNo);
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = messageformat;
                    mailClient.Send(mailMessage);
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendLRImportEmailToStockist", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            return bResult;
        }
        #endregion

        #region Send Email To Cheque Summary of Previous Month/Week
        public bool SendEmailToChqSummaryForMonthlyOrWeekly(string ToEmail, string EmailCC, string BCCEmail, string Subject, string BodyText)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            MailMessage mailMessage = null;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();
                if (!string.IsNullOrWhiteSpace(ToEmail))//Recipient Email
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(EmailCC))//Recipient Email cc
                {
                    string CCEmailId = EmailCC.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(EmailCC) && !string.IsNullOrEmpty(BCCEmail))
                {
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = BodyText;
                    mailClient.Send(mailMessage);
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailToChqSummaryForMonthlyOrWeekly", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return bResult;
        }
        #endregion

        #region Send Alert To Internal Team For Chq Smmry
        //Send Alert To Internal Team For Chq Smmry
        public bool SendAlertToSalesTeamForChqSmmry(string ToEmail, string EmailCC, string BCCEmail, string Subject, string BodyText)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            MailMessage mailMessage = null;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();

                if (!string.IsNullOrWhiteSpace(ToEmail))
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(EmailCC))
                {
                    string CCEmailId = EmailCC.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(EmailCC) && !string.IsNullOrEmpty(BCCEmail))
                {
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = BodyText;
                    mailClient.Send(mailMessage);
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendAlertToInternalTeamForChqSmmry", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return bResult;
        }
        #endregion

        #region Send Email To Stockist For Consignment Received
        public bool SendEmailForConsignmentReceived(string ToEmail, string CCEmail, string BCCEmail, string Subject, string StockistName, string TransCourName, string LRNo, DateTime LRDate, int ClaimFormAvailable, string ClaimNo, string ClaimString, string WithoutClaim, string ClaimNote, string MailFilePath)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            MailMessage mailMessage = null;
            string messageformat = string.Empty;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();

                if (!string.IsNullOrWhiteSpace(ToEmail))
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(CCEmail))
                {
                    string CCEmailId = CCEmail.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(CCEmail) && !string.IsNullOrEmpty(BCCEmail))
                {
                    messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                    messageformat = messageformat.Replace("<!--StockiestName-->", StockistName);
                    messageformat = messageformat.Replace("<!--TransporterName-->", TransCourName);
                    messageformat = messageformat.Replace("<!--LRNo-->", LRNo);
                    messageformat = messageformat.Replace("<!--LRDate-->", LRDate.Date.ToString("dd-MM-yyyy"));
                    mailMessage.Subject = Subject;
                    if (ClaimNo != null && ClaimFormAvailable == 1)
                    {
                        messageformat = messageformat.Replace("<!--ClaimString-->", ClaimString);
                        messageformat = messageformat.Replace("<!--ClaimNo-->", ClaimNo);
                    }
                    if (ClaimFormAvailable == 0)
                    {
                        messageformat = messageformat.Replace("<!--WithoutClaim-->", WithoutClaim);
                        messageformat = messageformat.Replace("<!--ClaimNote-->", ClaimNote);
                    }
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = messageformat;
                    mailClient.Send(mailMessage);
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailForConsignmentRecieved", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return bResult;
        }
        #endregion

        #region Send Email To Stockiest For Missing Claim Form
        public bool SendEmailMissingClaimForm(string ToEmail, string EmailCC, string BCCEmail, string Subject, string StockistName, string TransCourName, string LRNumber, DateTime LRDate, string MailFilePath)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            MailMessage mailMessage = null;
            string messageformat = string.Empty;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();

                if (!string.IsNullOrWhiteSpace(ToEmail))
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(EmailCC))
                {
                    string CCEmailId = EmailCC.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(EmailCC) && !string.IsNullOrEmpty(BCCEmail))
                {
                    messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                    //Replace Notification Table
                    messageformat = messageformat.Replace("<!--StockiestName-->", StockistName);
                    messageformat = messageformat.Replace("<!--TransporterName-->", TransCourName);
                    messageformat = messageformat.Replace("<!--LRNo-->", LRNumber);
                    messageformat = messageformat.Replace("<!--LRDate-->", LRDate.Date.ToString("dd-MM-yyyy"));
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = messageformat;
                    mailClient.Send(mailMessage);
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailMissingClaimForm", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return bResult;
        }
        #endregion

        #region  Send Email For approval Update Alert
        public bool sendEmailForApproval(string Emailid, string EmailCC, string Subject, string BodyText)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            // Create the mail message
            MailMessage mailMessage = null;
            try
            {
                BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Send Email For approval Update Alert", "START", "");

                mailClient = new SmtpClient();
                mailMessage = new MailMessage();

                BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC + " Subject:  " + Subject + " BodyText:  " + BodyText, "Step 1", "");

                if (!string.IsNullOrWhiteSpace(Emailid)) // Recipient Email
                {
                    BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC + " Subject:  " + Subject + " BodyText:  " + BodyText, "Step 2", "");

                    string ToEmailId = Emailid.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "ToEmailId:  " + ToEmailId, "IF START", "");

                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }

                        BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "ToEmailId:  " + ToEmailId, "IF END", "");
                    }
                    else
                    {
                        BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "ToEmailId:  " + ToEmailId, "IF START", "");

                        mailMessage.To.Add(ToEmailId);

                        BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "ToEmailId:  " + ToEmailId, "IF END", "");
                    }
                }

                if (!string.IsNullOrWhiteSpace(EmailCC)) // Recipient Email cc
                {
                    BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC + " Subject:  " + Subject + " BodyText:  " + BodyText, "Step 3", "");

                    string CCEmailId = EmailCC.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "CCEmailId:  " + CCEmailId, "IF START", "");

                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }

                        BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "CCEmailId:  " + CCEmailId, "IF END", "");
                    }
                    else
                    {
                        BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "CCEmailId:  " + CCEmailId, "ELSE START", "");
                        mailMessage.CC.Add(CCEmailId);
                        BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "CCEmailId:  " + CCEmailId, "ELSE END", "");
                    }
                }

                BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC, "-----", "");

                if (!string.IsNullOrEmpty(Emailid) && !string.IsNullOrEmpty(EmailCC))
                {
                    BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC + " Subject:  " + Subject + " BodyText:  " + BodyText, "Step 4", "");

                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = BodyText;
                    mailClient.Send(mailMessage);

                    bResult = true;

                    BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC + " Subject:  " + Subject + " BodyText:  " + BodyText, "Step 5", "");
                }
                else
                {
                    BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC + " Subject:  " + Subject + " BodyText:  " + BodyText, "Step 6", "");

                    bResult = false;

                    BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC + " Subject:  " + Subject + " BodyText:  " + BodyText, "Step 7", "");
                }

                BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Send Email For approval Update Alert", "END", "");
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC + " Subject:  " + Subject + " BodyText:  " + BodyText, "Step 8", "");

                BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            BusinessCont.SaveLog(0, 0, 0, "sendEmailForApproval", "Emailid:  " + Emailid + "  EmailCC:  " + EmailCC + " Subject:  " + Subject + " BodyText:  " + BodyText, "Step 9", "");

            return bResult;
        }
        #endregion

        #region Send Email When Import CN(Creadit Note)
        public bool SendEmailImportCN(string ToEmail, string CCEmail, string BCCEmail, string StockistName, string CrDrNoteNo, string ClaimNo, DateTime ClaimDate, string Subject, string MailFilePath)
        {
            bool bResult = false;
            SmtpClient mailClient = null;
            // Create the mail message
            MailMessage mailMessage = null;
            string messageformat = string.Empty;
            try
            {
                mailClient = new SmtpClient();
                mailMessage = new MailMessage();

                if (!string.IsNullOrWhiteSpace(ToEmail))//Recipient Email
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(CCEmail))//Recipient Email cc
                {
                    string CCEmailId = CCEmail.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(BCCEmail)) // For BCC Email 
                {
                    string BCCEmailId = BCCEmail.Trim();
                    if (BCCEmailId.Contains(";"))
                    {
                        string[] emails = BCCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.Bcc.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.Bcc.Add(BCCEmailId.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(CCEmail) && !string.IsNullOrEmpty(BCCEmail))
                {
                    messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                    // Replace Notification Table
                    messageformat = messageformat.Replace("<!--StockiestName-->", StockistName);
                    messageformat = messageformat.Replace("<!--CrDrNoteNo-->", CrDrNoteNo);
                    messageformat = messageformat.Replace("<!--ClaimNo-->", ClaimNo);
                    messageformat = messageformat.Replace("<!--Date-->", DateTime.Today.Date.ToString("dd-MM-yyyy"));
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = messageformat;
                    mailClient.Send(mailMessage);
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailImportCN", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return bResult;
        }
        #endregion

        #region Send Email To Stockist For Claim Approvals
        public static bool SendEmailForClaimApprove(string ToEmail, string EmailCC, string Subject, string ClaimNo, DateTime ApproveClaimDate, string MailFilePath)
        {
            var date = ApproveClaimDate.Date.ToString("dd-MM-yyyy");
            bool bResult = false;
            SmtpClient mailClient = null;
            MailMessage mailMessage = null;
            try
            {

                mailClient = new SmtpClient();
                mailMessage = new MailMessage();
                if (!string.IsNullOrWhiteSpace(ToEmail))
                {
                    string ToEmailId = ToEmail.Trim();
                    if (ToEmailId.Contains(";"))
                    {
                        string[] emails = ToEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.To.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(ToEmailId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(EmailCC))
                {
                    string CCEmailId = EmailCC.Trim();
                    if (CCEmailId.Contains(";"))
                    {
                        string[] emails = CCEmailId.Trim().Split(';');
                        foreach (string email in emails)
                        {
                            mailMessage.CC.Add(email.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(CCEmailId);
                    }
                }
                if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(EmailCC))
                {
                    string messageformat = File.OpenText(MailFilePath).ReadToEnd().ToString();
                    messageformat = messageformat.Replace("<!--ClaimNo-->", ClaimNo);
                    messageformat = messageformat.Replace("<!--ApproveClaimDate-->", Convert.ToString(date));
                    messageformat = messageformat.Replace("<!--DatedOn-->", DateTime.Today.Date.ToString("dd-MM-yyyy"));
                    mailMessage.Subject = Subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = messageformat;
                    mailClient.Send(mailMessage);

                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailForClaimApprove", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            return bResult;
        }
        #endregion
    }
}
