(function () {
  const baseController = "/Home";
  const scriptBaseConfig = {
    Url: {
      validateToken: baseController + "/ValidateToken",
      register: baseController + "/Register",
      login: baseController + "/Login",
      logout: baseController + "/Logout",
      roles: baseController + "/GetRoles",
      users: baseController + "/GetUsers",
      user: baseController + "/GetUser",
      addUser: baseController + "/AddUser",
      updateUser: baseController + "/UpdateUser",
      deleteUser: baseController + "/DeleteUser",
    },
    Claims: {
      Name: null,
      Role: null
    },
    Roles: {
      Admin: "Admin",
      RegularUser: "RegularUser"
    },
    Holder: {
      LoginForm: {
        username: "#txtUsername",
        password: "#txtPassword"
      },
      RegisterForm: {
        role: "#ddRole",
        email: "#txtRegisterEmail",
        username: "#txtRegisterUsername",
        password: "#txtRegisterPassword",
        phoneno: "#txtRegisterPhoneNumber"
      },
      UserForm: {
        userId : "",
        role: "#ddManageRole",
        email: "#txtManageEmail",
        username: "#txtManageUsername",
        password: "#txtManagePassword",
        phoneno: "#txtManagePhoneNumber"
      },
      TableBody: {
        user: "#tbody-users",
      },
      Modals: {
        manageUserModal: "#manageUserModal"
      }
    },
    SendRequest: function (oData) {
      $.ajax({
        url: oData.url,
        type: oData.type,
        contentType: oData.contentType,
        dataType: oData.dataType,
        data: oData.data,
        success: oData.success,
        error: oData.error,
        beforeSend: oData.beforeSend,
        complete: oData.complete,
        async: oData.async
      });
    }
  }
  var scriptManager = {
    Initialize: function () {
      $(document).ready(function () {
        scriptManager.EventHandlers();
        scriptManager.ClientRequest.ValidateToken(true);
      });
    },
    EventHandlers: function () {
      $(document).on("click", "#btnLogin", function (event) {
        if ($(this).text() == "Submit") {
          scriptManager.ClientRequest.Login();
        } else {
          scriptManager.ClientRequest.Logout();
        }
      })
      $(document).on("click", "#btnRegister", function (event) {
        scriptManager.ClientRequest.Register();
      })
      $(document).on("click", "#btnAddUser", function (event) {
        scriptBaseConfig.Holder.UserForm.userId = null;
        scriptManager.UIHelper.ClearUserForm();
        var userModal = scriptBaseConfig.Holder.Modals.manageUserModal;
        $(userModal).find(".modal-title").text("Add User");
        $(userModal).find("#btnSaveUser").text("Save");
        $(userModal).modal("show")
      })
      $(document).on("click", ".edit-details", function (event) {
        scriptBaseConfig.Holder.UserForm.userId = $(this).attr("userId");
        scriptManager.ClientRequest.GetUser();
      })
      $(document).on("click", ".delete-details", function (event) {
        scriptBaseConfig.Holder.UserForm.userId = $(this).attr("userId");
        scriptManager.ClientRequest.DeleteUser();
      })
      $(document).on("click", "#btnSaveUser", function (event) {
        if (scriptBaseConfig.Holder.UserForm.userId != null) {
          scriptManager.ClientRequest.UpdateUser();
        } if (scriptBaseConfig.Holder.UserForm.userId == null) {
          scriptManager.ClientRequest.AddUser();
        }
      })
    },
    UIHelper: {
      ValidateForm: function (IsValid, isRefreshPage, oResult) {
        var claims = scriptBaseConfig.Claims;
        var defaultRole = scriptBaseConfig.Roles;
        if (IsValid && isRefreshPage) {
          claims.Name = oResult.userName
          claims.Role = oResult.roleName
          $("#btnLogin").text("Logout")
          $(".login-title").text("Current User")
          $(".current-user").text(claims.Name);
          $(".current-user-role").text(claims.Role);
          $(".login-details").removeClass("d-none")
          $(".login-form").addClass("d-none")
          $(".register-form").addClass("d-none")
          if (claims.Role == defaultRole.Admin) {
            $(".user-container").removeClass("d-none")
            $(".inventory-container").addClass("d-none")
            scriptManager.ClientRequest.GetRoles();
            scriptManager.ClientRequest.GetUsers();
          } else if (claims.Role == defaultRole.RegularUser) {
            $(".user-container").addClass("d-none")
            $(".inventory-container").removeClass("d-none")
          } 
        } else {
          claims.Name = null;
          claims.Role = null
          $("#btnLogin").text("Submit")
          $(".login-title").text("Login")
          $(".current-user").text("");
          $(".current-user-role").text("");
          $(".login-details").addClass("d-none")
          $(".login-form").removeClass("d-none")
          $(".register-form").removeClass("d-none")
          $(".user-container").addClass("d-none")
          $(".inventory-container").addClass("d-none")
        }
      },
      ClearRegistrationForm: function () {
        var registerForm = scriptBaseConfig.Holder.RegisterForm;
        $(registerForm.role).val("");
        $(registerForm.username).val("");
        $(registerForm.password).val("");
        $(registerForm.email).val("");
        $(registerForm.phoneno).val("");
      },
      ClearUserForm: function () {
        var userForm = scriptBaseConfig.Holder.UserForm;
        $(userForm.username).val("");
        $(userForm.password).val("");
        $(userForm.email).val("");
        $(userForm.phoneno).val("");
        $(userForm.password).parent().removeClass("d-none");
        $(userForm.role).parent().removeClass("d-none");
      }
    },
    ClientRequest: {
      ValidateToken: function (isRefreshPage) {
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.validateToken,
          type: "POST",
          dataType: "json",
          beforeSend: function () {
          },
          success: function (oResult) {
            scriptManager.UIHelper.ValidateForm(true, isRefreshPage, oResult);
          },
          complete: function () {

          },
          error: function (xhr) {
            scriptManager.UIHelper.ValidateForm(false, isRefreshPage, null);
          }
        });
      },
      Register: function () {
        var registerForm = scriptBaseConfig.Holder.RegisterForm;
        var payload = {
          RoleId: $(registerForm.role).val(),
          Username: $(registerForm.username).val(),
          Password: $(registerForm.password).val(),
          Email: $(registerForm.email).val(),
          PhoneNumber: $(registerForm.phoneno).val()
        };
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.register,
          type: "POST",
          dataType: "json",
          data: payload,
          beforeSend: function () {
          },
          success: function (oResult) {
            alert(oResult)
          },
          complete: function () {
            scriptManager.UIHelper.ClearRegistrationForm();
          },
          error: function (xhr) {
            alert(xhr.responseText)
          }
        });
      },
      Login: function () {
        var payload = {
          Username: $(scriptBaseConfig.Holder.LoginForm.username).val(),
          Password: $(scriptBaseConfig.Holder.LoginForm.password).val()
        };
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.login,
          type: "POST",
          dataType: "json",
          data: payload,
          beforeSend: function () {
          },
          success: function (oResult) {
            alert(oResult)
          },
          complete: function () {
            scriptManager.ClientRequest.ValidateToken(true);
            scriptManager.UIHelper.ClearRegistrationForm();
            scriptManager.UIHelper.ClearUserForm();
          },
          error: function (xhr) {
            alert(xhr.responseText)
          }
        });
      },
      Logout: function () {
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.logout,
          type: "POST",
          dataType: "json",
          beforeSend: function () {
            $(scriptBaseConfig.Holder.LoginForm.username).val(""),
            $(scriptBaseConfig.Holder.LoginForm.password).val("")
          },
          success: function (oResult) {
            alert(oResult)
          },
          complete: function () {
            scriptManager.ClientRequest.ValidateToken(true);
          },
          error: function (xhr) {
            alert(xhr.responseText)
          }
        });
      },
      GetRoles: function () {
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.roles,
          type: "GET",
          dataType: "json",
          beforeSend: function () {
            $(scriptBaseConfig.Holder.RegisterForm.role).empty();
            $(scriptBaseConfig.Holder.UserForm.role).empty();
          },
          success: function (oResult) {
            oResult.forEach(function (item) {
              $(scriptBaseConfig.Holder.RegisterForm.role).append(`<option value="${item.id}">${item.name}</option>`)
              $(scriptBaseConfig.Holder.UserForm.role).append(`<option value="${item.id}">${item.name}</option>`)
            });
          },
          error: function (xhr) {
            scriptManager.ClientRequest.ValidateToken(false);
          }
        });
      },
      GetUsers: function () {
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.users,
          type: "GET",
          dataType: "json",
          beforeSend: function () {
            $(scriptBaseConfig.Holder.TableBody.user).empty()
          },
          success: function (oResult) {
            oResult.forEach(function (item) {
              $(scriptBaseConfig.Holder.TableBody.user).append(`
              <tr>
                <td>${item.userName}</td>
                <td>${item.email}</td>
                <td>${item.roleName}</td>
                <td>
                  <span userId="${item.id}" class="table-action edit-details">Edit</span>
                  <span userId="${item.id}" class="table-action delete-details">Delete</span>
                </td>
              </tr>`)
            });
          },
          error: function (xhr) {
            scriptManager.ClientRequest.ValidateToken(false);
          }
        });
      },
      GetUser: function () {
        var userModal = scriptBaseConfig.Holder.Modals.manageUserModal;
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.user,
          type: "GET",
          dataType: "json",
          data: { UserId: scriptBaseConfig.Holder.UserForm.userId },
          beforeSend: function () {
            $(userModal).find(".modal-title").text("Update User");
            $(userModal).find("#btnSaveUser").text("Save Changes");
            scriptManager.UIHelper.ClearUserForm();
          },
          success: function (oResult) {
            var userForm = scriptBaseConfig.Holder.UserForm;
            $(userForm.role).parent().addClass("d-none");
            $(userForm.password).parent().addClass("d-none");
            $(userForm.username).val(oResult.userName);
            $(userForm.email).val(oResult.email);
            $(userForm.phoneno).val(oResult.phoneNumber);
            $(scriptBaseConfig.Holder.Modals.manageUserModal).modal("show")
          },
          complete: function () {
          },
          error: function (xhr) {
            scriptManager.ClientRequest.ValidateToken(false);
          }
        });
      },
      AddUser: function () {
        var userForm = scriptBaseConfig.Holder.UserForm;
        var payload = {
          RoleId: $(userForm.role).val(),
          Username: $(userForm.username).val(),
          Password: $(userForm.password).val(),
          Email: $(userForm.email).val(),
          PhoneNumber: $(userForm.phoneno).val()
        };
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.addUser,
          type: "POST",
          dataType: "json",
          data: payload,
          beforeSend: function () {
          },
          success: function (oResult) {
            alert(oResult)
            scriptManager.ClientRequest.GetUsers();
            $(scriptBaseConfig.Holder.Modals.manageUserModal).modal("hide")
          },
          complete: function () {
         
          },
          error: function (xhr) {
            alert(xhr.responseText)
            scriptManager.ClientRequest.ValidateToken(false);
          }
        });
      },
      UpdateUser: function () {
        var userForm = scriptBaseConfig.Holder.UserForm;
        var payload = {
          UserId: userForm.userId,
          Username: $(userForm.username).val(),
          Email: $(userForm.email).val(),
          PhoneNumber: $(userForm.phoneno).val()
        };
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.updateUser,
          type: "POST",
          dataType: "json",
          data: payload,
          beforeSend: function () {
          },
          success: function (oResult) {
            alert(oResult)
            scriptManager.ClientRequest.GetUsers();
            $(scriptBaseConfig.Holder.Modals.manageUserModal).modal("hide")
          },
          complete: function () {
     
          },
          error: function (xhr) {
            alert(xhr.responseText)
            scriptManager.ClientRequest.ValidateToken(false);
          }
        });
      },
      DeleteUser: function () {
        scriptBaseConfig.SendRequest({
          url: scriptBaseConfig.Url.deleteUser,
          type: "POST",
          dataType: "json",
          data: { UserId: scriptBaseConfig.Holder.UserForm.userId },
          beforeSend: function () {
          },
          success: function (oResult) {
            alert(oResult)
            scriptManager.ClientRequest.GetUsers();
          },
          complete: function () {

          },
          error: function (xhr) {
            alert(xhr.responseText)
            scriptManager.ClientRequest.ValidateToken(false);
          }
        });
      }
    }
  }
  scriptManager.Initialize();
})(jQuery);