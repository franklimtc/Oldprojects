option explicit

main

sub main
    
    Dim objWMISvc
    Dim colItems
    Dim objItem
    Dim wshShell

    Dim objPrinter
    Dim colInstalledPrinters

    set objwmisvc = getobject( _
        "winmgmts:" & "{impersonationlevel=impersonate}!\\.\root\cimv2")

    set colitems = objwmisvc.execquery( "select * from win32_computersystem")
    for each objitem in colitems
                
        Set colInstalledPrinters =  objwmisvc.ExecQuery _
            ("Select * from Win32_Printer where Network = false")

        For Each objPrinter in colInstalledPrinters
            if objPrinter.KeepPrintedJobs = false Then 
                objPrinter.KeepPrintedJobs = true
                objPrinter.Put_
            end if
        Next
    next
end sub