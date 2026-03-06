using System;
using System.Runtime.InteropServices;

namespace MoonshineSharp
{
    public unsafe partial struct transcriber_option_t
    {
        [NativeTypeName("const char *")]
        public sbyte* name;

        [NativeTypeName("const char *")]
        public sbyte* value;
    }

    public unsafe partial struct transcript_line_t
    {
        [NativeTypeName("const char *")]
        public sbyte* text;

        [NativeTypeName("const float *")]
        public float* audio_data;

        [NativeTypeName("size_t")]
        public UIntPtr audio_data_count;

        public float start_time;

        public float duration;

        [NativeTypeName("uint64_t")]
        public ulong id;

        [NativeTypeName("int8_t")]
        public sbyte is_complete;

        [NativeTypeName("int8_t")]
        public sbyte is_updated;

        [NativeTypeName("int8_t")]
        public sbyte is_new;

        [NativeTypeName("int8_t")]
        public sbyte has_text_changed;

        [NativeTypeName("int8_t")]
        public sbyte has_speaker_id;

        [NativeTypeName("uint64_t")]
        public ulong speaker_id;

        [NativeTypeName("uint32_t")]
        public uint speaker_index;

        [NativeTypeName("uint32_t")]
        public uint last_transcription_latency_ms;
    }

    public unsafe partial struct transcript_t
    {
        [NativeTypeName("struct transcript_line_t *")]
        public transcript_line_t* lines;

        [NativeTypeName("uint64_t")]
        public ulong line_count;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void moonshine_intent_callback(void* user_data, [NativeTypeName("const char *")] sbyte* trigger_phrase, [NativeTypeName("const char *")] sbyte* utterance, float similarity);

    public static unsafe partial class MoonshineSharp
    {
        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_get_version();

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* moonshine_error_to_string([NativeTypeName("int32_t")] int error);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("const char *")]
        public static extern sbyte* moonshine_transcript_to_string([NativeTypeName("const struct transcript_t *")] transcript_t* transcript);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_load_transcriber_from_files([NativeTypeName("const char *")] sbyte* path, [NativeTypeName("uint32_t")] uint model_arch, [NativeTypeName("const struct transcriber_option_t *")] transcriber_option_t* options, [NativeTypeName("uint64_t")] ulong options_count, [NativeTypeName("int32_t")] int moonshine_version);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_load_transcriber_from_memory([NativeTypeName("const uint8_t *")] byte* encoder_model_data, [NativeTypeName("size_t")] UIntPtr encoder_model_data_size, [NativeTypeName("const uint8_t *")] byte* decoder_model_data, [NativeTypeName("size_t")] UIntPtr decoder_model_data_size, [NativeTypeName("const uint8_t *")] byte* tokenizer_data, [NativeTypeName("size_t")] UIntPtr tokenizer_data_size, [NativeTypeName("uint32_t")] uint model_arch, [NativeTypeName("const struct transcriber_option_t *")] transcriber_option_t* options, [NativeTypeName("uint64_t")] ulong options_count, [NativeTypeName("int32_t")] int moonshine_version);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void moonshine_free_transcriber([NativeTypeName("int32_t")] int transcriber_handle);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_transcribe_without_streaming([NativeTypeName("int32_t")] int transcriber_handle, float* audio_data, [NativeTypeName("uint64_t")] ulong audio_length, [NativeTypeName("int32_t")] int sample_rate, [NativeTypeName("uint32_t")] uint flags, [NativeTypeName("struct transcript_t **")] transcript_t** out_transcript);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_create_stream([NativeTypeName("int32_t")] int transcriber_handle, [NativeTypeName("uint32_t")] uint flags);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_free_stream([NativeTypeName("int32_t")] int transcriber_handle, [NativeTypeName("int32_t")] int stream_handle);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_start_stream([NativeTypeName("int32_t")] int transcriber_handle, [NativeTypeName("int32_t")] int stream_handle);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_stop_stream([NativeTypeName("int32_t")] int transcriber_handle, [NativeTypeName("int32_t")] int stream_handle);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_transcribe_add_audio_to_stream([NativeTypeName("int32_t")] int transcriber_handle, [NativeTypeName("int32_t")] int stream_handle, [NativeTypeName("const float *")] float* new_audio_data, [NativeTypeName("uint64_t")] ulong audio_length, [NativeTypeName("int32_t")] int sample_rate, [NativeTypeName("uint32_t")] uint flags);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_transcribe_stream([NativeTypeName("int32_t")] int transcriber_handle, [NativeTypeName("int32_t")] int stream_handle, [NativeTypeName("uint32_t")] uint flags, [NativeTypeName("struct transcript_t **")] transcript_t** out_transcript);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_create_intent_recognizer([NativeTypeName("const char *")] sbyte* model_path, [NativeTypeName("uint32_t")] uint model_arch, [NativeTypeName("const char *")] sbyte* model_variant, float threshold);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void moonshine_free_intent_recognizer([NativeTypeName("int32_t")] int intent_recognizer_handle);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_register_intent([NativeTypeName("int32_t")] int intent_recognizer_handle, [NativeTypeName("const char *")] sbyte* trigger_phrase, [NativeTypeName("moonshine_intent_callback")] IntPtr callback, void* user_data);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_unregister_intent([NativeTypeName("int32_t")] int intent_recognizer_handle, [NativeTypeName("const char *")] sbyte* trigger_phrase);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_process_utterance([NativeTypeName("int32_t")] int intent_recognizer_handle, [NativeTypeName("const char *")] sbyte* utterance);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_set_intent_threshold([NativeTypeName("int32_t")] int intent_recognizer_handle, float threshold);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float moonshine_get_intent_threshold([NativeTypeName("int32_t")] int intent_recognizer_handle);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_get_intent_count([NativeTypeName("int32_t")] int intent_recognizer_handle);

        [DllImport("moonshine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int moonshine_clear_intents([NativeTypeName("int32_t")] int intent_recognizer_handle);

        [NativeTypeName("#define MOONSHINE_HEADER_VERSION (20000)")]
        public const int MOONSHINE_HEADER_VERSION = (20000);

        [NativeTypeName("#define MOONSHINE_MODEL_ARCH_TINY (0)")]
        public const int MOONSHINE_MODEL_ARCH_TINY = (0);

        [NativeTypeName("#define MOONSHINE_MODEL_ARCH_BASE (1)")]
        public const int MOONSHINE_MODEL_ARCH_BASE = (1);

        [NativeTypeName("#define MOONSHINE_MODEL_ARCH_TINY_STREAMING (2)")]
        public const int MOONSHINE_MODEL_ARCH_TINY_STREAMING = (2);

        [NativeTypeName("#define MOONSHINE_MODEL_ARCH_BASE_STREAMING (3)")]
        public const int MOONSHINE_MODEL_ARCH_BASE_STREAMING = (3);

        [NativeTypeName("#define MOONSHINE_MODEL_ARCH_SMALL_STREAMING (4)")]
        public const int MOONSHINE_MODEL_ARCH_SMALL_STREAMING = (4);

        [NativeTypeName("#define MOONSHINE_MODEL_ARCH_MEDIUM_STREAMING (5)")]
        public const int MOONSHINE_MODEL_ARCH_MEDIUM_STREAMING = (5);

        [NativeTypeName("#define MOONSHINE_ERROR_NONE (0)")]
        public const int MOONSHINE_ERROR_NONE = (0);

        [NativeTypeName("#define MOONSHINE_ERROR_UNKNOWN (-1)")]
        public const int MOONSHINE_ERROR_UNKNOWN = (-1);

        [NativeTypeName("#define MOONSHINE_ERROR_INVALID_HANDLE (-2)")]
        public const int MOONSHINE_ERROR_INVALID_HANDLE = (-2);

        [NativeTypeName("#define MOONSHINE_ERROR_INVALID_ARGUMENT (-3)")]
        public const int MOONSHINE_ERROR_INVALID_ARGUMENT = (-3);

        [NativeTypeName("#define MOONSHINE_FLAG_FORCE_UPDATE (1 << 0)")]
        public const int MOONSHINE_FLAG_FORCE_UPDATE = (1 << 0);

        [NativeTypeName("#define MOONSHINE_EMBEDDING_MODEL_ARCH_GEMMA_300M (0)")]
        public const int MOONSHINE_EMBEDDING_MODEL_ARCH_GEMMA_300M = (0);
    }
}
