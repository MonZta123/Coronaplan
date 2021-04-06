using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using BeastSources.Coronaplan.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BeastSources.Coronaplan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly string[][] _data = {
            new [] {"ein", "zwei", "drei", "vier", "fünf", "sechs"},
            new [] {"tägige/n", "wöchige/n", "monatige/n", "fache/n", "malige/n", "hebige/n"},
            new [] {"harte/n", "softe/n", "optionale/n", "intransparente/n", "alternativlose/n", "unumkehrbare/n"},
            new [] {"Wellenbrecher", "Brücken", "Treppen", "Wende", "Impf", "Ehren"},
            new [] {"lockdown","stopp","maßnahme","kampagne","sprint","matrix"},
            new [] {"zum Sommer", "auf Weiteres", "zur Bundestagswahl", "2030", "nach Abiturprüfungen", "in die Puppen"},
            new [] {"sofortigen", "nachhaltigen", "allmählichen", "unausweichlichen", "wirtschaftsschonenden", "willkürlichen"},
            new [] {"Senkung", "Steigerung", "Beendigung", "Halbierung", "Vernichtung", "Beschönigung"},
            new [] {"Infektionszahlen", "privaten Treffen", "Wirtschaftsleistung", "Wahlprognosen", "dritten Welle", "Bundeskanzlerin"}
        };

        private readonly Random _r = new Random();
        private string A(int v1, int v2) => _data[v1][v2];


        [HttpGet]
        public IActionResult GetRandomPlan()
        {
            var seedInt = new List<int>();

            for (var i = 0; i < _data.Length; i++)
            {
                seedInt.Add(_r.Next(6));
            }

            var result = getText(seedInt.ToArray());

            return Ok(new
            {
                seed = string.Join("", seedInt.Select(n => n.ToString())),
                plan = result
            });
        }

        [HttpGet]
        [Route("{seed}")]
        public IActionResult GetRandomPlanSeed(string seed)
        {
            if (seed.Length != 8)
            {
                throw new ExceptionBase("Seed muss genau 8 Stellen lang sein!", 1000);
            }

            if (seed.Any(n => Convert.ToInt32(n) >= 7))
            {
                throw new ExceptionBase("Unbekannter Seed!", 1001);
            }

            var seedInt = seed.Select(Convert.ToInt32).ToArray();

            var result = getText(seedInt);

            return Ok(new
            {
                seed = string.Join("", seedInt.Select(n => n.ToString())),
                plan = result
            });
        }

        private string getText(IReadOnlyList<int> seedInt) =>
            $"Was wir jetzt brauchen ist ein/e " +
            $"{A(0, seedInt[0])}{A(1, seedInt[1])} {A(2, seedInt[2])} " +
            $"{A(3, seedInt[3])}{A(4, seedInt[4])} bis {A(5, seedInt[5])} zur " +
            $"{A(6, seedInt[6])} {A(7, seedInt[7])} der {A(8, seedInt[8])}.";
    }
}