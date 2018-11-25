using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning.Preview;

// 93deb65e-dfa1-49a3-8a7c-b802a3a909e1_28f9b6ec-eae5-49e6-8da7-2b70beb4c498

namespace CustomVisionCompanion.UWP
{
    public sealed class _x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498ModelInput
    {
        public VideoFrame data { get; set; }
    }

    public sealed class _x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498ModelOutput
    {
        public IList<string> classLabel { get; set; }
        public IDictionary<string, float> loss { get; set; }
        public _x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498ModelOutput()
        {
            this.classLabel = new List<string>();
            this.loss = new Dictionary<string, float>()
            {
                { "controller", float.NaN },
                { "keyboard", float.NaN },
                { "laptop", float.NaN },
                { "monitor", float.NaN },
                { "mouse", float.NaN },
            };
        }
    }

    public sealed class _x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498Model
    {
        private LearningModelPreview learningModel;
        public static async Task<_x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498Model> Create_x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498Model(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            _x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498Model model = new _x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498Model();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<_x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498ModelOutput> EvaluateAsync(_x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498ModelInput input) {
            _x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498ModelOutput output = new _x0039_3deb65e_x002D_dfa1_x002D_49a3_x002D_8a7c_x002D_b802a3a909e1_28f9b6ec_x002D_eae5_x002D_49e6_x002D_8da7_x002D_2b70beb4c498ModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("data", input.data);
            binding.Bind("classLabel", output.classLabel);
            binding.Bind("loss", output.loss);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
