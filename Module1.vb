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
