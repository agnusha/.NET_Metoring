using System;
using TemplateMethod.Task2.Cookers;

namespace TemplateMethod.Task2
{
    public class MasalaCooker
    {
        private readonly ICooker cooker;

        public MasalaCooker(ICooker cooker)
        {
            this.cooker = cooker;
        }

        public void CookMasala(Country country)
        {
            CookerBase cb;
            if (country == Country.Ukraine) {
                cb = new UkraineCooker();
            }
            else
            {
                cb = new IndiaCooker();
            }
            cb.CookMasala(cooker);
        }
    }
}
