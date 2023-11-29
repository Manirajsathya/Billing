 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BILLING.aspx.cs" Inherits="WebApplication_BILLING.BILLING" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vegetables Billing</title>
    <link rel="icon" href="carrot.png"  />
    <link rel="stylesheet" href="StyleSheet1.css" />
</head>
<body style="background-color:gainsboro">
    <form id="form1" runat="server">
        
        <div class="header">
            <h1 style="padding-bottom:10px">  Fresh Vegetables </h1>
            <img src="vegetables-all.png" width="500 px" height="200px" />
        </div>
        <center> 
        <div class="input"> 

            <table>
                <tr>     <td><asp:Label runat="server" ID="IdVegetableLabel" Text="VEGETABLE"></asp:Label> </td>               
                       <td> <asp:DropDownList runat="server" ID="IdVegDrop"></asp:DropDownList></td>
                    <td><asp:Label runat="server" ID="IdWeightLabel" Text="WEIGHT"></asp:Label> </td>
                        
                        <td><asp:TextBox runat="server" ID="IdWeightTextBox"></asp:TextBox></td>
                   
                    </tr>
                     <tr>
                         <td></td>
                    <td><asp:Button runat="server" ID="IdAddButton" Text="ADD" OnClick="IdAddButton_Click" /> </td>                    
                         <td><asp:Button runat="server" ID="Idclear" Text="Clear" OnClick="Idclear_Click" /> </td>
            </table>
            </div>
            </center>           
    
            
                 <div class="left"> 

            <asp:GridView runat="server" ID="GridView2" AutoGenerateColumns="false" OnRowUpdating="GridView2_RowUpdating" OnRowEditing="GridView2_RowEditing" OnRowDeleting="GridView2_RowDeleting1" ShowFooter="true" > 
                <Columns>
                    <asp:TemplateField HeaderText="Id">
                        <ItemTemplate> 
                            <asp:TextBox runat="server" ID="IdTextBox" Text='<%# Bind("Id") %>'  Width="20px" ></asp:TextBox>  
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vegetable" >
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="VegText" Text='<%# Bind("Vegetable") %>' Width="50px"  ></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="VegLabel" Text='<%# Bind ("Vegetable") %>' Width="50px" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Weight">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="WeightTextBox" Text='<%#Bind ("Weight") %>' Width="50px" ></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="WeightLabel" Text='<%# Bind("Weight")%>' Width="50px" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Price/Kg"  >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="PriceKgLabel" Text='<%# Bind("[Price/Kg]")%>' Width="50px"  ></asp:Label>
                        </ItemTemplate>
                        
                            
                    </asp:TemplateField>
                     
                    <asp:TemplateField HeaderText="Total" >
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="TotalTextBox" Text='<%#Bind ("Total") %>' ></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="TotalLabel" Text='<%# Bind("Total")%>'  ></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                <asp:Label ID="TotalLabel" runat="server" Text="Total" ></asp:Label>
                            
            </FooterTemplate>
                        

                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Action">

                        <ItemTemplate>
                            <asp:Button runat="server" ID="IdBUtton1" Text="Edit" CommandName="Edit"  />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button runat="server" ID="IdBUtton2" Text="Update" CommandName="Update"  />
                            <asp:Button runat="server" ID="IdBUtton3" Text="Delete" CommandName="Delete"  />
                        </EditItemTemplate>
                    </asp:TemplateField>


                </Columns> 
                </asp:GridView>                      
                     </div>               
                     <div class="right" > 
              <asp:GridView runat="server" ID="GridView1" BackColor="WhiteSmoke" RowStyle-BackColor="Window"   ></asp:GridView>
                </div>
             
            
    </form>
</body>
</html>
