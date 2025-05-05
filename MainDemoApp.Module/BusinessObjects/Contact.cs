using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MainDemoApp.Module
{
    [DefaultClassOptions]
    public class Contact : Person
    {
        public Contact(Session session) : base(session) { }
        private string webPageAddress;
        public string WebPageAddress
        {
            get { return webPageAddress; }
            set { SetPropertyValue("WebPageAddress", ref webPageAddress, value); }
        }
        private string nickName;
        public string NickName
        {
            get { return nickName; }
            set { SetPropertyValue("NickName", ref nickName, value); }
        }
        private string spouseName;
        public string SpouseName
        {
            get { return spouseName; }
            set { SetPropertyValue("SpouseName", ref spouseName, value); }
        }
        private TitleOfCourtesy titleOfCourtesy;
        public TitleOfCourtesy TitleOfCourtesy
        {
            get { return titleOfCourtesy; }
            set { SetPropertyValue("TitleOfCourtesy", ref titleOfCourtesy, value); }
        }
        private DateTime anniversary;
        public DateTime Anniversary
        {
            get { return anniversary; }
            set { SetPropertyValue("Anniversary", ref anniversary, value); }
        }
        private string notes;
        [Size(4096)]
        public string Notes
        {
            get { return notes; }
            set { SetPropertyValue("Notes", ref notes, value); }
        }

        private Department department;
        [Association("Department-Contacts")] 
        public Department Department
        {
            get { return department; }
            set { SetPropertyValue("Department", ref department, value); }
        }

        private Position position;
        public Position Position
        {
            get { return position; }
            set { SetPropertyValue("Position", ref position, value); }
        }

        private Contact manager;
        [DataSourceProperty("Department.Contacts")]

        public Contact Manager
        {
            get { return manager; }
            set { SetPropertyValue("Manager", ref manager, value); }
        }

        [Association("Contact-DemoTask")]
        public XPCollection<DemoTask> Tasks
        {
            get
            {
                return GetCollection<DemoTask>("Tasks");
            }
        }


    }
    public enum TitleOfCourtesy { Dr, Miss, Mr, Mrs, Ms };

    [DefaultClassOptions]
    [System.ComponentModel.DefaultProperty("Title")]
    public class Department : BaseObject
    {
        public Department(Session session) : base(session) { }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetPropertyValue("Title", ref title, value); }
        }
        private string office;
        public string Office
        {
            get { return office; }
            set { SetPropertyValue("Office", ref office, value); }
        }

        [Association("Department-Contacts")]
        public XPCollection<Contact> Contacts
        {
            get
            {
                return GetCollection<Contact>("Contacts");
            }
        }
    }
    [DefaultClassOptions]
    [System.ComponentModel.DefaultProperty("Title")]
    public class Position : BaseObject
    {
        public Position(Session session) : base(session) { }
        private string title;
        [RuleRequiredField(DefaultContexts.Save)]
        public string Title
        {
            get { return title; }
            set { SetPropertyValue("Title", ref title, value); }
        }
    }

    [DefaultClassOptions]
    [ModelDefault("Caption", "Task")]
    public class DemoTask : Task
    {
        public DemoTask(Session session) : base(session) { }
        private Priority priority;
        public Priority Priority
        {
            get { return priority; }
            set
            {
                SetPropertyValue("Priority", ref priority, value);
            }
        }

              /*public override void AfterConstruction()
                {
                    base.AfterConstruction();
                    Priority = Priority.Normal;
                }*/

        [Action(ToolTip = "Postpone the task to the next day")]
        public void Postpone()
        {
            if (DueDate == DateTime.MinValue)
            {
                DueDate = DateTime.Now;
            }
            DueDate = DueDate + TimeSpan.FromDays(1);
        }

        [Association("Contact-DemoTask")]
        public XPCollection<Contact> Contacts
        {
            get
            {
                return GetCollection<Contact>("Contacts");
            }
        }
    }
    public enum Priority
    {
        [ImageName("State_Priority_Low")]
        Low = 0,
        [ImageName("State_Priority_Normal")]
        Normal = 1,
        [ImageName("State_Priority_High")]
        High = 2
    }
}
