<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="JobPortalWebApplication.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <title></title>
</head>
<body style="background-color:aliceblue">
    <form id="form1" runat="server" >
        <div>
             <nav class="navbar navbar-expand-lg" style="background-color:darkslateblue">
            <%--  <nav class="navbar navbar-expand-lg navbar-dark bg-dark" >--%>
    <div class="container-fluid">
      <h3 class="navbar-brand" href="#" style="color: white;">BB_JobPortal</h3>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="#" style="color: white;">Home</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#" style="color: white;">Jobs</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#" style="color: white;">About</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#" style="color: white;">Registration</a>
          </li>
        </ul>
       <%-- <form class="d-flex">
          <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
          <button class="btn btn-outline-success" type="submit">Search</button>
        </form>--%>
      </div>
    </div>
  </nav>
        </div>   
     <div class="container">
            <h3 style="text-align:center; margin-top:180px; font-palette:light">
                   Welcome to JobPortal!!!!
            </h3>

                  
              
          

     <div class="d-flex justify-content-center align-items-center" style="height:20vh;">
    <div class="row">
        <div class="col">
            <label for="lblDropDownList1">Select User</label> <!-- Added label for better accessibility -->
         
           

        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Text="Select User Type" Value="" />
              <asp:ListItem Text="Admin" Value="Admin/AdminLogin.aspx" />
             <asp:ListItem Text="Candidate" Value="Candidate/CandidateRegistration.aspx" />
        <asp:ListItem Text="Employer" Value="Employer/EmployerRegistration.aspx" />
        <asp:ListItem Text="Trainer" Value="Trainer/TrainerReg.aspx" />
        </asp:DropDownList>
                </div>
                 </div>
             </div> 

      </div>
            
        
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
   
</body>
</html>
