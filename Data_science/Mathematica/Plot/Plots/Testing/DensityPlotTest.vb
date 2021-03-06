﻿#Region "Microsoft.VisualBasic::5d01c3f1ddab81a59fcdab1691aa2e17, ..\sciBASIC#\Data_science\Mathematica\Plot\Plots\Testing\DensityPlotTest.vb"

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

Imports System.Drawing
Imports Microsoft.VisualBasic.Data.ChartPlots.Statistics.Heatmap
Imports Microsoft.VisualBasic.Data.csv.IO
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D.Colors

Module DensityPlotTest

    Sub Main()
        Dim data = DataSet.LoadDataSet("D:\OneDrive\2017-8-31\3. DEPs\Time_series\T4vsT3.csv")
        Dim points = data.Select(Function(x)
                                     Return New PointF(x!log2FC, -Math.Log10(x("p.value")))
                                 End Function).ToArray

        Call DensityPlot.Plot(
            points,
            ptSize:=15,
            levels:=65,
            schema:=ColorMap.PatternJet).Save("./test.png")
    End Sub
End Module

