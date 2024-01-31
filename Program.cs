using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib;
using System.Net;


int snmpPort = 161; //161
var endpoint = new IPEndPoint(IPAddress.Parse("192.168.0.11"), snmpPort); //192.168.0.15
OctetString community = new OctetString("public");
VersionCode version = VersionCode.V2;

 

// Crea una lista de OIDs que deseas consultar
List<ObjectIdentifier> oids = new List<ObjectIdentifier>
{

    new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.1.16")
    /*new ObjectIdentifier("1.3.6.1.2.1.1.5.0")  //Nombre o direccion  
    ,new ObjectIdentifier("1.3.6.1.2.1.1.6.0")  //Localisacion 
    ,new ObjectIdentifier("1.3.6.1.2.1.1.2.0") //Nombre del fabricante 
    ,new ObjectIdentifier("1.3.6.1.2.1.1.3.0") //Numero de serie 
    ,new ObjectIdentifier("1.3.6.1.2.1.1.4.0") //Version del software
    //,new ObjectIdentifier("1.3.6.1.2.1.1.1.0") //
    ,new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.9") //Informacion de las tablas
    ,new ObjectIdentifier("1.3.6.1.2.1.2.2.1.10.9") //Velocidad de entrada
    ,new ObjectIdentifier("1.3.6.1.2.1.2.2.1.14.9") //Errores de entrada //recepcion de datos perdida de paquetes, paquetes dañados
    ,new ObjectIdentifier("1.3.6.1.2.1.2.2.1.16.9") //Velocidad de salida 
    ,new ObjectIdentifier("1.3.6.1.2.1.2.2.1.20.1") //Errores de salida //recepcion de datos perdida de paquetes, paquetes dañados
    ,new ObjectIdentifier("1.3.6.1.2.1.2.2.1.5.9") //Speed 
    
    //,new ObjectIdentifier("1.3.6.1.2.1.2.2.1.16.22") //Hasta el 27 todos marcan Realtek RTL8723DE 802.11b/g/n PCIe Adapter-WFP Native MAC Layer LightWeight Filter-0000     
    */
};


ObjectIdentifier objetdeseado = oids[0];


// Crea una lista de variables SNMP utilizando los OIDs
var variables = new List<Variable>();
foreach (var oid in oids)
{
    variables.Add(new Variable(oid));
}

 

try
{
    var result = Messenger.Get(version, endpoint, community, variables, 9000);

    if (result != null && result.Count > 0)
    {
        foreach (var v in result)
        {
            Console.WriteLine($"{v.Id.ToString()} = {v.Data.ToString()}");
        }
    }
    else
    {
        Console.WriteLine("No se obtuvo respuesta del dispositivo SNMP.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
