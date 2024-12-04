# Caja-De-Cambio
El proyecto Caja de cambio es un sistema que deberá trabajar con las siguientes denominaciones y cantidades de dinero:
	Denominación			Cantidad
$ 1,000.00					0
$ 500.00						5
$ 200.00						7
$ 100.00						9
$ 50.00						  11
$ 20.00						  13
$ 10.00					  	17
$ 5.00							23
$ 2.00							29
$ 1.00							31

Especificaciones:
El sistema sólo aceptará las denominaciones permitidas. Cualquier otro valor no se aceptará.
Una vez que se valida la denominación, se procederá a cambiar con la siguiente denominación o denominaciones menores.
Se debe de diferenciar las monedas de los billetes. Los billetes empiezan en 1000 y concluyen en 20. Las monedas son el resto de denominaciones.
La única moneda que no esta permitida para dar cambio es la $ 1.00 peso. Dado que no hay otra menor denominación.
Los mensajes de error que se han de utilizar en el sistema, son los siguientes:
	Denominación incorrecta.
	No hay suficientes billetes y/o monedas para dar cambio de la denominación ingresada.
	La moneda de $ 1.00 no ofrece cambio.
	La opción ingresada es incorrecta.
	Error desconocido en el proceso de cambio.
El sistema contará con sólo tres opciones:
	Se ingresará la denominación a cambiar, y se mostrará el cambio obtenido.
	Se ingresará  el valor de -1, el sistema mostrará las cantidades que hay en todas las denominaciones.
	Se ingresará el valor de -2, y el sistema cerrará la operación y posteriormente el cierre del sistema.
Las cantidades de las denominaciones se almacenarán en un archivo de texto plano. Y el mismo se actualizará al cierre del sistema.
El sistema deberá de contar con una interface amigable. De tal manera que el usuario interactue en forma senciila, práctica y eficaz.
