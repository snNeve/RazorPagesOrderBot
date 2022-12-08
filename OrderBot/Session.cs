using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, BOOK, SERVICE, Appointment, DATETIME, ENDMESSAGE
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to Dental Chatbot!");
                    aMessages.Add("Do you want to 'Book' or 'Reschedule' an appointment?");
                    this.nCur = State.BOOK;
                    break;
                case State.BOOK:
                    this.oOrder.Appointment = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("What service would you like to  " + this.oOrder.Appointment +  " 'regular cleaning', 'whitening' or 'checkup'? ");
                    //aMessages.Add("Which date would you like the appointment  " + this.oOrder.Size + " ? ");
                    this.nCur = State.SERVICE;
                    break;
                case State.SERVICE:
                    string sSERVICE = this.oOrder.Service = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Which date would you like the appointment  " + this.oOrder.Appointment + "ed? ");
                    this.nCur = State.DATETIME;
                    break;
                case State.DATETIME:
                    this.oOrder.Datetime = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Appointment Booked on  " + this.oOrder.Datetime);
                    aMessages.Add("See you on " + this.oOrder.Datetime + " . Bye");
                    break;
            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
