namespace Tutorin.Models
{
    public enum EtatPrestation
    {
        A_affecter, Enseignants_inscrits, Payee_par_responsable_eleve, Realisée, Enseigants_payes, Attestation_a_faire, Facturee, Annulee
    }

    public static class EtatPrestationExtensions
    {
        public static string NomEtat(EtatPrestation prestation)
        {
            switch ((int)prestation)
            {
                case 0:
                    return "A affecter";
                case 1:
                    return "Enseignants inscrits";
                case 2:
                    return "Payée par le responsable élève";
                case 3:
                    return "Réalisée";
                case 4:
                    return "Enseignants payés";
                case 5:
                    return "Attestation à faire";
                case 6:
                    return "Facturée";
                case 7:
                    return "Annulée";
            }
            return null;
        }


    }
}
