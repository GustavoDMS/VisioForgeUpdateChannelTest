using VisioForge.Core.MediaBlocks;
using VisioForge.Core.MediaBlocks.Bridge;
using VisioForge.Core.MediaBlocks.Sinks;
using VisioForge.Core.MediaBlocks.Sources;
using VisioForge.Core.MediaBlocks.Special;
using VisioForge.Core.MediaBlocks.VideoRendering;
using VisioForge.Core.Types;
using VisioForge.Core.Types.X.Bridge;
using VisioForge.Core.Types.X.Sources;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        MediaBlocksPipeline _pipeline1 = new MediaBlocksPipeline();
        MediaBlocksPipeline _pipeline2 = new MediaBlocksPipeline();
        public Form1()
        {
            InitializeComponent();
            Start();
        }

        public async void Start()
        {
            
            var videoTest1 =
                new VirtualVideoSourceBlock(new VirtualVideoSourceSettings(1920, 1080, VideoFrameRate.FPS_30));
            var bridgeSink = new BridgeVideoSinkBlock(new BridgeVideoSinkSettings("pgm"));
            _pipeline1.AddBlock(bridgeSink);
            _pipeline1.Connect(videoTest1.Output, bridgeSink.Input);
            await _pipeline1.StartAsync();

            var videoTest2 =
                new VirtualVideoSourceBlock(new VirtualVideoSourceSettings(1920, 1080, VideoFrameRate.FPS_30){Pattern = VirtualVideoSourcePattern.Circular});
            var bridgeSink2 = new BridgeVideoSinkBlock(new BridgeVideoSinkSettings("pvw"));
            _pipeline2.AddBlock(bridgeSink2);
            _pipeline2.Connect(videoTest2.Output, bridgeSink2.Input);
            await _pipeline2.StartAsync();


            



            var pgmPipeline = new MediaBlocksPipeline();

            var bridgeSourcePgm = new BridgeVideoSourceBlock(new BridgeVideoSourceSettings("pgm"));
            var videoRender = new VideoRendererBlock(pgmPipeline, videoView1);
            
            pgmPipeline.Connect(bridgeSourcePgm.Output, videoRender.Input);
     
            await pgmPipeline.StartAsync();

            var pvwPipeline = new MediaBlocksPipeline();

            var bridgeSourcePvw = new BridgeVideoSourceBlock(new BridgeVideoSourceSettings("pvw"));
            var videoRenderPvw = new VideoRendererBlock(pvwPipeline, videoView2);
            pvwPipeline.Connect(bridgeSourcePvw.Output, videoRenderPvw.Input);
            await pvwPipeline.StartAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pgmSink = _pipeline1.GetBlock(MediaBlockType.BridgeVideoSink) as BridgeVideoSinkBlock;
            var pvwSink = _pipeline2.GetBlock(MediaBlockType.BridgeVideoSink) as BridgeVideoSinkBlock;

            pgmSink?.UpdateChannel("pvw");
            pvwSink?.UpdateChannel("pgm");
        }
    }
}