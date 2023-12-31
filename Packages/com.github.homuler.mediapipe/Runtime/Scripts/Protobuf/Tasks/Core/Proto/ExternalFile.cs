// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: mediapipe/tasks/cc/core/proto/external_file.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Mediapipe.Tasks.Core.Proto {

  /// <summary>Holder for reflection information generated from mediapipe/tasks/cc/core/proto/external_file.proto</summary>
  public static partial class ExternalFileReflection {

    #region Descriptor
    /// <summary>File descriptor for mediapipe/tasks/cc/core/proto/external_file.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ExternalFileReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjFtZWRpYXBpcGUvdGFza3MvY2MvY29yZS9wcm90by9leHRlcm5hbF9maWxl",
            "LnByb3RvEhptZWRpYXBpcGUudGFza3MuY29yZS5wcm90byLNAQoMRXh0ZXJu",
            "YWxGaWxlEhQKDGZpbGVfY29udGVudBgBIAEoDBIRCglmaWxlX25hbWUYAiAB",
            "KAkSTAoUZmlsZV9kZXNjcmlwdG9yX21ldGEYAyABKAsyLi5tZWRpYXBpcGUu",
            "dGFza3MuY29yZS5wcm90by5GaWxlRGVzY3JpcHRvck1ldGESRgoRZmlsZV9w",
            "b2ludGVyX21ldGEYBCABKAsyKy5tZWRpYXBpcGUudGFza3MuY29yZS5wcm90",
            "by5GaWxlUG9pbnRlck1ldGEiQAoSRmlsZURlc2NyaXB0b3JNZXRhEgoKAmZk",
            "GAEgASgFEg4KBmxlbmd0aBgCIAEoAxIOCgZvZmZzZXQYAyABKAMiMgoPRmls",
            "ZVBvaW50ZXJNZXRhEg8KB3BvaW50ZXIYASABKAQSDgoGbGVuZ3RoGAIgASgD",
            "QjoKJWNvbS5nb29nbGUubWVkaWFwaXBlLnRhc2tzLmNvcmUucHJvdG9CEUV4",
            "dGVybmFsRmlsZVByb3Rv"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.Tasks.Core.Proto.ExternalFile), global::Mediapipe.Tasks.Core.Proto.ExternalFile.Parser, new[]{ "FileContent", "FileName", "FileDescriptorMeta", "FilePointerMeta" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.Tasks.Core.Proto.FileDescriptorMeta), global::Mediapipe.Tasks.Core.Proto.FileDescriptorMeta.Parser, new[]{ "Fd", "Length", "Offset" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.Tasks.Core.Proto.FilePointerMeta), global::Mediapipe.Tasks.Core.Proto.FilePointerMeta.Parser, new[]{ "Pointer", "Length" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Represents external files used by the engines (e.g. TF Lite flatbuffers). The
  /// files can be specified by one of the following three ways:
  ///
  /// (1) file contents loaded in `file_content`.
  /// (2) file path in `file_name`.
  /// (3) file descriptor through `file_descriptor_meta` as returned by open(2).
  /// (4) file pointer and length in memory through `file_pointer_meta`.
  ///
  /// If more than one field of these fields is provided, they are used in this
  /// precedence order.
  /// Next id: 5
  /// </summary>
  public sealed partial class ExternalFile : pb::IMessage<ExternalFile>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ExternalFile> _parser = new pb::MessageParser<ExternalFile>(() => new ExternalFile());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ExternalFile> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.Tasks.Core.Proto.ExternalFileReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExternalFile() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExternalFile(ExternalFile other) : this() {
      fileContent_ = other.fileContent_;
      fileName_ = other.fileName_;
      fileDescriptorMeta_ = other.fileDescriptorMeta_ != null ? other.fileDescriptorMeta_.Clone() : null;
      filePointerMeta_ = other.filePointerMeta_ != null ? other.filePointerMeta_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ExternalFile Clone() {
      return new ExternalFile(this);
    }

    /// <summary>Field number for the "file_content" field.</summary>
    public const int FileContentFieldNumber = 1;
    private readonly static pb::ByteString FileContentDefaultValue = pb::ByteString.Empty;

    private pb::ByteString fileContent_;
    /// <summary>
    /// The file contents as a byte array.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString FileContent {
      get { return fileContent_ ?? FileContentDefaultValue; }
      set {
        fileContent_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "file_content" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasFileContent {
      get { return fileContent_ != null; }
    }
    /// <summary>Clears the value of the "file_content" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearFileContent() {
      fileContent_ = null;
    }

    /// <summary>Field number for the "file_name" field.</summary>
    public const int FileNameFieldNumber = 2;
    private readonly static string FileNameDefaultValue = "";

    private string fileName_;
    /// <summary>
    /// The path to the file to open and mmap in memory
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string FileName {
      get { return fileName_ ?? FileNameDefaultValue; }
      set {
        fileName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "file_name" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasFileName {
      get { return fileName_ != null; }
    }
    /// <summary>Clears the value of the "file_name" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearFileName() {
      fileName_ = null;
    }

    /// <summary>Field number for the "file_descriptor_meta" field.</summary>
    public const int FileDescriptorMetaFieldNumber = 3;
    private global::Mediapipe.Tasks.Core.Proto.FileDescriptorMeta fileDescriptorMeta_;
    /// <summary>
    /// The file descriptor to a file opened with open(2), with optional additional
    /// offset and length information.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Core.Proto.FileDescriptorMeta FileDescriptorMeta {
      get { return fileDescriptorMeta_; }
      set {
        fileDescriptorMeta_ = value;
      }
    }

    /// <summary>Field number for the "file_pointer_meta" field.</summary>
    public const int FilePointerMetaFieldNumber = 4;
    private global::Mediapipe.Tasks.Core.Proto.FilePointerMeta filePointerMeta_;
    /// <summary>
    /// The pointer points to location of a file in memory. Use the util method,
    /// `SetExternalFile` in [1], to configure `file_pointer_meta` from a
    /// `std::string_view` object.
    ///
    /// [1]: mediapipe/tasks/cc/metadata/utils/zip_utils.h
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Tasks.Core.Proto.FilePointerMeta FilePointerMeta {
      get { return filePointerMeta_; }
      set {
        filePointerMeta_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ExternalFile);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ExternalFile other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (FileContent != other.FileContent) return false;
      if (FileName != other.FileName) return false;
      if (!object.Equals(FileDescriptorMeta, other.FileDescriptorMeta)) return false;
      if (!object.Equals(FilePointerMeta, other.FilePointerMeta)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (HasFileContent) hash ^= FileContent.GetHashCode();
      if (HasFileName) hash ^= FileName.GetHashCode();
      if (fileDescriptorMeta_ != null) hash ^= FileDescriptorMeta.GetHashCode();
      if (filePointerMeta_ != null) hash ^= FilePointerMeta.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (HasFileContent) {
        output.WriteRawTag(10);
        output.WriteBytes(FileContent);
      }
      if (HasFileName) {
        output.WriteRawTag(18);
        output.WriteString(FileName);
      }
      if (fileDescriptorMeta_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(FileDescriptorMeta);
      }
      if (filePointerMeta_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(FilePointerMeta);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (HasFileContent) {
        output.WriteRawTag(10);
        output.WriteBytes(FileContent);
      }
      if (HasFileName) {
        output.WriteRawTag(18);
        output.WriteString(FileName);
      }
      if (fileDescriptorMeta_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(FileDescriptorMeta);
      }
      if (filePointerMeta_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(FilePointerMeta);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (HasFileContent) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(FileContent);
      }
      if (HasFileName) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FileName);
      }
      if (fileDescriptorMeta_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(FileDescriptorMeta);
      }
      if (filePointerMeta_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(FilePointerMeta);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ExternalFile other) {
      if (other == null) {
        return;
      }
      if (other.HasFileContent) {
        FileContent = other.FileContent;
      }
      if (other.HasFileName) {
        FileName = other.FileName;
      }
      if (other.fileDescriptorMeta_ != null) {
        if (fileDescriptorMeta_ == null) {
          FileDescriptorMeta = new global::Mediapipe.Tasks.Core.Proto.FileDescriptorMeta();
        }
        FileDescriptorMeta.MergeFrom(other.FileDescriptorMeta);
      }
      if (other.filePointerMeta_ != null) {
        if (filePointerMeta_ == null) {
          FilePointerMeta = new global::Mediapipe.Tasks.Core.Proto.FilePointerMeta();
        }
        FilePointerMeta.MergeFrom(other.FilePointerMeta);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            FileContent = input.ReadBytes();
            break;
          }
          case 18: {
            FileName = input.ReadString();
            break;
          }
          case 26: {
            if (fileDescriptorMeta_ == null) {
              FileDescriptorMeta = new global::Mediapipe.Tasks.Core.Proto.FileDescriptorMeta();
            }
            input.ReadMessage(FileDescriptorMeta);
            break;
          }
          case 34: {
            if (filePointerMeta_ == null) {
              FilePointerMeta = new global::Mediapipe.Tasks.Core.Proto.FilePointerMeta();
            }
            input.ReadMessage(FilePointerMeta);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            FileContent = input.ReadBytes();
            break;
          }
          case 18: {
            FileName = input.ReadString();
            break;
          }
          case 26: {
            if (fileDescriptorMeta_ == null) {
              FileDescriptorMeta = new global::Mediapipe.Tasks.Core.Proto.FileDescriptorMeta();
            }
            input.ReadMessage(FileDescriptorMeta);
            break;
          }
          case 34: {
            if (filePointerMeta_ == null) {
              FilePointerMeta = new global::Mediapipe.Tasks.Core.Proto.FilePointerMeta();
            }
            input.ReadMessage(FilePointerMeta);
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  /// A proto defining file descriptor metadata for mapping file into memory using
  /// mmap(2).
  /// </summary>
  public sealed partial class FileDescriptorMeta : pb::IMessage<FileDescriptorMeta>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<FileDescriptorMeta> _parser = new pb::MessageParser<FileDescriptorMeta>(() => new FileDescriptorMeta());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<FileDescriptorMeta> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.Tasks.Core.Proto.ExternalFileReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FileDescriptorMeta() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FileDescriptorMeta(FileDescriptorMeta other) : this() {
      _hasBits0 = other._hasBits0;
      fd_ = other.fd_;
      length_ = other.length_;
      offset_ = other.offset_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FileDescriptorMeta Clone() {
      return new FileDescriptorMeta(this);
    }

    /// <summary>Field number for the "fd" field.</summary>
    public const int FdFieldNumber = 1;
    private readonly static int FdDefaultValue = 0;

    private int fd_;
    /// <summary>
    /// File descriptor as returned by open(2).
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Fd {
      get { if ((_hasBits0 & 1) != 0) { return fd_; } else { return FdDefaultValue; } }
      set {
        _hasBits0 |= 1;
        fd_ = value;
      }
    }
    /// <summary>Gets whether the "fd" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasFd {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "fd" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearFd() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "length" field.</summary>
    public const int LengthFieldNumber = 2;
    private readonly static long LengthDefaultValue = 0L;

    private long length_;
    /// <summary>
    /// Optional length of the mapped memory. If not specified, the actual file
    /// size is used at runtime.
    ///
    /// This is an advanced option, e.g. this can be used on Android to specify the
    /// length of a given asset obtained from AssetFileDescriptor#getLength().
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long Length {
      get { if ((_hasBits0 & 2) != 0) { return length_; } else { return LengthDefaultValue; } }
      set {
        _hasBits0 |= 2;
        length_ = value;
      }
    }
    /// <summary>Gets whether the "length" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasLength {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "length" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearLength() {
      _hasBits0 &= ~2;
    }

    /// <summary>Field number for the "offset" field.</summary>
    public const int OffsetFieldNumber = 3;
    private readonly static long OffsetDefaultValue = 0L;

    private long offset_;
    /// <summary>
    /// Optional starting offset in the file referred to by the file descriptor
    /// `fd`.
    ///
    /// This is an advanced option, e.g. this can be used on Android to specify the
    /// offset of a given asset obtained from AssetFileDescriptor#getStartOffset().
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long Offset {
      get { if ((_hasBits0 & 4) != 0) { return offset_; } else { return OffsetDefaultValue; } }
      set {
        _hasBits0 |= 4;
        offset_ = value;
      }
    }
    /// <summary>Gets whether the "offset" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasOffset {
      get { return (_hasBits0 & 4) != 0; }
    }
    /// <summary>Clears the value of the "offset" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearOffset() {
      _hasBits0 &= ~4;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as FileDescriptorMeta);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(FileDescriptorMeta other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Fd != other.Fd) return false;
      if (Length != other.Length) return false;
      if (Offset != other.Offset) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (HasFd) hash ^= Fd.GetHashCode();
      if (HasLength) hash ^= Length.GetHashCode();
      if (HasOffset) hash ^= Offset.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (HasFd) {
        output.WriteRawTag(8);
        output.WriteInt32(Fd);
      }
      if (HasLength) {
        output.WriteRawTag(16);
        output.WriteInt64(Length);
      }
      if (HasOffset) {
        output.WriteRawTag(24);
        output.WriteInt64(Offset);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (HasFd) {
        output.WriteRawTag(8);
        output.WriteInt32(Fd);
      }
      if (HasLength) {
        output.WriteRawTag(16);
        output.WriteInt64(Length);
      }
      if (HasOffset) {
        output.WriteRawTag(24);
        output.WriteInt64(Offset);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (HasFd) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Fd);
      }
      if (HasLength) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Length);
      }
      if (HasOffset) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Offset);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(FileDescriptorMeta other) {
      if (other == null) {
        return;
      }
      if (other.HasFd) {
        Fd = other.Fd;
      }
      if (other.HasLength) {
        Length = other.Length;
      }
      if (other.HasOffset) {
        Offset = other.Offset;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Fd = input.ReadInt32();
            break;
          }
          case 16: {
            Length = input.ReadInt64();
            break;
          }
          case 24: {
            Offset = input.ReadInt64();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Fd = input.ReadInt32();
            break;
          }
          case 16: {
            Length = input.ReadInt64();
            break;
          }
          case 24: {
            Offset = input.ReadInt64();
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  /// The pointer points to location of a file in memory. Make sure the file memory
  /// that it points locates on the same machine and it outlives this
  /// FilePointerMeta object.
  /// </summary>
  public sealed partial class FilePointerMeta : pb::IMessage<FilePointerMeta>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<FilePointerMeta> _parser = new pb::MessageParser<FilePointerMeta>(() => new FilePointerMeta());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<FilePointerMeta> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.Tasks.Core.Proto.ExternalFileReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FilePointerMeta() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FilePointerMeta(FilePointerMeta other) : this() {
      _hasBits0 = other._hasBits0;
      pointer_ = other.pointer_;
      length_ = other.length_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public FilePointerMeta Clone() {
      return new FilePointerMeta(this);
    }

    /// <summary>Field number for the "pointer" field.</summary>
    public const int PointerFieldNumber = 1;
    private readonly static ulong PointerDefaultValue = 0UL;

    private ulong pointer_;
    /// <summary>
    /// Memory address of the file in decimal.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ulong Pointer {
      get { if ((_hasBits0 & 1) != 0) { return pointer_; } else { return PointerDefaultValue; } }
      set {
        _hasBits0 |= 1;
        pointer_ = value;
      }
    }
    /// <summary>Gets whether the "pointer" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasPointer {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "pointer" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearPointer() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "length" field.</summary>
    public const int LengthFieldNumber = 2;
    private readonly static long LengthDefaultValue = 0L;

    private long length_;
    /// <summary>
    /// File length.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long Length {
      get { if ((_hasBits0 & 2) != 0) { return length_; } else { return LengthDefaultValue; } }
      set {
        _hasBits0 |= 2;
        length_ = value;
      }
    }
    /// <summary>Gets whether the "length" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasLength {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "length" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearLength() {
      _hasBits0 &= ~2;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as FilePointerMeta);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(FilePointerMeta other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Pointer != other.Pointer) return false;
      if (Length != other.Length) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (HasPointer) hash ^= Pointer.GetHashCode();
      if (HasLength) hash ^= Length.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (HasPointer) {
        output.WriteRawTag(8);
        output.WriteUInt64(Pointer);
      }
      if (HasLength) {
        output.WriteRawTag(16);
        output.WriteInt64(Length);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (HasPointer) {
        output.WriteRawTag(8);
        output.WriteUInt64(Pointer);
      }
      if (HasLength) {
        output.WriteRawTag(16);
        output.WriteInt64(Length);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (HasPointer) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Pointer);
      }
      if (HasLength) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Length);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(FilePointerMeta other) {
      if (other == null) {
        return;
      }
      if (other.HasPointer) {
        Pointer = other.Pointer;
      }
      if (other.HasLength) {
        Length = other.Length;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Pointer = input.ReadUInt64();
            break;
          }
          case 16: {
            Length = input.ReadInt64();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Pointer = input.ReadUInt64();
            break;
          }
          case 16: {
            Length = input.ReadInt64();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
