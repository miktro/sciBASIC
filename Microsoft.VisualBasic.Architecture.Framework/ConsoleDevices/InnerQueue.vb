﻿Imports System.Threading

Namespace Terminal

    ''' <summary>
    ''' Task action Queue for terminal QUEUE SOLVER 🙉
    ''' </summary>
    Module InnerQueue

        ''' <summary>
        ''' Writer Thread ☺
        ''' </summary>
        Dim MyThread As New Thread(AddressOf exeQueue)

        ''' <summary>
        ''' If TRUE, the Writing process will be separated from the main thread.
        ''' </summary>
        Public Property MultiThreadSupport As Boolean = True

        ''' <summary>
        ''' Just my queue
        ''' </summary>
        ReadOnly Queue As New Queue(Of Action)(6666)

        ''' <summary>
        ''' Is thread running?
        ''' hum
        ''' </summary>
        Dim QSolverRunning As Boolean = False

        ''' <summary>
        ''' lock
        ''' </summary>
        Dim dummy As New Object()

        ''' <summary>
        ''' Add an Action to the queue.
        ''' </summary>
        ''' <param name="A">()=>{ .. }</param>
        Public Sub AddToQueue(A As Action)
            Queue.Enqueue(A)

            If MultiThreadSupport Then
                SyncLock dummy
                    If Not MyThread.IsAlive Then
                        QSolverRunning = False
                        MyThread = New Thread(AddressOf exeQueue)

                        MyThread.Name = "xConsole · Multi-Thread Writer"
                        MyThread.Start()
                    End If
                End SyncLock
            Else
                exeQueue()
            End If
        End Sub

        ''' <summary>
        ''' Wait for xConsole queue (Needed if you are using multiThreaded queue)
        ''' </summary>
        Public Sub WaitQueue()
            While QSolverRunning = True
                Thread.Sleep(10)
            End While
        End Sub

        ''' <summary>
        ''' Execute the queue list
        ''' </summary>
        Private Sub exeQueue()
            QSolverRunning = True

            While Queue IsNot Nothing AndAlso Queue.Count > 0
                Thread.MemoryBarrier()

                Dim a As Action = Queue.Dequeue()

                If a IsNot Nothing Then
                    a.Invoke()
                End If
            End While

            QSolverRunning = False
        End Sub
    End Module
End Namespace