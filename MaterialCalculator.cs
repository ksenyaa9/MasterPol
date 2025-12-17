using System;
using System.Linq;
using System.Windows;

namespace MasterPolGerasimova
{
    public static class MaterialCalculator
    {
        /// <param name="productTypeId">Идентификатор типа продукции</param>
        /// <param name="materialTypeId">Идентификатор типа материала</param>
        /// <param name="productCount">Количество получаемой продукции</param>
        /// <param name="param1">Первый параметр продукции (вещественное, положительное)</param>
        /// <param name="param2">Второй параметр продукции (вещественное, положительное)</param>
        /// <returns>Количество необходимого материала с учетом брака или -1 при ошибке</returns>
        public static int CalculateRequiredMaterial(int productTypeId, int materialTypeId,
                                                   int productCount, double param1, double param2)
        {
            try
            {
                // Проверка входных параметров
                if (productCount <= 0 || param1 <= 0 || param2 <= 0)
                    return -1;

                var context = MasterPol_Entities.GetContext();

                var productType = context.ProductType
                    .FirstOrDefault(pt => pt.id_productType == productTypeId);
                if (productType == null)
                    return -1;


                var materialType = context.materialType
                    .FirstOrDefault(mt => mt.id_type == materialTypeId);
                if (materialType == null)
                    return -1;

                double productTypeFactor = productType.product_typeFactor;

                // Получение процента брака материала
                double defectRate = materialType.defect_rate;
                if (defectRate < 0 || defectRate >= 1)
                    return -1;

                double materialPerUnit = param1 * param2 * productTypeFactor;

                double totalMaterialWithoutDefect = materialPerUnit * productCount;


                double totalMaterialWithDefect = totalMaterialWithoutDefect / (1 - defectRate);

                int requiredMaterial = (int)Math.Ceiling(totalMaterialWithDefect);

                return requiredMaterial;
            }
            catch (Exception ex)
            {
             
                MessageBox.Show($"Ошибка при расчете: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

      
    }
}