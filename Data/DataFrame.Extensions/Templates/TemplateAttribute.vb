﻿#Region "Microsoft.VisualBasic::7cd8507a7caf663ef5ae16e46d24bbd7, ..\sciBASIC#\Data\DataFrame.Extensions\Templates\TemplateAttribute.vb"

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

''' <summary>
''' 用于标记当前的类型为csv输入文件的模板
''' </summary>
<AttributeUsage(AttributeTargets.Class Or AttributeTargets.Struct,
                AllowMultiple:=False,
                Inherited:=True)>
Public Class TemplateAttribute : Inherits Attribute

    Public ReadOnly Property Category As String
    Public ReadOnly Property AliasName As String

    ''' <summary>
    ''' 目标类型的模板会以csv文件的形式被保存，并且文件名为类型的名称
    ''' </summary>
    ''' <param name="category">分类信息，这个应该是一个文件夹的名称</param>
    Sub New(category$, Optional alias$ = Nothing)
        Me.Category = category
        Me.AliasName = [alias]
    End Sub

    Public Overrides Function ToString() As String
        Return Category
    End Function
End Class

