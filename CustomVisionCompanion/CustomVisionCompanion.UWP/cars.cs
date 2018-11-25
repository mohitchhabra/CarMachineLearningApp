using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning.Preview;

// e36d5e70-97f5-4d4a-8ddc-fb90bc176f6f_ba552405-7607-4bb5-abec-714b30f4d06c

namespace CustomVisionCompanion.UWP
{
    public sealed class E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModelInput
    {
        public VideoFrame data { get; set; }
    }

    public sealed class E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModelOutput
    {
        public IList<string> classLabel { get; set; }
        public IDictionary<string, float> loss { get; set; }
        public E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModelOutput()
        {
            this.classLabel = new List<string>();
            this.loss = new Dictionary<string, float>()
            {
                { "cars", float.NaN },
                { "Negative", float.NaN },
            };
        }
    }

    public sealed class E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModel
    {
        private LearningModelPreview learningModel;
        public static async Task<E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModel> CreateE36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModel(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModel model = new E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModel();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModelOutput> EvaluateAsync(E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModelInput input) {
            E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModelOutput output = new E36d5e70_x002D_97f5_x002D_4d4a_x002D_8ddc_x002D_fb90bc176f6f_ba552405_x002D_7607_x002D_4bb5_x002D_abec_x002D_714b30f4d06cModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("data", input.data);
            binding.Bind("classLabel", output.classLabel);
            binding.Bind("loss", output.loss);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
