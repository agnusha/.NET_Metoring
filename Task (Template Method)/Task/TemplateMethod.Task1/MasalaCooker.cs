using System;

namespace TemplateMethod.Task1
{
    public class MasalaCooker
    {
        private ICooker cooker;

        public MasalaCooker(ICooker cooker)
        {
            this.cooker = cooker;
        }

        public void CookMasala(Country country)
        {
            if (country == Country.India)
            {
                cooker.FryChicken(100, Level.Strong);
                cooker.FryRice(200, Level.Strong);
                cooker.PrepareTea(15, TeaKind.Green, 12);

                cooker.PepperChicken(Level.Strong);
                cooker.PepperRice(Level.Strong);

                cooker.SaltChicken(Level.Strong);
                cooker.SaltRice(Level.Strong);
            }
            else if (country == Country.Ukraine)
            {
                cooker.FryChicken(300, Level.Medium);
                cooker.FryRice(500, Level.Strong);
                cooker.PrepareTea(10, TeaKind.Black, 10);

                cooker.PepperChicken(Level.Low);
                cooker.PepperRice(Level.Low);

                cooker.SaltChicken(Level.Medium);
                cooker.SaltRice(Level.Strong);
            }
        }
    }
}
