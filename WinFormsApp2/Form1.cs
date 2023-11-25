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
        MediaBlocksPipeline _pipeline1Sink = new MediaBlocksPipeline();
        MediaBlocksPipeline _pipeline2Sink = new MediaBlocksPipeline();

        MediaBlocksPipeline _pipeline1Source = new MediaBlocksPipeline();
        MediaBlocksPipeline _pipeline2Source = new MediaBlocksPipeline();

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
            _pipeline1Sink.AddBlock(bridgeSink);
            _pipeline1Sink.Connect(videoTest1.Output, bridgeSink.Input);
            await _pipeline1Sink.StartAsync();

            var videoTest2 =
                new VirtualVideoSourceBlock(new VirtualVideoSourceSettings(1920, 1080, VideoFrameRate.FPS_30) { Pattern = VirtualVideoSourcePattern.Pinwheel });
            var bridgeSink2 = new BridgeVideoSinkBlock(new BridgeVideoSinkSettings("pvw"));
            _pipeline2Sink.AddBlock(bridgeSink2);
            _pipeline2Sink.Connect(videoTest2.Output, bridgeSink2.Input);
            await _pipeline2Sink.StartAsync();

            var bridgeSourcePgm = new BridgeVideoSourceBlock(new BridgeVideoSourceSettings("pgm"));
            var videoRender = new VideoRendererBlock(_pipeline1Source, videoView1);
            _pipeline1Source.Connect(bridgeSourcePgm.Output, videoRender.Input);
            await _pipeline1Source.StartAsync();

            var bridgeSourcePvw = new BridgeVideoSourceBlock(new BridgeVideoSourceSettings("pvw"));
            var videoRenderPvw = new VideoRendererBlock(_pipeline2Source, videoView2);
            _pipeline2Source.Connect(bridgeSourcePvw.Output, videoRenderPvw.Input);
            await _pipeline2Source.StartAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var source1 = _pipeline1Source.GetBlock(MediaBlockType.BridgeVideoSource) as BridgeVideoSourceBlock;
            var source2 = _pipeline2Source.GetBlock(MediaBlockType.BridgeVideoSource) as BridgeVideoSourceBlock;

            _pipeline1Source.GetPipelineContext().Pipeline.SetState(Gst.State.Null);
            _pipeline2Source.GetPipelineContext().Pipeline.SetState(Gst.State.Null);

            source1?.UpdateChannel("pvw");
            source2?.UpdateChannel("pgm");

            _pipeline1Source.GetPipelineContext().Pipeline.SetState(Gst.State.Playing);
            _pipeline2Source.GetPipelineContext().Pipeline.SetState(Gst.State.Playing);
        }
    }
}