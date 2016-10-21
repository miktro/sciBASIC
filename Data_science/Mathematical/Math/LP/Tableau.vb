Imports System
Imports Microsoft.VisualBasic.Language.Java

Namespace LP


    Public Class Tableau

        Public Const EPSILON As Double = 0.000001 ' acceptable error

        Public Sub New(ByVal matrix As Double()())
            Me.Matrix = matrix
        End Sub

        Public ReadOnly Property Matrix As Double()()

        ''' <summary>
        ''' Return the index of the left-most negative variable in the objective function * </summary>
        Public Overridable ReadOnly Property PivotColumn As Integer
            Get
                Dim objective As Double() = Matrix(0)
                For i As Integer = 0 To objective.Length - 1
                    If objective(i) < 0 Then Return i
                Next i
                Return -1
            End Get
        End Property

        ''' <summary>
        ''' Finds the index of the pivot row (leaving basic variable) by applying
        ''' the minimum ratio test. *
        ''' </summary>
        Public Overridable Function getPivotRow(ByVal pivotColumnIndex As Integer) As Integer
            Dim minValue As Double = Double.MaxValue
            Dim minIndex As Integer = 0
            Dim rhsColumn As Double() = getColumn(Matrix(0).Length - 1)
            Dim ___pivotColumn As Double() = getColumn(pivotColumnIndex)
            For i As Integer = 1 To rhsColumn.Length - 1
                Dim rhs As Double = rhsColumn(i)
                Dim pivot As Double = ___pivotColumn(i)
                If Math.Abs(pivot - 0.0) > EPSILON Then
                    Dim ratio As Double = rhs / pivot

                    'Bland's Rule: in case of a tie, pick up the row with a lower index
                    If Math.Abs(ratio - minValue) < EPSILON Then
                        Continue For
                    ElseIf ratio < minValue Then
                        minValue = ratio
                        minIndex = i
                    End If
                End If
            Next i
            Return minIndex
        End Function

        ''' <summary>
        ''' Returns the nth column of the matrix * </summary>
        Public Overridable Function getColumn(ByVal columnIndex As Integer) As Double()
            'TODO: Precondition
            Dim ___column As Double() = New Double(Matrix.Length - 1) {}
            For i As Integer = 0 To Matrix.Length - 1
                ___column(i) = Matrix(i)(columnIndex)
            Next i
            Return ___column
        End Function

        ''' <summary>
        ''' Returns the nth row of the matrix * </summary>
        Public Overridable Function getRow(ByVal rowIndex As Integer) As Double()
            'TODO: Precondition
            Return Matrix(rowIndex)
        End Function

        Public Overridable Sub pivot(ByVal row As Integer, ByVal col As Integer)
            Dim pivotValue As Double = Matrix(row)(col)
            If Not pivotValue > 0 Then
                Throw New EvaluateException
            End If
            divideRowBy(row, pivotValue)

            For i As Integer = 0 To Matrix.Length - 1
                If i <> row Then
                    Dim multiplier As Double = Matrix(i)(col)
                    subtractRows(Matrix(i), Matrix(row), multiplier)
                End If
            Next i
        End Sub

        Private Sub subtractRows(ByVal row As Double(), ByVal pivotRow As Double(), ByVal multiplier As Double)
            For i As Integer = 0 To row.Length - 1 ' r[i] = r[i] - z * r[row];
                row(i) -= multiplier * pivotRow(i)
            Next i
        End Sub

        Public Overridable Sub divideRowBy(ByVal i As Integer, ByVal value As Double)
            Dim ___row As Double() = Matrix(i)
            For j As Integer = 0 To ___row.Length - 1
                ___row(j) = ___row(j) / value
            Next j
        End Sub

        Public Overridable Function transpose() As Double()()
            Dim x As Integer = Matrix.Length
            Dim y As Integer = Matrix(0).Length
            Dim transposed As Double()() = MAT(Of Double)(y, x)
            For i As Integer = 0 To x - 1
                For j As Integer = 0 To y - 1
                    transposed(j)(i) = Matrix(i)(j)
                Next j
            Next i
            Return transposed
        End Function

        ''' <summary>
        ''' Returns a copy of constraint rows of the matrix * </summary>
        Public Overridable ReadOnly Property Constraints As Double()()
            Get
                Return Arrays.copyOfRange(Matrix, 1, Matrix.Length)
            End Get
        End Property

        ''' <summary>
        ''' Returns true if the model is unbounded * </summary>
        Public Overridable ReadOnly Property Unbounded As Boolean
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Returns true if the problem is infeasible * </summary>
        Public Overridable ReadOnly Property Infeasible As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overridable Sub print()
            For i As Integer = 0 To Matrix.Length - 1
                For j As Integer = 0 To Matrix(i).Length - 1
                    Console.Write(Format(Matrix(i)(j), "#0.0") & " ")
                Next j
                Console.WriteLine()
            Next i
        End Sub

        ''' <summary>
        ''' Returns true if the tableau is in Proper Form, ie. it has
        ''' exactly one basic variable per equation.
        ''' 
        ''' </summary>
        Public Overridable Function inProperForm() As Boolean
            Dim basicVars As Integer() = BasicVariables
            For Each i As Integer In basicVars
                If i = -1 Then Return False
            Next i
            Return True
        End Function

        Public Overridable ReadOnly Property BasicVariables As Integer()
            Get
                Dim x As Integer = Matrix.Length
                Dim y As Integer = Matrix(0).Length - 1
                Dim basicVars As Integer() = New Integer(x - 1) {}
                Arrays.Fill(basicVars, -1)
                For i As Integer = 0 To x - 1
                    For j As Integer = 0 To y - 1
                        Dim basic As Boolean = False
                        If Math.Abs(Matrix(i)(j) - 1.0) < EPSILON Then
                            basic = True
                            Dim ___column As Double() = getColumn(j)
                            ' check if all other coefficients in column are 0
                            For m As Integer = 0 To ___column.Length - 1
                                If m <> i AndAlso Math.Abs(___column(m) - 0.0) > EPSILON Then basic = False
                            Next m
                        End If
                        If basic Then
                            If basicVars(i) <> -1 Then Throw New System.ArgumentException("Tableau not in proper form. Repeated basic variable for equation " & i)
                            basicVars(i) = j
                        End If
                    Next j
                Next i

                Return basicVars
            End Get
        End Property
    End Class

End Namespace