;#DropDB MainDemoAppEasyTest

#Application MainDemoApp.Win
#Application MainDemoApp.Web

*FillForm
 User Name = Admin
*Action Log In

#IfDef MainDemoApp.Win

*Action About...
 
*Action OK

#EndIf

#IfDef MainDemoApp.Web
*Action Navigation(My Details)
#EndIf
