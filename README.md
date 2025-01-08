# Ordenar Texto

 Este Proyecto consiste en llevar a cabo una serie de acciones 
 sobre archivos: Abrir, Guardar, Ordenar Texto, Agregar 
 Fecha como encabezado.

 # Descripcion

 Este proyecto realiza una serie de acciones sobre Archivos de texto
 entre las que se pueden destacar: Abrir Archivo, Guardar Archivo, 
 Ordenar Archivo, obtener la hora local en la cabezera del archivo,
 lo cual se logra con los objetos StripMenu, OpenDialog y SaveDialog.
 Tambien se trabaja con la rutina de Ordenacion ShellSort que es
 llamada para ordenar un archivo de etxto cuando se muestra por pantalla
 y conservar los cambios si lo decide el usuario
 
 
 # Actualizacion: 08/01/2025
 # Hora: 13:15

Detalles técnicos del proyecto:
Idioma: Visual Basic.NET
Versión del framework: 4.7.2

# Sort Text

This project consists of carrying out a series of actions on files:
Open, Save, Sort Text, Add Date as a header.

# Description

This project performs a series of actions on text files,
among which we can highlight: Open File, Save File,
Sort File, get the local time in the file header,
which is achieved with the StripMenu, OpenDialog 
and SaveDialog objects. It also works with the ShellSort 
sorting routine that is called to sort a text file when 
it is displayed on the screen and keep the changes if 
the user decides so.

# Update: 01/08/2025
# Time: 13:15

Technical details of the project:
Language: Visual Basic.NET
Framework version: 4.7.2

![imagen](https://github.com/user-attachments/assets/6734ff3b-2f13-4b4d-86da-ad00d540d646)

Code of the Project:

Imports System.Array
Public Class Form1

    Dim ArregloJusto() As String

    Private Sub OpenToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim TodoElTexto As String = "", LíneaDeTexto As String = ""
        OpenFileDialog1.Filter = "Archivos de texto (*.TXT)|*.TXT"
        OpenFileDialog1.ShowDialog() 'abre el cuadro de diálogo Abrir
        If OpenFileDialog1.FileName <> "" Then
            Try 'abre el archivo y detecta cualesquiera errores mediante un controlador
                FileOpen(1, OpenFileDialog1.FileName, OpenMode.Input)
                Do Until EOF(1) 'lee las líneas del archivo
                    LíneaDeTexto = LineInput(1)
                    'agrega cada una de las líneas a la variable TodoElTexto
                    TodoElTexto = TodoElTexto & LíneaDeTexto & vbCrLf
                Loop                   'actualizar etiqueta
                etiNota.Text = OpenFileDialog1.FileName
                txtNota.Text = TodoElTexto 'despliega el archivo
                txtNota.Enabled = True 'activa el cursor de texto
                CloseToolStripMenuItem.Enabled = True  'activa el comando Cerrar
                OpenToolStripMenuItem.Enabled = False  'desactiva el comando Abrir
            Catch
                MsgBox("Error de apertura de archivo. Tal vez sea demasiado grande.")
            Finally
                FileClose(1) 'cierra el archivo
            End Try
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        txtNota.Text = ""             'elimina el contenido del cuadro de texto
        etiNota.Text = "Carga un archivo de texto mediante el comando Abrir."
        CloseToolStripMenuItem.Enabled = False  'desactiva el comando Cerrar
        OpenToolStripMenuItem.Enabled = True    'activa el comando Abrir
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt"
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName <> "" Then
            FileOpen(1, SaveFileDialog1.FileName, OpenMode.Output)
            PrintLine(1, txtNota.Text) 'copia el texto en el disco
            FileClose(1)
        End If
    End Sub

    Private Sub InsertDateToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles InsertDateToolStripMenuItem.Click
        txtNota.Text = My.Computer.Clock.LocalTime & vbCrLf & txtNota.Text
        txtNota.Select(1, 0)  'elimina la selección
    End Sub


    Private Sub SortTextToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles SortTextToolStripMenuItem.Click
        Dim ln, líneact, letra As String
        Dim i, CarEnArchivo, CuentaLíneas As Short

        'determina el número de líneas que hay en el objeto cuadro de texto (txtNota)
        CuentaLíneas = 0 'esta variable aloja el número total de líneas
        CarEnArchivo = txtNota.Text.Length 'obtiene el total de caracteres
        For i = 0 To CarEnArchivo - 1 'mueve un carácter a la vez
            letra = txtNota.Text.Substring(i, 1) 'obtiene una letra
            If letra = Chr(13) Then 'si encuentra un retorno de carro
                CuentaLíneas += 1 'va a la línea siguiente (y la suma a la cuenta)
                i += 1 'salta el carácter de alimentación de línea (normalmente sigue a cr en PC)
            End If
        Next i

        'crea un arreglo para contener el texto del cuadro de texto
        ReDim ArregloJusto(CuentaLíneas) 'crea un arreglo del tamaño apropiado
        líneact = 1
        ln = "" 'emplea ln para crear líneas, un carácter a la vez
        For i = 0 To CarEnArchivo - 1 'hace un nuevo ciclo en el texto
            letra = txtNota.Text.Substring(i, 1) 'get letter
            If letra = Chr(13) Then 'si se localiza un retorno de carro
                líneact = líneact + 1 'incrementa el conteo de líneas
                i += 1 'salta el carácter de alimentación de líneas
                ln = "" 'borra la línea y va a la siguiente
            Else
                ln = ln & letra 'agrega una letra a la línea
                ArregloJusto(líneact) = ln 'y la suma al arreglo
            End If
        Next i

        'ordena el arreglo
        ShellSort(ArregloJusto, CuentaLíneas)

        'y despliega el arreglo ordenado en el cuadro de texto
        txtNota.Text = ""
        líneact = 1
        For i = 1 To CuentaLíneas
            txtNota.Text = txtNota.Text &
              ArregloJusto(líneact) & vbCrLf
            líneact += 1
        Next i
        txtNota.Select(1, 0)   'elimina la selección del texto
    End Sub

    Private Sub ExitToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Module Module1

    Sub ShellSort(ByRef ordenar() As String, ByVal numDeElementos As Short)
        Dim temp As String
        Dim i, j, abarcar As Short

        'El Procedimiento ShellSort organiza los elementos de ordenar()
        'en orden descendente y los devuelve al procedimiento que los invoco

        abarcar = numDeElementos / 2
        Do While abarcar > 0
            For i = abarcar To numDeElementos - 1
                For j = (i - abarcar + 1) To 1 Step -abarcar
                    If ordenar(j) <= ordenar(j + abarcar) Then Exit For

                    'Revisa los elementos del Arreglo que estan en desorden
                    temp = ordenar(j)
                    ordenar(j) = ordenar(j + abarcar)
                    ordenar(j + abarcar) = temp
                Next j
            Next i
            abarcar = abarcar / 2
        Loop
    End Sub

End Module

End Class
