using LinqSnipers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LinqSnippets
{
    public class Snippets
    {
        static public void StringLinq()
        {
            //creo lista/arreglo --> de string
            string[] cars =
            {
                "VW Gol",
                "Ford Escort",
                "Ford Ka",
                "Audi A3",
                "Peugeot 207"
            };

            //1. selecciono todos los cars --> en sql SELECT * 
            //creo una variable y mediante cod sql le asigno los cars
            var carList = from car in cars select car;

            //la recorro con cod c#
            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }


            //2. SELECT WHERE car is Ausi-> filtro solo Ford
            var fordList = from car in cars where car.Contains("Ford") select car; //Contains("Ford") estoy manejando cadena de string

            foreach (var car in carList)
            {
                Console.WriteLine(fordList);
            }

        }

        //lista con numros
        static public void NumbersLinq()
        {
            List<int> numbers = new List<int> { 1,2,3,4,5,6,7,8,9};

            //mult todos x3 menos el 9 y los ordeno
            var listX3 =
                numbers
                .Select(num => num * 3)
                .Where(num => num != 9)
                .OrderBy(num => num);//ascendente

            foreach(var num in listX3)
            {
                Console.WriteLine(num);
            }
        }

        //
        static public void EjDeBusquedas()
        {
            List<string> textList= new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "cj",
                "f",
                "c"
            };

            //encontrar el 1ro de los elemntos
            var searchFirst = textList.First();

            //busco el 1er elemnt q comienza con "c"
            var firtsC = textList.First(text => text.Equals("c"));
            //busco todos los q comienzan con c
            var allC = textList.All(text => text.Equals("c"));
            //1er elemnt q contenga la j
            var contJ = textList.First(text => text.Contains("j"));
            //1er elemnt q cont z o sino return vacio
            var firtsZ = textList.FirstOrDefault(text => text.Contains("z"));
            //ultimo q cont z o vacio
            var lastZ = textList.LastOrDefault(text => text.Contains("z"));
            //omite repetidos
            var unicos = textList.Single();

            //creo 2 listas de num 
            int[] numPares = { 0, 2, 4, 6, 8 };
            int[] otros = { 0, 2, 6 };

            //muestro los q no se repiten
            var filtrorepetidos = numPares.Except(otros);
        }

        
        //funciones para multiples selecciones
        static public void SeleccMultiples()
        {
            string[] miOpiniones =
            {
                "Titulo, texto",
            };

            //voy a separar Por la coma
            var separo = miOpiniones.SelectMany(texto => texto.Split(","));


            //ejemplos con objetos
            //creo array de objetos --> Empresa y c/empresa -> array de Employee
            var enterprises = new[]
            {
                new Enterprice()//instancio new objet
                {
                    Id = 1,
                    Name = "Enterp 1",
                    Employess = new [] //array de objts
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Pepe",
                            Email = "pepe@hot.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Jose",
                            Email = "pepe@hot.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Pepe",
                            Email = "pepe@hot.com",
                            Salary = 2000
                        },
                    }
                },
                new Enterprice()
                {
                    Id = 2,
                    Name = "Enterp 2",
                    Employess = new [] //array de objts
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Ana",
                            Email = "pepe@hot.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "Maria",
                            Email = "pepe@hot.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "Sol",
                            Email = "pepe@hot.com",
                            Salary = 2000
                        }
                    }
                }
            };

            //obt todos los empleados
            var allEmp = enterprises.SelectMany(e => e.Employess);

            //compruebo q tenga empresas
            bool hayEmpresas = enterprises.Any();

            //compruebo q tenga empleados
            bool hayEmpleados = enterprises.Any(e => e.Employess.Any());

            //busco empleados con salario mas de 1000
            bool salari1000 =
                enterprises.Any(emp =>
                emp.Employess.Any(empleado => empleado.Salary < 2000));
        }
         

        //colecciones
        static public void linqCollections()
        {
            //lista1
            var firstList = new List<string> { "a", "b", "c"};
            //lista2
            var secondList = new List<string> { "a", "d", "c" };

            //inner join SOLO los comunes
            var comunes = from element in firstList
                          join elemnts2 in secondList
                          on element equals elemnts2
                          select new { element, elemnts2};


            //outer join - left --> esto se entiende mediante conjuntos
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElemnt in temporalList.DefaultIfEmpty()
                                where element != temporalElemnt
                                select new { Element = element };

            //outer join - left --> esto se entiende mediante conjuntos
            //opcion menos codigo
            var left2 = from element in firstList
                        from secondElement in secondList.Where(sE => sE == element).DefaultIfEmpty()
                        select new {Element = element, SecondElement = secondElement };

            //outer join - right --> esto se entiende mediante conjuntos
            var rigthOuterJoin = from secondElement in secondList
                                join element in firstList
                                on secondElement equals element
                                into temporalList
                                from temporalElemnt in temporalList.DefaultIfEmpty()
                                where secondElement != temporalElemnt
                                select new { Element = secondElement };


            //union --> osea todos los elemntos (menos la intersección) --> VER conjuntos
            var unionList = leftOuterJoin.Union(rigthOuterJoin); //uso lista de arriba
        }

        
        //saltear o descartar elementos
        static public void SaltarElemnts()
        {
            var lista = new[] { 1, 2, 3, 4, 5 };


            var saltaEl2 = lista.Skip(2);//1,3,4,5
            var saltaLosUltimos2 = lista.SkipLast(2);
            var saltaMayores4 = lista.SkipWhile(num => num < 4);// 5 6 7

            //tomar opuesto a lo anterior
            var tomoHastaEl2 = lista.Take(2); //{1,2}
            var tomoLos2Ultimos = lista.TakeLast(2); //{4,5}
            var tomoMenoresA4 = lista.TakeWhile(num => num < 4);

        }


        //paginacion
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int numPagina, int totXpag)//la collection es el array total
        {
            int startIndex = (numPagina - 1) * totXpag;
            return collection.Skip(startIndex);//con Skip salto los elemnts q no quiero ya se de 10en10 para c/pagina

        }

        //variables --> este metodo es para declarar variables dentro de las consultas
        static public void LinqVariables()
        {
            int[] numList = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

            //obtengo los numeros q esten por encima de la media, elevedos al cuadrado
            var result = from num in numList
                         let obtMedia = numList.Average()//obt media
                         let numCuadrado = Math.Pow(num, 2)//estas variables solo funcionan para result(no fuera)
                         where numCuadrado > obtMedia
                         select num;

            Console.WriteLine("Media: {0}", numList.Average());

            foreach(int num in result)
            {
                Console.WriteLine("Number: { 0 } Square: {1}", num, Math.Pow(num, 2));
            }

        }

        //funcion ZIP
        //esta funcion intercala los valores de ambas listas (tienen q terner misma cant de elemnts)
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] words = { "one", "two", "three", "four", "five" };

            //concateno
            IEnumerable<string> zipElements = numbers.Zip(words, (num, word) => num + "=" + word);//{"1=one","2=two"}

        }
        
        //Repeat & Range
        static public void repeat()
        {
            //genero una colleccion del 1 al 1000 
            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            //Repeat
            IEnumerable<string> cincoVecsX = Enumerable.Repeat("X", 3);//{"X","X","X"}
        }
        

        //consultas para la clase Student
        static public void studentLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Marcos",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "lore",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 3,
                    Name = "jose",
                    Grade = 20,
                    Certified = false,
                },
                new Student
                {
                    Id = 4,
                    Name = "pp",
                    Grade = 75,
                    Certified = false,
                }
            };

            //busco certificados
            var certificados = from alumno in classRoom
                               where alumno.Certified== true
                               select alumno;
            //no certif
            var noCertif = from alumno in classRoom
                           where alumno.Certified== false
                           select alumno;

            //alum con nota mayor a 50 y esté certif
            var aprobadoCertifName = from alumno in classRoom
                                 where alumno.Grade >= 50 && alumno.Certified== true
                                 select alumno.Name;

        }

        //All -> odos los elemnts deben beden cumplir la condicion ; ANY -->  algunos
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1,2,3,4,5 };

            //saber si todos los num son menores q 10
            bool result = numbers.All(n => n < 10);

        }

        //Aggregate
        //ejecuta varias operaciones cuya salida de la primera es la entrada de la sigt 
        static public void aggregateFuncion()
        {
            int[] numbers = { 1, 2, 3, 5 };

            //sumo todos los elemnts
            int suma = numbers.Aggregate((prevSuma, actual) => prevSuma + actual);
            //1er caso -> 0,1 = 1
            //2do caso -> 1,2 = 3

        }

        //District
        //para obtener valores distintos
        static public void distValores()
        {
            int[] numbers = { 1, 2, 3, 5, 1, 2, };

            IEnumerable<int> result = numbers.Distinct();
        }


        //GroupBy
        //agrupacion por algún tipo de condicion
        static public void gropBy() { 
            List<int> numbers = new List<int> { 1,2,3, 5, 6, 7, 8, 9 };

            //obtengo los pares
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            //al recorrer la variable 
            foreach(var valor in grouped)
            {
                Console.WriteLine(valor); //salida -> 13579 .. 2468 [primero los q no cumplen]
            }


            //otro ejm 
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Marcos",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "lore",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 3,
                    Name = "jose",
                    Grade = 20,
                    Certified = false,
                },
                new Student
                {
                    Id = 4,
                    Name = "pp",
                    Grade = 75,
                    Certified = false,
                }
            };

            var certifQuery = classRoom.GroupBy(student => student.Certified);

            //recorro 1er los grupos Q son 2 --> no certif y SI  certif
            foreach(var group in certifQuery)
            {
                Console.WriteLine("--------{0}-------", group.Key);
                foreach(var estudent in group)
                {
                    Console.WriteLine(estudent.Name);
                } 
            }
        }

    }
}