# codechallenge

# User Mangement


1- Create New MVC Project with Unit Test Project
![alt tag](https://image.prntscr.com/image/4lrfPIPlQwmPEOtYZZMUKg.png)
![alt tag](https://image.prntscr.com/image/WwnM0XsFQpaQW1hPGctM3g.png)
![alt tag](https://image.prntscr.com/image/JsfZAFscSvaqzh6e5Kqnxg.png)


2- Install "jquery.unobtrusive-ajax" using NuGet Package Manager, to enabe ajax form

3- create database "CustomerDB / UserDB " with one Table
````sql
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[BirthDate] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[ModifiedBy] [nvarchar](250) NULL,
	[isDeleted] [bit] NULL CONSTRAINT [DF_Customers_isDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
````

4- Create Data entity model (generate edmx file from DB)

5- In HomeController, code is written to get customer/User data, save and delete customer with async task jsonresult.
````sql
public ActionResult getCustomers()
        {
            try
            {
                using (CustomerDBEntities db = new CustomerDBEntities())
                {
                    var customers = db.Customers.Where(w => w.isDeleted != true).ToList().Select(s => new
                    {
                        s.CustomerID,
                        s.FirstName,
                        s.LastName,
                        s.Email,
                        BirthDate = s.BirthDate.HasValue ? s.BirthDate.Value.ToShortDateString() : "",
                        FullName = s.FirstName + " " + s.LastName,
                        CustomerCode = (s.FirstName + "" + s.LastName).Replace(" ", "").ToLower() + (s.BirthDate.HasValue ? s.BirthDate.Value.ToString("yyyyMMdd") : "")
                    }).OrderBy(o => o.LastName).ToList();

                    return Json(new { customers }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception er)
            {
                return Json(new { status="error", msg = er.Message }, JsonRequestBehavior.AllowGet);
            }
        }
````
Output 
![alt tag](https://github.com/ManojLingala/Ansible-Playbook/blob/master/Images/Output.png)

# Unit Test
Unit Test Project

7 Test cases were written to cover all the scenario's from controller to Model.

![alt tag](https://github.com/ManojLingala/Ansible-Playbook/blob/master/Images/TestCasesExecution.png)

# Build a CI/CD (Continuous Integration/Continuous Deployment) 

1- Add solution to source control by right click on solution then select Add this project to source control (You should have GitHub Account) 

2- Go to Tools, choose Extensions and Updates.

3- From the prompted window, select Continuous Delivery Tools for Visual Studio and click Enable.

4- Go in the Solution Explorer, right click on your web-based project.

5- Click on the new context menu Configure Continuous Delivery. and follow settings and instructions to perform this process

7- After your project is created into Team Services account, in the output window you can see that your CI/CD are setting up for your project.

8- Copy Build Definition link, and open it in your browser

9- It is shown an output of your build server which is running your build automatically.

10- Click on the Edit build definition, then Test your CI and CD, and you can manage (Continuous Integration/Continuous Deployment) Pipeline

![alt tag](https://github.com/ManojLingala/Ansible-Playbook/blob/master/Images/CI.png)

![alt tag](https://github.com/ManojLingala/Ansible-Playbook/blob/master/Images/CICD.png)










