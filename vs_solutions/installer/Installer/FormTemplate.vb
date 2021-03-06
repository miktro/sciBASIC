﻿#Region "Microsoft.VisualBasic::76281fef1a506a679b4a1336667845ba, ..\sciBASIC#\vs_solutions\installer\Installer\FormTemplate.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
    '       xie (genetics@smrucc.org)
    ' 
    ' Copyright (c) 2016 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.

#End Region

Public Class FormTemplate

    Public Shared ReadOnly TemplateColor As Color = Color.FromArgb(0, 99, 177)

    Protected Overridable Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click

    End Sub

    Public Sub Highlight(label As Label)
        label.BackColor = Color.FromArgb(34, 118, 173)
        label.ForeColor = Color.White
    End Sub
End Class

