' *****************************************************************************
' Copyright 2012 Yuriy Lagodiuk
' 
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
' 
'   http://www.apache.org/licenses/LICENSE-2.0
' 
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.
' *****************************************************************************

Namespace GAF

    Public Class GeneticAlgorithm(Of C As Chromosome(Of C), T As IComparable(Of T))

        Shared ReadOnly ALL_PARENTAL_CHROMOSOMES As Integer = Integer.MaxValue

        Private Class ChromosomesComparator
            Implements IComparer(Of C)

            ReadOnly outerInstance As GeneticAlgorithm(Of C, T)

            Public Sub New(outerInstance As GeneticAlgorithm(Of C, T))
                Me.outerInstance = outerInstance
            End Sub

            ReadOnly cache As New Dictionary(Of C, T)

            Public Function compare(chr1 As C, chr2 As C) As Integer Implements IComparer(Of C).Compare
                Dim fit1 As T = Me.fit(chr1)
                Dim fit2 As T = Me.fit(chr2)
                Dim ret As Integer = fit1.CompareTo(fit2)
                Return ret
            End Function

            Public Overridable Function fit(chr As C) As T
                If cache.ContainsKey(chr) Then
                    Return cache(chr)
                Else
                    Dim fit__ As T = outerInstance.fitnessFunc.calculate(chr)
                    Me.cache(chr) = fit__
                    Return fit__
                End If
            End Function

            Public Overridable Sub clearCache()
                Me.cache.Clear()
            End Sub
        End Class

        ReadOnly _chromosomesComparator As ChromosomesComparator
        ReadOnly fitnessFunc As Fitness(Of C, T)

        ''' <summary>
        ''' listeners of genetic algorithm iterations (handle callback afterwards)
        ''' </summary>
        ReadOnly iterationListeners As New List(Of IterartionListener(Of C, T))

        Dim terminate__ As Boolean = False

        Public Sub New(population As Population(Of C), fitnessFunc As Fitness(Of C, T))
            Me.Population = population
            Me.fitnessFunc = fitnessFunc
            Me._chromosomesComparator = New ChromosomesComparator(Me)
            Me.Population.sortPopulationByFitness(_chromosomesComparator)
        End Sub

        Public Overridable Sub evolve()
            Dim parentPopulationSize As Integer = Population.Size
            Dim newPopulation As New Population(Of C)()
            Dim i As Integer = 0

            Do While (i < parentPopulationSize) AndAlso (i < Me.ParentChromosomesSurviveCount)
                newPopulation.addChromosome(Population(i))
                i += 1
            Loop

            For i = 0 To parentPopulationSize - 1
                Dim chromosome As C = Population(i)
                Dim mutated As C = chromosome.mutate()

                Dim otherChromosome As C = Me.Population.RandomChromosome
                Dim crossovered As IList(Of C) = chromosome.crossover(otherChromosome)

                newPopulation.addChromosome(mutated)

                For Each c As C In crossovered
                    newPopulation.addChromosome(c)
                Next
            Next

            newPopulation.sortPopulationByFitness(_chromosomesComparator)
            newPopulation.Trim(parentPopulationSize)
            _Population = newPopulation
        End Sub

        Public Overridable Sub evolve(count As Integer)
            Me.terminate__ = False

            For i As Integer = 0 To count - 1
                If Me.terminate__ Then
                    Exit For
                End If
                Me.evolve()
                Me._Iteration = i
                For Each l As IterartionListener(Of C, T) In Me.iterationListeners
                    l.update(Me)
                Next
            Next
        End Sub

        Public Overridable ReadOnly Property Iteration As Integer

        Public Overridable Sub terminate()
            Me.terminate__ = True
        End Sub

        Public Overridable ReadOnly Property Population As Population(Of C)

        Public Overridable ReadOnly Property Best As C
            Get
                Return Population(0)
            End Get
        End Property

        Public Overridable ReadOnly Property Worst As C
            Get
                Return Population(Population.Size - 1)
            End Get
        End Property

        ''' <summary>
        ''' Number of parental chromosomes, which survive (and move to new
        ''' population)
        ''' </summary>
        ''' <returns></returns>
        Public Overridable Property ParentChromosomesSurviveCount As Integer = ALL_PARENTAL_CHROMOSOMES

        Public Overridable Sub addIterationListener(listener As IterartionListener(Of C, T))
            Me.iterationListeners.Add(listener)
        End Sub

        Public Overridable Sub removeIterationListener(listener As IterartionListener(Of C, T))
            Me.iterationListeners.Remove(listener)
        End Sub

        Public Overridable Function fitness(chromosome As C) As T
            Return _chromosomesComparator.fit(chromosome)
        End Function

        Public Overridable Sub clearCache()
            _chromosomesComparator.clearCache()
        End Sub
    End Class
End Namespace