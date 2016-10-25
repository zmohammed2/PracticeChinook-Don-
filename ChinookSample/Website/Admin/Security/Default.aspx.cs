using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Security Namespace
using ChinookSystem.Security;
#endregion

public partial class Admin_Security_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void RefreshAll(object sender, EventArgs e)
    {
        DataBind();
    }

    protected void UnregisteredUsersGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //position the gridview to the selectdIndex(row) that caused
        //the postback
        UnregisteredUsersGridView.SelectedIndex = e.NewSelectedIndex;

        //set up a avariable that will be the physical pointer to the selected row
        GridViewRow agvrow = UnregisteredUsersGridView.SelectedRow;

        //you can alreay checka pointer to see if something has been obtained
        if (agvrow != null)
        {
            //access information contained in a text box on the gv row
            //use the method.FindControl("controlidname") as an controlType
            //once you have a pointer to the control, you can access the 
            //data content of the control using the Control's access method
            string assignedUserName = " ";
            TextBox inputControl = agvrow.FindControl("AssignedUserName") as TextBox;
            if (inputControl != null)
            {
                assignedUserName = inputControl.Text;
            };
            string assignedemail = (agvrow.FindControl("AssignedEmail") as TextBox).Text;

            //create the UnregisteredUser instance
            //during creation, I will pass to it the needed data to load the instance attributes

            //accessing boundfields on a GridView Row uses .Cells[index].Text
            //index represents the column of the Grid
            //Coplumns are index(starting at 0)
            UnRegisteredUserProfile user = new UnRegisteredUserProfile()
            {
                UserId = int.Parse(UnregisteredUsersGridView.SelectedDataKey.Value.ToString()),
                UserType = (UnRegisteredUserType)Enum.Parse(typeof(UnRegisteredUserType), agvrow.Cells[1].Text),
                FirstName = agvrow.Cells[2].Text,
                LastName = agvrow.Cells[3].Text,
                UserName = assignedUserName,
                Email = assignedemail
            };

            //register the user viq the chinnok.UserManager controller
            UserManager sysmgr = new UserManager();
            sysmgr.RegisterUser(user);

            //assume successful creation os a user
            //referesh the form
            DataBind();
        }
    }
}

