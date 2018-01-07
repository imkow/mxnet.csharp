using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable UnusedMember.Global

namespace mxnet.csharp
{
    public partial class NdArray
    {
private static readonly List<string> ContribBoxNmsInFormatConvert = new List<string>(){"center","corner"};
private static readonly List<string> ContribBoxNmsOutFormatConvert = new List<string>(){"center","corner"};
/// <summary>
/// Apply non-maximum suppression to input.The output will be sorted in descending order according to `score`. Boxes withoverlaps larger than `overlap_thresh` and smaller scores will be removed andfilled with -1, the corresponding position will be recorded for backward propogation.During back-propagation, the gradient will be copied to the originalposition according to the input index. For positions that have been suppressed,the in_grad will be assigned 0.In summary, gradients are sticked to its boxes, will either be moved or discardedaccording to its original index in input.Input requirements:1. Input tensor have at least 2 dimensions, (n, k), any higher dims will be regardedas batch, e.g. (a, b, c, d, n, k) == (a*b*c*d, n, k)2. n is the number of boxes in each batch3. k is the width of each box item.By default, a box is [id, score, xmin, ymin, xmax, ymax, ...],additional elements are allowed.- `id_index`: optional, use -1 to ignore, useful if `force_suppress=False`, which meanswe will skip highly overlapped boxes if one is `apple` while the other is `car`.- `coord_start`: required, default=2, the starting index of the 4 coordinates.Two formats are supported:  `corner`: [xmin, ymin, xmax, ymax]  `center`: [x, y, width, height]- `score_index`: required, default=1, box score/confidence.When two boxes overlap IOU > `overlap_thresh`, the one with smaller score will be suppressed.- `in_format` and `out_format`: default='corner', specify in/out box formats.Examples::  x = [[0, 0.5, 0.1, 0.1, 0.2, 0.2], [1, 0.4, 0.1, 0.1, 0.2, 0.2],       [0, 0.3, 0.1, 0.1, 0.14, 0.14], [2, 0.6, 0.5, 0.5, 0.7, 0.8]]  box_nms(x, overlap_thresh=0.1, coord_start=2, score_index=1, id_index=0,      force_suppress=True, in_format='corner', out_typ='corner') =      [[2, 0.6, 0.5, 0.5, 0.7, 0.8], [0, 0.5, 0.1, 0.1, 0.2, 0.2],       [-1, -1, -1, -1, -1, -1], [-1, -1, -1, -1, -1, -1]]  out_grad = [[0.1, 0.1, 0.1, 0.1, 0.1, 0.1], [0.2, 0.2, 0.2, 0.2, 0.2, 0.2],              [0.3, 0.3, 0.3, 0.3, 0.3, 0.3], [0.4, 0.4, 0.4, 0.4, 0.4, 0.4]]  # exe.backward  in_grad = [[0.2, 0.2, 0.2, 0.2, 0.2, 0.2], [0, 0, 0, 0, 0, 0],             [0, 0, 0, 0, 0, 0], [0.1, 0.1, 0.1, 0.1, 0.1, 0.1]]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\bounding_box.cc:L82
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="overlap_thresh">Overlapping(IoU) threshold to suppress object with smaller score.</param>
/// <param name="topk">Apply nms to topk boxes with descending scores, -1 to no restriction.</param>
/// <param name="coord_start">Start index of the consecutive 4 coordinates.</param>
/// <param name="score_index">Index of the scores/confidence of boxes.</param>
/// <param name="id_index">Optional, index of the class categories, -1 to disable.</param>
/// <param name="force_suppress">Optional, if set false and id_index is provided, nms will only apply to boxes belongs to the same category</param>
/// <param name="in_format">The input box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
/// <param name="out_format">The output box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribBoxNms(NdArray @out,
NdArray data,
float overlap_thresh=0.5f,
int topk=-1,
int coord_start=2,
int score_index=1,
int id_index=-1,
bool force_suppress=false,
ContribBoxNmsInFormat in_format=ContribBoxNmsInFormat.Corner,
ContribBoxNmsOutFormat out_format=ContribBoxNmsOutFormat.Corner)
{
return new Operator("_contrib_box_nms")
.SetParam("overlap_thresh", overlap_thresh)
.SetParam("topk", topk)
.SetParam("coord_start", coord_start)
.SetParam("score_index", score_index)
.SetParam("id_index", id_index)
.SetParam("force_suppress", force_suppress)
.SetParam("in_format", Util.EnumToString<ContribBoxNmsInFormat>(in_format,ContribBoxNmsInFormatConvert))
.SetParam("out_format", Util.EnumToString<ContribBoxNmsOutFormat>(out_format,ContribBoxNmsOutFormatConvert))
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Apply non-maximum suppression to input.The output will be sorted in descending order according to `score`. Boxes withoverlaps larger than `overlap_thresh` and smaller scores will be removed andfilled with -1, the corresponding position will be recorded for backward propogation.During back-propagation, the gradient will be copied to the originalposition according to the input index. For positions that have been suppressed,the in_grad will be assigned 0.In summary, gradients are sticked to its boxes, will either be moved or discardedaccording to its original index in input.Input requirements:1. Input tensor have at least 2 dimensions, (n, k), any higher dims will be regardedas batch, e.g. (a, b, c, d, n, k) == (a*b*c*d, n, k)2. n is the number of boxes in each batch3. k is the width of each box item.By default, a box is [id, score, xmin, ymin, xmax, ymax, ...],additional elements are allowed.- `id_index`: optional, use -1 to ignore, useful if `force_suppress=False`, which meanswe will skip highly overlapped boxes if one is `apple` while the other is `car`.- `coord_start`: required, default=2, the starting index of the 4 coordinates.Two formats are supported:  `corner`: [xmin, ymin, xmax, ymax]  `center`: [x, y, width, height]- `score_index`: required, default=1, box score/confidence.When two boxes overlap IOU > `overlap_thresh`, the one with smaller score will be suppressed.- `in_format` and `out_format`: default='corner', specify in/out box formats.Examples::  x = [[0, 0.5, 0.1, 0.1, 0.2, 0.2], [1, 0.4, 0.1, 0.1, 0.2, 0.2],       [0, 0.3, 0.1, 0.1, 0.14, 0.14], [2, 0.6, 0.5, 0.5, 0.7, 0.8]]  box_nms(x, overlap_thresh=0.1, coord_start=2, score_index=1, id_index=0,      force_suppress=True, in_format='corner', out_typ='corner') =      [[2, 0.6, 0.5, 0.5, 0.7, 0.8], [0, 0.5, 0.1, 0.1, 0.2, 0.2],       [-1, -1, -1, -1, -1, -1], [-1, -1, -1, -1, -1, -1]]  out_grad = [[0.1, 0.1, 0.1, 0.1, 0.1, 0.1], [0.2, 0.2, 0.2, 0.2, 0.2, 0.2],              [0.3, 0.3, 0.3, 0.3, 0.3, 0.3], [0.4, 0.4, 0.4, 0.4, 0.4, 0.4]]  # exe.backward  in_grad = [[0.2, 0.2, 0.2, 0.2, 0.2, 0.2], [0, 0, 0, 0, 0, 0],             [0, 0, 0, 0, 0, 0], [0.1, 0.1, 0.1, 0.1, 0.1, 0.1]]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\bounding_box.cc:L82
/// </summary>
/// <param name="data">The input</param>
/// <param name="overlap_thresh">Overlapping(IoU) threshold to suppress object with smaller score.</param>
/// <param name="topk">Apply nms to topk boxes with descending scores, -1 to no restriction.</param>
/// <param name="coord_start">Start index of the consecutive 4 coordinates.</param>
/// <param name="score_index">Index of the scores/confidence of boxes.</param>
/// <param name="id_index">Optional, index of the class categories, -1 to disable.</param>
/// <param name="force_suppress">Optional, if set false and id_index is provided, nms will only apply to boxes belongs to the same category</param>
/// <param name="in_format">The input box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
/// <param name="out_format">The output box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribBoxNms(NdArray data,
float overlap_thresh=0.5f,
int topk=-1,
int coord_start=2,
int score_index=1,
int id_index=-1,
bool force_suppress=false,
ContribBoxNmsInFormat in_format=ContribBoxNmsInFormat.Corner,
ContribBoxNmsOutFormat out_format=ContribBoxNmsOutFormat.Corner)
{
return new Operator("_contrib_box_nms")
.SetParam("overlap_thresh", overlap_thresh)
.SetParam("topk", topk)
.SetParam("coord_start", coord_start)
.SetParam("score_index", score_index)
.SetParam("id_index", id_index)
.SetParam("force_suppress", force_suppress)
.SetParam("in_format", Util.EnumToString<ContribBoxNmsInFormat>(in_format,ContribBoxNmsInFormatConvert))
.SetParam("out_format", Util.EnumToString<ContribBoxNmsOutFormat>(out_format,ContribBoxNmsOutFormatConvert))
.SetInput("data", data)
.Invoke();
}
private static readonly List<string> BackwardContribBoxNmsInFormatConvert = new List<string>(){"center","corner"};
private static readonly List<string> BackwardContribBoxNmsOutFormatConvert = new List<string>(){"center","corner"};
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="overlap_thresh">Overlapping(IoU) threshold to suppress object with smaller score.</param>
/// <param name="topk">Apply nms to topk boxes with descending scores, -1 to no restriction.</param>
/// <param name="coord_start">Start index of the consecutive 4 coordinates.</param>
/// <param name="score_index">Index of the scores/confidence of boxes.</param>
/// <param name="id_index">Optional, index of the class categories, -1 to disable.</param>
/// <param name="force_suppress">Optional, if set false and id_index is provided, nms will only apply to boxes belongs to the same category</param>
/// <param name="in_format">The input box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
/// <param name="out_format">The output box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribBoxNms(NdArray @out,
float overlap_thresh=0.5f,
int topk=-1,
int coord_start=2,
int score_index=1,
int id_index=-1,
bool force_suppress=false,
BackwardContribBoxNmsInFormat in_format=BackwardContribBoxNmsInFormat.Corner,
BackwardContribBoxNmsOutFormat out_format=BackwardContribBoxNmsOutFormat.Corner)
{
return new Operator("_backward_contrib_box_nms")
.SetParam("overlap_thresh", overlap_thresh)
.SetParam("topk", topk)
.SetParam("coord_start", coord_start)
.SetParam("score_index", score_index)
.SetParam("id_index", id_index)
.SetParam("force_suppress", force_suppress)
.SetParam("in_format", Util.EnumToString<BackwardContribBoxNmsInFormat>(in_format,BackwardContribBoxNmsInFormatConvert))
.SetParam("out_format", Util.EnumToString<BackwardContribBoxNmsOutFormat>(out_format,BackwardContribBoxNmsOutFormatConvert))
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="overlap_thresh">Overlapping(IoU) threshold to suppress object with smaller score.</param>
/// <param name="topk">Apply nms to topk boxes with descending scores, -1 to no restriction.</param>
/// <param name="coord_start">Start index of the consecutive 4 coordinates.</param>
/// <param name="score_index">Index of the scores/confidence of boxes.</param>
/// <param name="id_index">Optional, index of the class categories, -1 to disable.</param>
/// <param name="force_suppress">Optional, if set false and id_index is provided, nms will only apply to boxes belongs to the same category</param>
/// <param name="in_format">The input box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
/// <param name="out_format">The output box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribBoxNms(float overlap_thresh=0.5f,
int topk=-1,
int coord_start=2,
int score_index=1,
int id_index=-1,
bool force_suppress=false,
BackwardContribBoxNmsInFormat in_format=BackwardContribBoxNmsInFormat.Corner,
BackwardContribBoxNmsOutFormat out_format=BackwardContribBoxNmsOutFormat.Corner)
{
return new Operator("_backward_contrib_box_nms")
.SetParam("overlap_thresh", overlap_thresh)
.SetParam("topk", topk)
.SetParam("coord_start", coord_start)
.SetParam("score_index", score_index)
.SetParam("id_index", id_index)
.SetParam("force_suppress", force_suppress)
.SetParam("in_format", Util.EnumToString<BackwardContribBoxNmsInFormat>(in_format,BackwardContribBoxNmsInFormatConvert))
.SetParam("out_format", Util.EnumToString<BackwardContribBoxNmsOutFormat>(out_format,BackwardContribBoxNmsOutFormatConvert))
.Invoke();
}
private static readonly List<string> ContribBoxIouFormatConvert = new List<string>(){"center","corner"};
/// <summary>
/// Bounding box overlap of two arrays.  The overlap is defined as Intersection-over-Union, aka, IOU.  - lhs: (a_1, a_2, ..., a_n, 4) array  - rhs: (b_1, b_2, ..., b_n, 4) array  - output: (a_1, a_2, ..., a_n, b_1, b_2, ..., b_n) array  Note::    Zero gradients are back-propagated in this op for now.  Example::    x = [[0.5, 0.5, 1.0, 1.0], [0.0, 0.0, 0.5, 0.5]]    y = [0.25, 0.25, 0.75, 0.75]    box_iou(x, y, format='corner') = [[0.1428], [0.1428]]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\bounding_box.cc:L123
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">The first input</param>
/// <param name="rhs">The second input</param>
/// <param name="format">The box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribBoxIou(NdArray @out,
NdArray lhs,
NdArray rhs,
ContribBoxIouFormat format=ContribBoxIouFormat.Corner)
{
return new Operator("_contrib_box_iou")
.SetParam("format", Util.EnumToString<ContribBoxIouFormat>(format,ContribBoxIouFormatConvert))
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Bounding box overlap of two arrays.  The overlap is defined as Intersection-over-Union, aka, IOU.  - lhs: (a_1, a_2, ..., a_n, 4) array  - rhs: (b_1, b_2, ..., b_n, 4) array  - output: (a_1, a_2, ..., a_n, b_1, b_2, ..., b_n) array  Note::    Zero gradients are back-propagated in this op for now.  Example::    x = [[0.5, 0.5, 1.0, 1.0], [0.0, 0.0, 0.5, 0.5]]    y = [0.25, 0.25, 0.75, 0.75]    box_iou(x, y, format='corner') = [[0.1428], [0.1428]]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\bounding_box.cc:L123
/// </summary>
/// <param name="lhs">The first input</param>
/// <param name="rhs">The second input</param>
/// <param name="format">The box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribBoxIou(NdArray lhs,
NdArray rhs,
ContribBoxIouFormat format=ContribBoxIouFormat.Corner)
{
return new Operator("_contrib_box_iou")
.SetParam("format", Util.EnumToString<ContribBoxIouFormat>(format,ContribBoxIouFormatConvert))
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
private static readonly List<string> BackwardContribBoxIouFormatConvert = new List<string>(){"center","corner"};
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="format">The box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribBoxIou(NdArray @out,
BackwardContribBoxIouFormat format=BackwardContribBoxIouFormat.Corner)
{
return new Operator("_backward_contrib_box_iou")
.SetParam("format", Util.EnumToString<BackwardContribBoxIouFormat>(format,BackwardContribBoxIouFormatConvert))
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="format">The box encoding type.  "corner" means boxes are encoded as [xmin, ymin, xmax, ymax], "center" means boxes are encodes as [x, y, width, height].</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribBoxIou(BackwardContribBoxIouFormat format=BackwardContribBoxIouFormat.Corner)
{
return new Operator("_backward_contrib_box_iou")
.SetParam("format", Util.EnumToString<BackwardContribBoxIouFormat>(format,BackwardContribBoxIouFormatConvert))
.Invoke();
}
/// <summary>
/// Compute bipartite matching.  The matching is performed on score matrix with shape [B, N, M]  - B: batch_size  - N: number of rows to match  - M: number of columns as reference to be matched against.  Returns:  x : matched column indices. -1 indicating non-matched elements in rows.  y : matched row indices.  Note::    Zero gradients are back-propagated in this op for now.  Example::    s = [[0.5, 0.6], [0.1, 0.2], [0.3, 0.4]]    x, y = bipartite_matching(x, threshold=1e-12, is_ascend=False)    x = [1, -1, 0]    y = [2, 0]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\bounding_box.cc:L169
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="threshold">Ignore matching when score < thresh, if is_ascend=false, or ignore score > thresh, if is_ascend=true.</param>
/// <param name="is_ascend">Use ascend order for scores instead of descending. Please set threshold accordingly.</param>
/// <param name="topk">Limit the number of matches to topk, set -1 for no limit</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribBipartiteMatching(NdArray @out,
NdArray data,
float threshold,
bool is_ascend=false,
int topk=-1)
{
return new Operator("_contrib_bipartite_matching")
.SetParam("is_ascend", is_ascend)
.SetParam("threshold", threshold)
.SetParam("topk", topk)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Compute bipartite matching.  The matching is performed on score matrix with shape [B, N, M]  - B: batch_size  - N: number of rows to match  - M: number of columns as reference to be matched against.  Returns:  x : matched column indices. -1 indicating non-matched elements in rows.  y : matched row indices.  Note::    Zero gradients are back-propagated in this op for now.  Example::    s = [[0.5, 0.6], [0.1, 0.2], [0.3, 0.4]]    x, y = bipartite_matching(x, threshold=1e-12, is_ascend=False)    x = [1, -1, 0]    y = [2, 0]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\bounding_box.cc:L169
/// </summary>
/// <param name="data">The input</param>
/// <param name="threshold">Ignore matching when score < thresh, if is_ascend=false, or ignore score > thresh, if is_ascend=true.</param>
/// <param name="is_ascend">Use ascend order for scores instead of descending. Please set threshold accordingly.</param>
/// <param name="topk">Limit the number of matches to topk, set -1 for no limit</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribBipartiteMatching(NdArray data,
float threshold,
bool is_ascend=false,
int topk=-1)
{
return new Operator("_contrib_bipartite_matching")
.SetParam("is_ascend", is_ascend)
.SetParam("threshold", threshold)
.SetParam("topk", topk)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="threshold">Ignore matching when score < thresh, if is_ascend=false, or ignore score > thresh, if is_ascend=true.</param>
/// <param name="is_ascend">Use ascend order for scores instead of descending. Please set threshold accordingly.</param>
/// <param name="topk">Limit the number of matches to topk, set -1 for no limit</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribBipartiteMatching(NdArray @out,
float threshold,
bool is_ascend=false,
int topk=-1)
{
return new Operator("_backward_contrib_bipartite_matching")
.SetParam("is_ascend", is_ascend)
.SetParam("threshold", threshold)
.SetParam("topk", topk)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="threshold">Ignore matching when score < thresh, if is_ascend=false, or ignore score > thresh, if is_ascend=true.</param>
/// <param name="is_ascend">Use ascend order for scores instead of descending. Please set threshold accordingly.</param>
/// <param name="topk">Limit the number of matches to topk, set -1 for no limit</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribBipartiteMatching(float threshold,
bool is_ascend=false,
int topk=-1)
{
return new Operator("_backward_contrib_bipartite_matching")
.SetParam("is_ascend", is_ascend)
.SetParam("threshold", threshold)
.SetParam("topk", topk)
.Invoke();
}
private static readonly List<string> ContribDequantizeOutTypeConvert = new List<string>(){"float32"};
/// <summary>
/// Dequantize the input tensor into a float tensor.[min_range, max_range] are scalar floats that spcify the range forthe output data.Each value of the tensor will undergo the following:`out[i] = min_range + (in[i] * (max_range - min_range) / range(INPUT_TYPE))`here `range(T) = numeric_limits<T>::max() - numeric_limits<T>::min()`Defined in I:\workspace\incubator-mxnet\src\operator\contrib\dequantize.cc:L41
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="input">A ndarray/symbol of type `uint8`</param>
/// <param name="min_range">The minimum scalar value possibly produced for the input</param>
/// <param name="max_range">The maximum scalar value possibly produced for the input</param>
/// <param name="out_type">Output data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribDequantize(NdArray @out,
NdArray input,
NdArray min_range,
NdArray max_range,
ContribDequantizeOutType out_type)
{
return new Operator("_contrib_dequantize")
.SetParam("out_type", Util.EnumToString<ContribDequantizeOutType>(out_type,ContribDequantizeOutTypeConvert))
.SetInput("input", input)
.SetInput("min_range", min_range)
.SetInput("max_range", max_range)
.Invoke(@out);
}
/// <summary>
/// Dequantize the input tensor into a float tensor.[min_range, max_range] are scalar floats that spcify the range forthe output data.Each value of the tensor will undergo the following:`out[i] = min_range + (in[i] * (max_range - min_range) / range(INPUT_TYPE))`here `range(T) = numeric_limits<T>::max() - numeric_limits<T>::min()`Defined in I:\workspace\incubator-mxnet\src\operator\contrib\dequantize.cc:L41
/// </summary>
/// <param name="input">A ndarray/symbol of type `uint8`</param>
/// <param name="min_range">The minimum scalar value possibly produced for the input</param>
/// <param name="max_range">The maximum scalar value possibly produced for the input</param>
/// <param name="out_type">Output data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribDequantize(NdArray input,
NdArray min_range,
NdArray max_range,
ContribDequantizeOutType out_type)
{
return new Operator("_contrib_dequantize")
.SetParam("out_type", Util.EnumToString<ContribDequantizeOutType>(out_type,ContribDequantizeOutTypeConvert))
.SetInput("input", input)
.SetInput("min_range", min_range)
.SetInput("max_range", max_range)
.Invoke();
}
private static readonly List<string> ContribQuantizeOutTypeConvert = new List<string>(){"uint8"};
/// <summary>
/// Quantize a input tensor from float to `out_type`,with user-specified `min_range` and `max_range`.[min_range, max_range] are scalar floats that spcify the range forthe input data. Each value of the tensor will undergo the following:`out[i] = (in[i] - min_range) * range(OUTPUT_TYPE) / (max_range - min_range)`here `range(T) = numeric_limits<T>::max() - numeric_limits<T>::min()`Defined in I:\workspace\incubator-mxnet\src\operator\contrib\quantize.cc:L41
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="input">A ndarray/symbol of type `float32`</param>
/// <param name="min_range">The minimum scalar value possibly produced for the input</param>
/// <param name="max_range">The maximum scalar value possibly produced for the input</param>
/// <param name="out_type">Output data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribQuantize(NdArray @out,
NdArray input,
NdArray min_range,
NdArray max_range,
ContribQuantizeOutType out_type=ContribQuantizeOutType.Uint8)
{
return new Operator("_contrib_quantize")
.SetParam("out_type", Util.EnumToString<ContribQuantizeOutType>(out_type,ContribQuantizeOutTypeConvert))
.SetInput("input", input)
.SetInput("min_range", min_range)
.SetInput("max_range", max_range)
.Invoke(@out);
}
/// <summary>
/// Quantize a input tensor from float to `out_type`,with user-specified `min_range` and `max_range`.[min_range, max_range] are scalar floats that spcify the range forthe input data. Each value of the tensor will undergo the following:`out[i] = (in[i] - min_range) * range(OUTPUT_TYPE) / (max_range - min_range)`here `range(T) = numeric_limits<T>::max() - numeric_limits<T>::min()`Defined in I:\workspace\incubator-mxnet\src\operator\contrib\quantize.cc:L41
/// </summary>
/// <param name="input">A ndarray/symbol of type `float32`</param>
/// <param name="min_range">The minimum scalar value possibly produced for the input</param>
/// <param name="max_range">The maximum scalar value possibly produced for the input</param>
/// <param name="out_type">Output data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribQuantize(NdArray input,
NdArray min_range,
NdArray max_range,
ContribQuantizeOutType out_type=ContribQuantizeOutType.Uint8)
{
return new Operator("_contrib_quantize")
.SetParam("out_type", Util.EnumToString<ContribQuantizeOutType>(out_type,ContribQuantizeOutTypeConvert))
.SetInput("input", input)
.SetInput("min_range", min_range)
.SetInput("max_range", max_range)
.Invoke();
}
/// <summary>
/// Calculate cross entropy of softmax output and one-hot label.- This operator computes the cross entropy in two steps:  - Applies softmax function on the input array.  - Computes and returns the cross entropy loss between the softmax output and the labels.- The softmax function and cross entropy loss is given by:  - Softmax Function:  .. math:: \text{softmax}(x)_i = \frac{exp(x_i)}{\sum_j exp(x_j)}  - Cross Entropy Function:  .. math:: \text{CE(label, output)} = - \sum_i \text{label}_i \log(\text{output}_i)Example::  x = [[1, 2, 3],       [11, 7, 5]]  label = [2, 0]  softmax(x) = [[0.09003057, 0.24472848, 0.66524094],                [0.97962922, 0.01794253, 0.00242826]]  softmax_cross_entropy(data, label) = - log(0.66524084) - log(0.97962922) = 0.4281871Defined in I:\workspace\incubator-mxnet\src\operator\loss_binary_op.cc:L59
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data</param>
/// <param name="label">Input label</param>
 /// <returns>returns new symbol</returns>
public static NdArray SoftmaxCrossEntropy(NdArray @out,
NdArray data,
NdArray label)
{
return new Operator("softmax_cross_entropy")
.SetInput("data", data)
.SetInput("label", label)
.Invoke(@out);
}
/// <summary>
/// Calculate cross entropy of softmax output and one-hot label.- This operator computes the cross entropy in two steps:  - Applies softmax function on the input array.  - Computes and returns the cross entropy loss between the softmax output and the labels.- The softmax function and cross entropy loss is given by:  - Softmax Function:  .. math:: \text{softmax}(x)_i = \frac{exp(x_i)}{\sum_j exp(x_j)}  - Cross Entropy Function:  .. math:: \text{CE(label, output)} = - \sum_i \text{label}_i \log(\text{output}_i)Example::  x = [[1, 2, 3],       [11, 7, 5]]  label = [2, 0]  softmax(x) = [[0.09003057, 0.24472848, 0.66524094],                [0.97962922, 0.01794253, 0.00242826]]  softmax_cross_entropy(data, label) = - log(0.66524084) - log(0.97962922) = 0.4281871Defined in I:\workspace\incubator-mxnet\src\operator\loss_binary_op.cc:L59
/// </summary>
/// <param name="data">Input data</param>
/// <param name="label">Input label</param>
 /// <returns>returns new symbol</returns>
public static NdArray SoftmaxCrossEntropy(NdArray data,
NdArray label)
{
return new Operator("softmax_cross_entropy")
.SetInput("data", data)
.SetInput("label", label)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmaxCrossEntropy(NdArray @out)
{
return new Operator("_backward_softmax_cross_entropy")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmaxCrossEntropy()
{
return new Operator("_backward_softmax_cross_entropy")
.Invoke();
}
/// <summary>
/// Applies the softmax function.The resulting array contains elements in the range (0,1) and the elements along the given axis sum up to 1... math::   softmax(\mathbf{z})_j = \frac{e^{z_j}}{\sum_{k=1}^K e^{z_k}}for :math:`j = 1, ..., K`Example::  x = [[ 1.  1.  1.]       [ 1.  1.  1.]]  softmax(x,axis=0) = [[ 0.5  0.5  0.5]                       [ 0.5  0.5  0.5]]  softmax(x,axis=1) = [[ 0.33333334,  0.33333334,  0.33333334],                       [ 0.33333334,  0.33333334,  0.33333334]]Defined in I:\workspace\incubator-mxnet\src\operator\nn\softmax.cc:L54
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
/// <param name="axis">The axis along which to compute softmax.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Softmax(NdArray @out,
NdArray data,
int axis=-1)
{
return new Operator("softmax")
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Applies the softmax function.The resulting array contains elements in the range (0,1) and the elements along the given axis sum up to 1... math::   softmax(\mathbf{z})_j = \frac{e^{z_j}}{\sum_{k=1}^K e^{z_k}}for :math:`j = 1, ..., K`Example::  x = [[ 1.  1.  1.]       [ 1.  1.  1.]]  softmax(x,axis=0) = [[ 0.5  0.5  0.5]                       [ 0.5  0.5  0.5]]  softmax(x,axis=1) = [[ 0.33333334,  0.33333334,  0.33333334],                       [ 0.33333334,  0.33333334,  0.33333334]]Defined in I:\workspace\incubator-mxnet\src\operator\nn\softmax.cc:L54
/// </summary>
/// <param name="data">The input array.</param>
/// <param name="axis">The axis along which to compute softmax.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Softmax(NdArray data,
int axis=-1)
{
return new Operator("softmax")
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmax(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_softmax")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmax(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_softmax")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Computes the log softmax of the input.This is equivalent to computing softmax followed by log.Examples::  >>> x = mx.nd.array([1, 2, .1])  >>> mx.nd.log_softmax(x).asnumpy()  array([-1.41702998, -0.41702995, -2.31702995], dtype=float32)  >>> x = mx.nd.array( [[1, 2, .1],[.1, 2, 1]] )  >>> mx.nd.log_softmax(x, axis=0).asnumpy()  array([[-0.34115392, -0.69314718, -1.24115396],         [-1.24115396, -0.69314718, -0.34115392]], dtype=float32)
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
/// <param name="axis">The axis along which to compute softmax.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LogSoftmax(NdArray @out,
NdArray data,
int axis=-1)
{
return new Operator("log_softmax")
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the log softmax of the input.This is equivalent to computing softmax followed by log.Examples::  >>> x = mx.nd.array([1, 2, .1])  >>> mx.nd.log_softmax(x).asnumpy()  array([-1.41702998, -0.41702995, -2.31702995], dtype=float32)  >>> x = mx.nd.array( [[1, 2, .1],[.1, 2, 1]] )  >>> mx.nd.log_softmax(x, axis=0).asnumpy()  array([[-0.34115392, -0.69314718, -1.24115396],         [-1.24115396, -0.69314718, -0.34115392]], dtype=float32)
/// </summary>
/// <param name="data">The input array.</param>
/// <param name="axis">The axis along which to compute softmax.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LogSoftmax(NdArray data,
int axis=-1)
{
return new Operator("log_softmax")
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLogSoftmax(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log_softmax")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLogSoftmax(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log_softmax")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Updater function for multi-precision sgd optimizer
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="grad">gradient</param>
/// <param name="weight32">Weight32</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray MpSgdUpdate(NdArray @out,
NdArray grad,
NdArray weight32,
float lr,
NdArray weight=null,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("mp_sgd_update")
.SetParam("lr", lr)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("weight32", weight32)
.Invoke(@out);
}
/// <summary>
/// Updater function for multi-precision sgd optimizer
/// </summary>
/// <param name="grad">gradient</param>
/// <param name="weight32">Weight32</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray MpSgdUpdate(NdArray grad,
NdArray weight32,
float lr,
NdArray weight=null,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("mp_sgd_update")
.SetParam("lr", lr)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("weight32", weight32)
.Invoke();
}
/// <summary>
/// Updater function for multi-precision sgd optimizer
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="grad">Gradient</param>
/// <param name="mom">Momentum</param>
/// <param name="weight32">Weight32</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="momentum">The decay rate of momentum estimates at each epoch.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray MpSgdMomUpdate(NdArray @out,
NdArray grad,
NdArray mom,
NdArray weight32,
float lr,
NdArray weight=null,
float momentum=0f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("mp_sgd_mom_update")
.SetParam("lr", lr)
.SetParam("momentum", momentum)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("mom", mom)
.SetInput("weight32", weight32)
.Invoke(@out);
}
/// <summary>
/// Updater function for multi-precision sgd optimizer
/// </summary>
/// <param name="grad">Gradient</param>
/// <param name="mom">Momentum</param>
/// <param name="weight32">Weight32</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="momentum">The decay rate of momentum estimates at each epoch.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray MpSgdMomUpdate(NdArray grad,
NdArray mom,
NdArray weight32,
float lr,
NdArray weight=null,
float momentum=0f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("mp_sgd_mom_update")
.SetParam("lr", lr)
.SetParam("momentum", momentum)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("mom", mom)
.SetInput("weight32", weight32)
.Invoke();
}
/// <summary>
/// The FTML optimizer described in*FTML - Follow the Moving Leader in Deep Learning*,available at http://proceedings.mlr.press/v70/zheng17a/zheng17a.pdf... math:: g_t = \nabla J(W_{t-1})\\ v_t = \beta_2 v_{t-1} + (1 - \beta_2) g_t^2\\ d_t = \frac{ (1 - \beta_1^t) }{ \eta_t } (\sqrt{ \frac{ v_t }{ 1 - \beta_2^t } } + \epsilon) \sigma_t = d_t - \beta_1 d_{t-1} z_t = \beta_1 z_{ t-1 } + (1 - \beta_1^t) g_t - \sigma_t W_{t-1} W_t = - \frac{ z_t }{ d_t }Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L219
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="grad">Gradient</param>
/// <param name="d">Internal state ``d_t``</param>
/// <param name="v">Internal state ``v_t``</param>
/// <param name="z">Internal state ``z_t``</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="beta1">The decay rate for the 1st moment estimates.</param>
/// <param name="beta2">The decay rate for the 2nd moment estimates.</param>
/// <param name="epsilon">A small constant for numerical stability.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray FtmlUpdate(NdArray @out,
NdArray grad,
NdArray d,
NdArray v,
NdArray z,
float lr,
NdArray weight=null,
float beta1=0.9f,
float beta2=0.999f,
float epsilon=1e-08f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("ftml_update")
.SetParam("lr", lr)
.SetParam("beta1", beta1)
.SetParam("beta2", beta2)
.SetParam("epsilon", epsilon)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("d", d)
.SetInput("v", v)
.SetInput("z", z)
.Invoke(@out);
}
/// <summary>
/// The FTML optimizer described in*FTML - Follow the Moving Leader in Deep Learning*,available at http://proceedings.mlr.press/v70/zheng17a/zheng17a.pdf... math:: g_t = \nabla J(W_{t-1})\\ v_t = \beta_2 v_{t-1} + (1 - \beta_2) g_t^2\\ d_t = \frac{ (1 - \beta_1^t) }{ \eta_t } (\sqrt{ \frac{ v_t }{ 1 - \beta_2^t } } + \epsilon) \sigma_t = d_t - \beta_1 d_{t-1} z_t = \beta_1 z_{ t-1 } + (1 - \beta_1^t) g_t - \sigma_t W_{t-1} W_t = - \frac{ z_t }{ d_t }Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L219
/// </summary>
/// <param name="grad">Gradient</param>
/// <param name="d">Internal state ``d_t``</param>
/// <param name="v">Internal state ``v_t``</param>
/// <param name="z">Internal state ``z_t``</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="beta1">The decay rate for the 1st moment estimates.</param>
/// <param name="beta2">The decay rate for the 2nd moment estimates.</param>
/// <param name="epsilon">A small constant for numerical stability.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray FtmlUpdate(NdArray grad,
NdArray d,
NdArray v,
NdArray z,
float lr,
NdArray weight=null,
float beta1=0.9f,
float beta2=0.999f,
float epsilon=1e-08f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("ftml_update")
.SetParam("lr", lr)
.SetParam("beta1", beta1)
.SetParam("beta2", beta2)
.SetParam("epsilon", epsilon)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("d", d)
.SetInput("v", v)
.SetInput("z", z)
.Invoke();
}
/// <summary>
/// Update function for Adam optimizer. Adam is seen as a generalizationof AdaGrad.Adam update consists of the following steps, where g represents gradient and m, vare 1st and 2nd order moment estimates (mean and variance)... math:: g_t = \nabla J(W_{t-1})\\ m_t = \beta_1 m_{t-1} + (1 - \beta_1) g_t\\ v_t = \beta_2 v_{t-1} + (1 - \beta_2) g_t^2\\ W_t = W_{t-1} - \alpha \frac{ m_t }{ \sqrt{ v_t } + \epsilon }It updates the weights using:: m = beta1*m + (1-beta1)*grad v = beta2*v + (1-beta2)*(grad**2) w += - learning_rate * m / (sqrt(v) + epsilon)If w, m and v are all of ``row_sparse`` storage type,only the row slices whose indices appear in grad.indices are updated (for w, m and v):: for row in grad.indices:     m[row] = beta1*m[row] + (1-beta1)*grad[row]     v[row] = beta2*v[row] + (1-beta2)*(grad[row]**2)     w[row] += - learning_rate * m[row] / (sqrt(v[row]) + epsilon)Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L266
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="grad">Gradient</param>
/// <param name="mean">Moving mean</param>
/// <param name="var">Moving variance</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="beta1">The decay rate for the 1st moment estimates.</param>
/// <param name="beta2">The decay rate for the 2nd moment estimates.</param>
/// <param name="epsilon">A small constant for numerical stability.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray AdamUpdate(NdArray @out,
NdArray grad,
NdArray mean,
NdArray var,
float lr,
NdArray weight=null,
float beta1=0.9f,
float beta2=0.999f,
float epsilon=1e-08f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("adam_update")
.SetParam("lr", lr)
.SetParam("beta1", beta1)
.SetParam("beta2", beta2)
.SetParam("epsilon", epsilon)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("mean", mean)
.SetInput("var", var)
.Invoke(@out);
}
/// <summary>
/// Update function for Adam optimizer. Adam is seen as a generalizationof AdaGrad.Adam update consists of the following steps, where g represents gradient and m, vare 1st and 2nd order moment estimates (mean and variance)... math:: g_t = \nabla J(W_{t-1})\\ m_t = \beta_1 m_{t-1} + (1 - \beta_1) g_t\\ v_t = \beta_2 v_{t-1} + (1 - \beta_2) g_t^2\\ W_t = W_{t-1} - \alpha \frac{ m_t }{ \sqrt{ v_t } + \epsilon }It updates the weights using:: m = beta1*m + (1-beta1)*grad v = beta2*v + (1-beta2)*(grad**2) w += - learning_rate * m / (sqrt(v) + epsilon)If w, m and v are all of ``row_sparse`` storage type,only the row slices whose indices appear in grad.indices are updated (for w, m and v):: for row in grad.indices:     m[row] = beta1*m[row] + (1-beta1)*grad[row]     v[row] = beta2*v[row] + (1-beta2)*(grad[row]**2)     w[row] += - learning_rate * m[row] / (sqrt(v[row]) + epsilon)Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L266
/// </summary>
/// <param name="grad">Gradient</param>
/// <param name="mean">Moving mean</param>
/// <param name="var">Moving variance</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="beta1">The decay rate for the 1st moment estimates.</param>
/// <param name="beta2">The decay rate for the 2nd moment estimates.</param>
/// <param name="epsilon">A small constant for numerical stability.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray AdamUpdate(NdArray grad,
NdArray mean,
NdArray var,
float lr,
NdArray weight=null,
float beta1=0.9f,
float beta2=0.999f,
float epsilon=1e-08f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("adam_update")
.SetParam("lr", lr)
.SetParam("beta1", beta1)
.SetParam("beta2", beta2)
.SetParam("epsilon", epsilon)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("mean", mean)
.SetInput("var", var)
.Invoke();
}
/// <summary>
/// Update function for `RMSProp` optimizer.`RMSprop` is a variant of stochastic gradient descent where the gradients aredivided by a cache which grows with the sum of squares of recent gradients?`RMSProp` is similar to `AdaGrad`, a popular variant of `SGD` which adaptivelytunes the learning rate of each parameter. `AdaGrad` lowers the learning rate foreach parameter monotonically over the course of training.While this is analytically motivated for convex optimizations, it may not be idealfor non-convex problems. `RMSProp` deals with this heuristically by allowing thelearning rates to rebound as the denominator decays over time.Define the Root Mean Square (RMS) error criterion of the gradient as:math:`RMS[g]_t = \sqrt{E[g^2]_t + \epsilon}`, where :math:`g` representsgradient and :math:`E[g^2]_t` is the decaying average over past squared gradient.The :math:`E[g^2]_t` is given by:.. math::  E[g^2]_t = \gamma * E[g^2]_{t-1} + (1-\gamma) * g_t^2The update step is.. math::  \theta_{t+1} = \theta_t - \frac{\eta}{RMS[g]_t} g_tThe RMSProp code follows the version inhttp://www.cs.toronto.edu/~tijmen/csc321/slides/lecture_slides_lec6.pdfTieleman & Hinton, 2012.Hinton suggests the momentum term :math:`\gamma` to be 0.9 and the learning rate:math:`\eta` to be 0.001.Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L320
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="grad">Gradient</param>
/// <param name="n">n</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="gamma1">The decay rate of momentum estimates.</param>
/// <param name="epsilon">A small constant for numerical stability.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
/// <param name="clip_weights">Clip weights to the range of [-clip_weights, clip_weights] If clip_weights <= 0, weight clipping is turned off. weights = max(min(weights, clip_weights), -clip_weights).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RmspropUpdate(NdArray @out,
NdArray grad,
NdArray n,
float lr,
NdArray weight=null,
float gamma1=0.95f,
float epsilon=1e-08f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f,
float clip_weights=-1f)
{
return new Operator("rmsprop_update")
.SetParam("lr", lr)
.SetParam("gamma1", gamma1)
.SetParam("epsilon", epsilon)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetParam("clip_weights", clip_weights)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("n", n)
.Invoke(@out);
}
/// <summary>
/// Update function for `RMSProp` optimizer.`RMSprop` is a variant of stochastic gradient descent where the gradients aredivided by a cache which grows with the sum of squares of recent gradients?`RMSProp` is similar to `AdaGrad`, a popular variant of `SGD` which adaptivelytunes the learning rate of each parameter. `AdaGrad` lowers the learning rate foreach parameter monotonically over the course of training.While this is analytically motivated for convex optimizations, it may not be idealfor non-convex problems. `RMSProp` deals with this heuristically by allowing thelearning rates to rebound as the denominator decays over time.Define the Root Mean Square (RMS) error criterion of the gradient as:math:`RMS[g]_t = \sqrt{E[g^2]_t + \epsilon}`, where :math:`g` representsgradient and :math:`E[g^2]_t` is the decaying average over past squared gradient.The :math:`E[g^2]_t` is given by:.. math::  E[g^2]_t = \gamma * E[g^2]_{t-1} + (1-\gamma) * g_t^2The update step is.. math::  \theta_{t+1} = \theta_t - \frac{\eta}{RMS[g]_t} g_tThe RMSProp code follows the version inhttp://www.cs.toronto.edu/~tijmen/csc321/slides/lecture_slides_lec6.pdfTieleman & Hinton, 2012.Hinton suggests the momentum term :math:`\gamma` to be 0.9 and the learning rate:math:`\eta` to be 0.001.Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L320
/// </summary>
/// <param name="grad">Gradient</param>
/// <param name="n">n</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="gamma1">The decay rate of momentum estimates.</param>
/// <param name="epsilon">A small constant for numerical stability.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
/// <param name="clip_weights">Clip weights to the range of [-clip_weights, clip_weights] If clip_weights <= 0, weight clipping is turned off. weights = max(min(weights, clip_weights), -clip_weights).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RmspropUpdate(NdArray grad,
NdArray n,
float lr,
NdArray weight=null,
float gamma1=0.95f,
float epsilon=1e-08f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f,
float clip_weights=-1f)
{
return new Operator("rmsprop_update")
.SetParam("lr", lr)
.SetParam("gamma1", gamma1)
.SetParam("epsilon", epsilon)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetParam("clip_weights", clip_weights)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("n", n)
.Invoke();
}
/// <summary>
/// Update function for RMSPropAlex optimizer.`RMSPropAlex` is non-centered version of `RMSProp`.Define :math:`E[g^2]_t` is the decaying average over past squared gradient and:math:`E[g]_t` is the decaying average over past gradient... math::  E[g^2]_t = \gamma_1 * E[g^2]_{t-1} + (1 - \gamma_1) * g_t^2\\  E[g]_t = \gamma_1 * E[g]_{t-1} + (1 - \gamma_1) * g_t\\  \Delta_t = \gamma_2 * \Delta_{t-1} - \frac{\eta}{\sqrt{E[g^2]_t - E[g]_t^2 + \epsilon}} g_t\\The update step is.. math::  \theta_{t+1} = \theta_t + \Delta_tThe RMSPropAlex code follows the version inhttp://arxiv.org/pdf/1308.0850v5.pdf Eq(38) - Eq(45) by Alex Graves, 2013.Graves suggests the momentum term :math:`\gamma_1` to be 0.95, :math:`\gamma_2`to be 0.9 and the learning rate :math:`\eta` to be 0.0001.Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L359
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="grad">Gradient</param>
/// <param name="n">n</param>
/// <param name="g">g</param>
/// <param name="delta">delta</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="gamma1">Decay rate.</param>
/// <param name="gamma2">Decay rate.</param>
/// <param name="epsilon">A small constant for numerical stability.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
/// <param name="clip_weights">Clip weights to the range of [-clip_weights, clip_weights] If clip_weights <= 0, weight clipping is turned off. weights = max(min(weights, clip_weights), -clip_weights).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RmspropalexUpdate(NdArray @out,
NdArray grad,
NdArray n,
NdArray g,
NdArray delta,
float lr,
NdArray weight=null,
float gamma1=0.95f,
float gamma2=0.9f,
float epsilon=1e-08f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f,
float clip_weights=-1f)
{
return new Operator("rmspropalex_update")
.SetParam("lr", lr)
.SetParam("gamma1", gamma1)
.SetParam("gamma2", gamma2)
.SetParam("epsilon", epsilon)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetParam("clip_weights", clip_weights)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("n", n)
.SetInput("g", g)
.SetInput("delta", delta)
.Invoke(@out);
}
/// <summary>
/// Update function for RMSPropAlex optimizer.`RMSPropAlex` is non-centered version of `RMSProp`.Define :math:`E[g^2]_t` is the decaying average over past squared gradient and:math:`E[g]_t` is the decaying average over past gradient... math::  E[g^2]_t = \gamma_1 * E[g^2]_{t-1} + (1 - \gamma_1) * g_t^2\\  E[g]_t = \gamma_1 * E[g]_{t-1} + (1 - \gamma_1) * g_t\\  \Delta_t = \gamma_2 * \Delta_{t-1} - \frac{\eta}{\sqrt{E[g^2]_t - E[g]_t^2 + \epsilon}} g_t\\The update step is.. math::  \theta_{t+1} = \theta_t + \Delta_tThe RMSPropAlex code follows the version inhttp://arxiv.org/pdf/1308.0850v5.pdf Eq(38) - Eq(45) by Alex Graves, 2013.Graves suggests the momentum term :math:`\gamma_1` to be 0.95, :math:`\gamma_2`to be 0.9 and the learning rate :math:`\eta` to be 0.0001.Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L359
/// </summary>
/// <param name="grad">Gradient</param>
/// <param name="n">n</param>
/// <param name="g">g</param>
/// <param name="delta">delta</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="gamma1">Decay rate.</param>
/// <param name="gamma2">Decay rate.</param>
/// <param name="epsilon">A small constant for numerical stability.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
/// <param name="clip_weights">Clip weights to the range of [-clip_weights, clip_weights] If clip_weights <= 0, weight clipping is turned off. weights = max(min(weights, clip_weights), -clip_weights).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RmspropalexUpdate(NdArray grad,
NdArray n,
NdArray g,
NdArray delta,
float lr,
NdArray weight=null,
float gamma1=0.95f,
float gamma2=0.9f,
float epsilon=1e-08f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f,
float clip_weights=-1f)
{
return new Operator("rmspropalex_update")
.SetParam("lr", lr)
.SetParam("gamma1", gamma1)
.SetParam("gamma2", gamma2)
.SetParam("epsilon", epsilon)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetParam("clip_weights", clip_weights)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("n", n)
.SetInput("g", g)
.SetInput("delta", delta)
.Invoke();
}
/// <summary>
/// Update function for Ftrl optimizer.Referenced from *Ad Click Prediction: a View from the Trenches*, available athttp://dl.acm.org/citation.cfm?id=2488200.It updates the weights using:: rescaled_grad = clip(grad * rescale_grad, clip_gradient) z += rescaled_grad - (sqrt(n + rescaled_grad**2) - sqrt(n)) * weight / learning_rate n += rescaled_grad**2 w = (sign(z) * lamda1 - z) / ((beta + sqrt(n)) / learning_rate + wd) * (abs(z) > lamda1)If w, z and n are all of ``row_sparse`` storage type,only the row slices whose indices appear in grad.indices are updated (for w, z and n):: for row in grad.indices:     rescaled_grad[row] = clip(grad[row] * rescale_grad, clip_gradient)     z[row] += rescaled_grad[row] - (sqrt(n[row] + rescaled_grad[row]**2) - sqrt(n[row])) * weight[row] / learning_rate     n[row] += rescaled_grad[row]**2     w[row] = (sign(z[row]) * lamda1 - z[row]) / ((beta + sqrt(n[row])) / learning_rate + wd) * (abs(z[row]) > lamda1)Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L399
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="grad">Gradient</param>
/// <param name="z">z</param>
/// <param name="n">Square of grad</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="lamda1">The L1 regularization coefficient.</param>
/// <param name="beta">Per-Coordinate Learning Rate beta.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray FtrlUpdate(NdArray @out,
NdArray grad,
NdArray z,
NdArray n,
float lr,
NdArray weight=null,
float lamda1=0.01f,
float beta=1f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("ftrl_update")
.SetParam("lr", lr)
.SetParam("lamda1", lamda1)
.SetParam("beta", beta)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("z", z)
.SetInput("n", n)
.Invoke(@out);
}
/// <summary>
/// Update function for Ftrl optimizer.Referenced from *Ad Click Prediction: a View from the Trenches*, available athttp://dl.acm.org/citation.cfm?id=2488200.It updates the weights using:: rescaled_grad = clip(grad * rescale_grad, clip_gradient) z += rescaled_grad - (sqrt(n + rescaled_grad**2) - sqrt(n)) * weight / learning_rate n += rescaled_grad**2 w = (sign(z) * lamda1 - z) / ((beta + sqrt(n)) / learning_rate + wd) * (abs(z) > lamda1)If w, z and n are all of ``row_sparse`` storage type,only the row slices whose indices appear in grad.indices are updated (for w, z and n):: for row in grad.indices:     rescaled_grad[row] = clip(grad[row] * rescale_grad, clip_gradient)     z[row] += rescaled_grad[row] - (sqrt(n[row] + rescaled_grad[row]**2) - sqrt(n[row])) * weight[row] / learning_rate     n[row] += rescaled_grad[row]**2     w[row] = (sign(z[row]) * lamda1 - z[row]) / ((beta + sqrt(n[row])) / learning_rate + wd) * (abs(z[row]) > lamda1)Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L399
/// </summary>
/// <param name="grad">Gradient</param>
/// <param name="z">z</param>
/// <param name="n">Square of grad</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="lamda1">The L1 regularization coefficient.</param>
/// <param name="beta">Per-Coordinate Learning Rate beta.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray FtrlUpdate(NdArray grad,
NdArray z,
NdArray n,
float lr,
NdArray weight=null,
float lamda1=0.01f,
float beta=1f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("ftrl_update")
.SetParam("lr", lr)
.SetParam("lamda1", lamda1)
.SetParam("beta", beta)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("z", z)
.SetInput("n", n)
.Invoke();
}
/// <summary>
/// Update function for Stochastic Gradient Descent (SDG) optimizer.It updates the weights using:: weight = weight - learning_rate * gradientIf weight is of ``row_sparse`` storage type,only the row slices whose indices appear in grad.indices are updated:: for row in gradient.indices:     weight[row] = weight[row] - learning_rate * gradient[row]Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L105
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="grad">Gradient</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SgdUpdate(NdArray @out,
NdArray grad,
float lr,
NdArray weight=null,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("sgd_update")
.SetParam("lr", lr)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.Invoke(@out);
}
/// <summary>
/// Update function for Stochastic Gradient Descent (SDG) optimizer.It updates the weights using:: weight = weight - learning_rate * gradientIf weight is of ``row_sparse`` storage type,only the row slices whose indices appear in grad.indices are updated:: for row in gradient.indices:     weight[row] = weight[row] - learning_rate * gradient[row]Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L105
/// </summary>
/// <param name="grad">Gradient</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SgdUpdate(NdArray grad,
float lr,
NdArray weight=null,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("sgd_update")
.SetParam("lr", lr)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.Invoke();
}
/// <summary>
/// Momentum update function for Stochastic Gradient Descent (SDG) optimizer.Momentum update has better convergence rates on neural networks. Mathematically it lookslike below:.. math::  v_1 = \alpha * \nabla J(W_0)\\  v_t = \gamma v_{t-1} - \alpha * \nabla J(W_{t-1})\\  W_t = W_{t-1} + v_tIt updates the weights using::  v = momentum * v - learning_rate * gradient  weight += vWhere the parameter ``momentum`` is the decay rate of momentum estimates at each epoch.If weight and grad are both of ``row_sparse`` storage type and momentum is of ``default`` storage type,standard update is applied.If weight, grad and momentum are all of ``row_sparse`` storage type,only the row slices whose indices appear in grad.indices are updated (for both weight and momentum)::  for row in gradient.indices:      v[row] = momentum[row] * v[row] - learning_rate * gradient[row]      weight[row] += v[row]Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L148
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="grad">Gradient</param>
/// <param name="mom">Momentum</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="momentum">The decay rate of momentum estimates at each epoch.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SgdMomUpdate(NdArray @out,
NdArray grad,
NdArray mom,
float lr,
NdArray weight=null,
float momentum=0f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("sgd_mom_update")
.SetParam("lr", lr)
.SetParam("momentum", momentum)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("mom", mom)
.Invoke(@out);
}
/// <summary>
/// Momentum update function for Stochastic Gradient Descent (SDG) optimizer.Momentum update has better convergence rates on neural networks. Mathematically it lookslike below:.. math::  v_1 = \alpha * \nabla J(W_0)\\  v_t = \gamma v_{t-1} - \alpha * \nabla J(W_{t-1})\\  W_t = W_{t-1} + v_tIt updates the weights using::  v = momentum * v - learning_rate * gradient  weight += vWhere the parameter ``momentum`` is the decay rate of momentum estimates at each epoch.If weight and grad are both of ``row_sparse`` storage type and momentum is of ``default`` storage type,standard update is applied.If weight, grad and momentum are all of ``row_sparse`` storage type,only the row slices whose indices appear in grad.indices are updated (for both weight and momentum)::  for row in gradient.indices:      v[row] = momentum[row] * v[row] - learning_rate * gradient[row]      weight[row] += v[row]Defined in I:\workspace\incubator-mxnet\src\operator\optimizer_op.cc:L148
/// </summary>
/// <param name="grad">Gradient</param>
/// <param name="mom">Momentum</param>
/// <param name="lr">Learning rate</param>
/// <param name="weight">Weight</param>
/// <param name="momentum">The decay rate of momentum estimates at each epoch.</param>
/// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
/// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
/// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SgdMomUpdate(NdArray grad,
NdArray mom,
float lr,
NdArray weight=null,
float momentum=0f,
float wd=0f,
float rescale_grad=1f,
float clip_gradient=-1f)
{
return new Operator("sgd_mom_update")
.SetParam("lr", lr)
.SetParam("momentum", momentum)
.SetParam("wd", wd)
.SetParam("rescale_grad", rescale_grad)
.SetParam("clip_gradient", clip_gradient)
.SetInput("weight", weight)
.SetInput("grad", grad)
.SetInput("mom", mom)
.Invoke();
}
/// <summary>
/// Concurrent sampling from multipleuniform distributions on the intervals given by *[low,high)*.The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Examples::   low = [ 0.0, 2.5 ]   high = [ 1.0, 3.7 ]   // Draw a single sample for each distribution   sample_uniform(low, high) = [ 0.40451524,  3.18687344]   // Draw a vector containing two samples for each distribution   sample_uniform(low, high, shape=(2)) = [[ 0.40451524,  0.18017688],                                           [ 3.18687344,  3.68352246]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L277
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="low">Lower bounds of the distributions.</param>
/// <param name="high">Upper bounds of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleUniform(NdArray @out,
NdArray low,
NdArray high,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_uniform")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("low", low)
.SetInput("high", high)
.Invoke(@out);
}
/// <summary>
/// Concurrent sampling from multipleuniform distributions on the intervals given by *[low,high)*.The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Examples::   low = [ 0.0, 2.5 ]   high = [ 1.0, 3.7 ]   // Draw a single sample for each distribution   sample_uniform(low, high) = [ 0.40451524,  3.18687344]   // Draw a vector containing two samples for each distribution   sample_uniform(low, high, shape=(2)) = [[ 0.40451524,  0.18017688],                                           [ 3.18687344,  3.68352246]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L277
/// </summary>
/// <param name="low">Lower bounds of the distributions.</param>
/// <param name="high">Upper bounds of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleUniform(NdArray low,
NdArray high,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_uniform")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("low", low)
.SetInput("high", high)
.Invoke();
}
/// <summary>
/// Concurrent sampling from multiplenormal distributions with parameters *mu* (mean) and *sigma* (standard deviation).The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Examples::   mu = [ 0.0, 2.5 ]   sigma = [ 1.0, 3.7 ]   // Draw a single sample for each distribution   sample_normal(mu, sigma) = [-0.56410581,  0.95934606]   // Draw a vector containing two samples for each distribution   sample_normal(mu, sigma, shape=(2)) = [[-0.56410581,  0.2928229 ],                                          [ 0.95934606,  4.48287058]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L279
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="mu">Means of the distributions.</param>
/// <param name="sigma">Standard deviations of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleNormal(NdArray @out,
NdArray mu,
NdArray sigma,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_normal")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("mu", mu)
.SetInput("sigma", sigma)
.Invoke(@out);
}
/// <summary>
/// Concurrent sampling from multiplenormal distributions with parameters *mu* (mean) and *sigma* (standard deviation).The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Examples::   mu = [ 0.0, 2.5 ]   sigma = [ 1.0, 3.7 ]   // Draw a single sample for each distribution   sample_normal(mu, sigma) = [-0.56410581,  0.95934606]   // Draw a vector containing two samples for each distribution   sample_normal(mu, sigma, shape=(2)) = [[-0.56410581,  0.2928229 ],                                          [ 0.95934606,  4.48287058]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L279
/// </summary>
/// <param name="mu">Means of the distributions.</param>
/// <param name="sigma">Standard deviations of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleNormal(NdArray mu,
NdArray sigma,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_normal")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("mu", mu)
.SetInput("sigma", sigma)
.Invoke();
}
/// <summary>
/// Concurrent sampling from multiplegamma distributions with parameters *alpha* (shape) and *beta* (scale).The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Examples::   alpha = [ 0.0, 2.5 ]   beta = [ 1.0, 0.7 ]   // Draw a single sample for each distribution   sample_gamma(alpha, beta) = [ 0.        ,  2.25797319]   // Draw a vector containing two samples for each distribution   sample_gamma(alpha, beta, shape=(2)) = [[ 0.        ,  0.        ],                                           [ 2.25797319,  1.70734084]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L282
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="alpha">Alpha (shape) parameters of the distributions.</param>
/// <param name="beta">Beta (scale) parameters of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleGamma(NdArray @out,
NdArray alpha,
NdArray beta,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_gamma")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("alpha", alpha)
.SetInput("beta", beta)
.Invoke(@out);
}
/// <summary>
/// Concurrent sampling from multiplegamma distributions with parameters *alpha* (shape) and *beta* (scale).The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Examples::   alpha = [ 0.0, 2.5 ]   beta = [ 1.0, 0.7 ]   // Draw a single sample for each distribution   sample_gamma(alpha, beta) = [ 0.        ,  2.25797319]   // Draw a vector containing two samples for each distribution   sample_gamma(alpha, beta, shape=(2)) = [[ 0.        ,  0.        ],                                           [ 2.25797319,  1.70734084]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L282
/// </summary>
/// <param name="alpha">Alpha (shape) parameters of the distributions.</param>
/// <param name="beta">Beta (scale) parameters of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleGamma(NdArray alpha,
NdArray beta,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_gamma")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("alpha", alpha)
.SetInput("beta", beta)
.Invoke();
}
/// <summary>
/// Concurrent sampling from multipleexponential distributions with parameters lambda (rate).The parameters of the distributions are provided as an input array.Let *[s]* be the shape of the input array, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input array, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input value at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input array.Examples::   lam = [ 1.0, 8.5 ]   // Draw a single sample for each distribution   sample_exponential(lam) = [ 0.51837951,  0.09994757]   // Draw a vector containing two samples for each distribution   sample_exponential(lam, shape=(2)) = [[ 0.51837951,  0.19866663],                                         [ 0.09994757,  0.50447971]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L284
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lam">Lambda (rate) parameters of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleExponential(NdArray @out,
NdArray lam,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_exponential")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("lam", lam)
.Invoke(@out);
}
/// <summary>
/// Concurrent sampling from multipleexponential distributions with parameters lambda (rate).The parameters of the distributions are provided as an input array.Let *[s]* be the shape of the input array, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input array, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input value at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input array.Examples::   lam = [ 1.0, 8.5 ]   // Draw a single sample for each distribution   sample_exponential(lam) = [ 0.51837951,  0.09994757]   // Draw a vector containing two samples for each distribution   sample_exponential(lam, shape=(2)) = [[ 0.51837951,  0.19866663],                                         [ 0.09994757,  0.50447971]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L284
/// </summary>
/// <param name="lam">Lambda (rate) parameters of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleExponential(NdArray lam,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_exponential")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("lam", lam)
.Invoke();
}
/// <summary>
/// Concurrent sampling from multiplePoisson distributions with parameters lambda (rate).The parameters of the distributions are provided as an input array.Let *[s]* be the shape of the input array, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input array, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input value at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input array.Samples will always be returned as a floating point data type.Examples::   lam = [ 1.0, 8.5 ]   // Draw a single sample for each distribution   sample_poisson(lam) = [  0.,  13.]   // Draw a vector containing two samples for each distribution   sample_poisson(lam, shape=(2)) = [[  0.,   4.],                                     [ 13.,   8.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L286
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lam">Lambda (rate) parameters of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SamplePoisson(NdArray @out,
NdArray lam,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_poisson")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("lam", lam)
.Invoke(@out);
}
/// <summary>
/// Concurrent sampling from multiplePoisson distributions with parameters lambda (rate).The parameters of the distributions are provided as an input array.Let *[s]* be the shape of the input array, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input array, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input value at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input array.Samples will always be returned as a floating point data type.Examples::   lam = [ 1.0, 8.5 ]   // Draw a single sample for each distribution   sample_poisson(lam) = [  0.,  13.]   // Draw a vector containing two samples for each distribution   sample_poisson(lam, shape=(2)) = [[  0.,   4.],                                     [ 13.,   8.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L286
/// </summary>
/// <param name="lam">Lambda (rate) parameters of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SamplePoisson(NdArray lam,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_poisson")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("lam", lam)
.Invoke();
}
/// <summary>
/// Concurrent sampling from multiplenegative binomial distributions with parameters *k* (failure limit) and *p* (failure probability).The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Samples will always be returned as a floating point data type.Examples::   k = [ 20, 49 ]   p = [ 0.4 , 0.77 ]   // Draw a single sample for each distribution   sample_negative_binomial(k, p) = [ 15.,  16.]   // Draw a vector containing two samples for each distribution   sample_negative_binomial(k, p, shape=(2)) = [[ 15.,  50.],                                                [ 16.,  12.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L289
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="k">Limits of unsuccessful experiments.</param>
/// <param name="p">Failure probabilities in each experiment.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleNegativeBinomial(NdArray @out,
NdArray k,
NdArray p,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_negative_binomial")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("k", k)
.SetInput("p", p)
.Invoke(@out);
}
/// <summary>
/// Concurrent sampling from multiplenegative binomial distributions with parameters *k* (failure limit) and *p* (failure probability).The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Samples will always be returned as a floating point data type.Examples::   k = [ 20, 49 ]   p = [ 0.4 , 0.77 ]   // Draw a single sample for each distribution   sample_negative_binomial(k, p) = [ 15.,  16.]   // Draw a vector containing two samples for each distribution   sample_negative_binomial(k, p, shape=(2)) = [[ 15.,  50.],                                                [ 16.,  12.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L289
/// </summary>
/// <param name="k">Limits of unsuccessful experiments.</param>
/// <param name="p">Failure probabilities in each experiment.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleNegativeBinomial(NdArray k,
NdArray p,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_negative_binomial")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("k", k)
.SetInput("p", p)
.Invoke();
}
/// <summary>
/// Concurrent sampling from multiplegeneralized negative binomial distributions with parameters *mu* (mean) and *alpha* (dispersion).The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Samples will always be returned as a floating point data type.Examples::   mu = [ 2.0, 2.5 ]   alpha = [ 1.0, 0.1 ]   // Draw a single sample for each distribution   sample_generalized_negative_binomial(mu, alpha) = [ 0.,  3.]   // Draw a vector containing two samples for each distribution   sample_generalized_negative_binomial(mu, alpha, shape=(2)) = [[ 0.,  3.],                                                                 [ 3.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L293
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="mu">Means of the distributions.</param>
/// <param name="alpha">Alpha (dispersion) parameters of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleGeneralizedNegativeBinomial(NdArray @out,
NdArray mu,
NdArray alpha,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_generalized_negative_binomial")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("mu", mu)
.SetInput("alpha", alpha)
.Invoke(@out);
}
/// <summary>
/// Concurrent sampling from multiplegeneralized negative binomial distributions with parameters *mu* (mean) and *alpha* (dispersion).The parameters of the distributions are provided as input arrays.Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*be the shape specified as the parameter of the operator, and *m* be the dimensionof *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*will be an *m*-dimensional array that holds randomly drawn samples from the distributionwhich is parameterized by the input values at index *i*. If the shape parameter of theoperator is not set, then one sample will be drawn per distribution and the output arrayhas the same shape as the input arrays.Samples will always be returned as a floating point data type.Examples::   mu = [ 2.0, 2.5 ]   alpha = [ 1.0, 0.1 ]   // Draw a single sample for each distribution   sample_generalized_negative_binomial(mu, alpha) = [ 0.,  3.]   // Draw a vector containing two samples for each distribution   sample_generalized_negative_binomial(mu, alpha, shape=(2)) = [[ 0.,  3.],                                                                 [ 3.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\multisample_op.cc:L293
/// </summary>
/// <param name="mu">Means of the distributions.</param>
/// <param name="alpha">Alpha (dispersion) parameters of the distributions.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleGeneralizedNegativeBinomial(NdArray mu,
NdArray alpha,
Shape shape=null,
Dtype dtype=null)
{
return new Operator("_sample_generalized_negative_binomial")
.SetParam("shape", shape)
.SetParam("dtype", dtype)
.SetInput("mu", mu)
.SetInput("alpha", alpha)
.Invoke();
}
/// <summary>
/// Concurrent sampling from multiple multinomial distributions.*data* is an *n* dimensional array whose last dimension has length *k*, where*k* is the number of possible outcomes of each multinomial distribution. Thisoperator will draw *shape* samples from each distribution. If shape is emptyone sample will be drawn from each distribution.If *get_prob* is true, a second array containing log likelihood of the drawnsamples will also be returned. This is usually used for reinforcement learningwhere you can provide reward as head gradient for this array to estimategradient.Note that the input distribution must be normalized, i.e. *data* must sum to1 along its last axis.Examples::   probs = [[0, 0.1, 0.2, 0.3, 0.4], [0.4, 0.3, 0.2, 0.1, 0]]   // Draw a single sample for each distribution   sample_multinomial(probs) = [3, 0]   // Draw a vector containing two samples for each distribution   sample_multinomial(probs, shape=(2)) = [[4, 2],                                           [0, 0]]   // requests log likelihood   sample_multinomial(probs, get_prob=True) = [2, 1], [0.2, 0.3]
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Distribution probabilities. Must sum to one on the last axis.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="get_prob">Whether to also return the log probability of sampled result. This is usually used for differentiating through stochastic variables, e.g. in reinforcement learning.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Only support int32 for now.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleMultinomial(NdArray @out,
NdArray data,
Shape shape=null,
bool get_prob=false,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Int32;}

return new Operator("_sample_multinomial")
.SetParam("shape", shape)
.SetParam("get_prob", get_prob)
.SetParam("dtype", dtype)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Concurrent sampling from multiple multinomial distributions.*data* is an *n* dimensional array whose last dimension has length *k*, where*k* is the number of possible outcomes of each multinomial distribution. Thisoperator will draw *shape* samples from each distribution. If shape is emptyone sample will be drawn from each distribution.If *get_prob* is true, a second array containing log likelihood of the drawnsamples will also be returned. This is usually used for reinforcement learningwhere you can provide reward as head gradient for this array to estimategradient.Note that the input distribution must be normalized, i.e. *data* must sum to1 along its last axis.Examples::   probs = [[0, 0.1, 0.2, 0.3, 0.4], [0.4, 0.3, 0.2, 0.1, 0]]   // Draw a single sample for each distribution   sample_multinomial(probs) = [3, 0]   // Draw a vector containing two samples for each distribution   sample_multinomial(probs, shape=(2)) = [[4, 2],                                           [0, 0]]   // requests log likelihood   sample_multinomial(probs, get_prob=True) = [2, 1], [0.2, 0.3]
/// </summary>
/// <param name="data">Distribution probabilities. Must sum to one on the last axis.</param>
/// <param name="shape">Shape to be sampled from each random distribution.</param>
/// <param name="get_prob">Whether to also return the log probability of sampled result. This is usually used for differentiating through stochastic variables, e.g. in reinforcement learning.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Only support int32 for now.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SampleMultinomial(NdArray data,
Shape shape=null,
bool get_prob=false,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Int32;}

return new Operator("_sample_multinomial")
.SetParam("shape", shape)
.SetParam("get_prob", get_prob)
.SetParam("dtype", dtype)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSampleMultinomial(NdArray @out)
{
return new Operator("_backward_sample_multinomial")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSampleMultinomial()
{
return new Operator("_backward_sample_multinomial")
.Invoke();
}
/// <summary>
/// Draw random samples from an exponential distribution.Samples are distributed according to an exponential distribution parametrized by *lambda* (rate).Example::   exponential(lam=4, shape=(2,2)) = [[ 0.0097189 ,  0.08999364],                                      [ 0.04146638,  0.31715935]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L115
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lam">Lambda parameter (rate) of the exponential distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomExponential(NdArray @out,
float lam=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_exponential")
.SetParam("lam", lam)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// Draw random samples from an exponential distribution.Samples are distributed according to an exponential distribution parametrized by *lambda* (rate).Example::   exponential(lam=4, shape=(2,2)) = [[ 0.0097189 ,  0.08999364],                                      [ 0.04146638,  0.31715935]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L115
/// </summary>
/// <param name="lam">Lambda parameter (rate) of the exponential distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomExponential(float lam=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_exponential")
.SetParam("lam", lam)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// Draw random samples from a Poisson distribution.Samples are distributed according to a Poisson distribution parametrized by *lambda* (rate).Samples will always be returned as a floating point data type.Example::   poisson(lam=4, shape=(2,2)) = [[ 5.,  2.],                                  [ 4.,  6.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L132
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lam">Lambda parameter (rate) of the Poisson distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomPoisson(NdArray @out,
float lam=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_poisson")
.SetParam("lam", lam)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// Draw random samples from a Poisson distribution.Samples are distributed according to a Poisson distribution parametrized by *lambda* (rate).Samples will always be returned as a floating point data type.Example::   poisson(lam=4, shape=(2,2)) = [[ 5.,  2.],                                  [ 4.,  6.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L132
/// </summary>
/// <param name="lam">Lambda parameter (rate) of the Poisson distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomPoisson(float lam=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_poisson")
.SetParam("lam", lam)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// Draw random samples from a negative binomial distribution.Samples are distributed according to a negative binomial distribution parametrized by*k* (limit of unsuccessful experiments) and *p* (failure probability in each experiment).Samples will always be returned as a floating point data type.Example::   negative_binomial(k=3, p=0.4, shape=(2,2)) = [[ 4.,  7.],                                                 [ 2.,  5.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L149
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="k">Limit of unsuccessful experiments.</param>
/// <param name="p">Failure probability in each experiment.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomNegativeBinomial(NdArray @out,
int k=1,
float p=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_negative_binomial")
.SetParam("k", k)
.SetParam("p", p)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// Draw random samples from a negative binomial distribution.Samples are distributed according to a negative binomial distribution parametrized by*k* (limit of unsuccessful experiments) and *p* (failure probability in each experiment).Samples will always be returned as a floating point data type.Example::   negative_binomial(k=3, p=0.4, shape=(2,2)) = [[ 4.,  7.],                                                 [ 2.,  5.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L149
/// </summary>
/// <param name="k">Limit of unsuccessful experiments.</param>
/// <param name="p">Failure probability in each experiment.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomNegativeBinomial(int k=1,
float p=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_negative_binomial")
.SetParam("k", k)
.SetParam("p", p)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// Draw random samples from a generalized negative binomial distribution.Samples are distributed according to a generalized negative binomial distribution parametrized by*mu* (mean) and *alpha* (dispersion). *alpha* is defined as *1/k* where *k* is the failure limit of thenumber of unsuccessful experiments (generalized to real numbers).Samples will always be returned as a floating point data type.Example::   generalized_negative_binomial(mu=2.0, alpha=0.3, shape=(2,2)) = [[ 2.,  1.],                                                                    [ 6.,  4.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L168
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="mu">Mean of the negative binomial distribution.</param>
/// <param name="alpha">Alpha (dispersion) parameter of the negative binomial distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomGeneralizedNegativeBinomial(NdArray @out,
float mu=1f,
float alpha=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_generalized_negative_binomial")
.SetParam("mu", mu)
.SetParam("alpha", alpha)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// Draw random samples from a generalized negative binomial distribution.Samples are distributed according to a generalized negative binomial distribution parametrized by*mu* (mean) and *alpha* (dispersion). *alpha* is defined as *1/k* where *k* is the failure limit of thenumber of unsuccessful experiments (generalized to real numbers).Samples will always be returned as a floating point data type.Example::   generalized_negative_binomial(mu=2.0, alpha=0.3, shape=(2,2)) = [[ 2.,  1.],                                                                    [ 6.,  4.]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L168
/// </summary>
/// <param name="mu">Mean of the negative binomial distribution.</param>
/// <param name="alpha">Alpha (dispersion) parameter of the negative binomial distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomGeneralizedNegativeBinomial(float mu=1f,
float alpha=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_generalized_negative_binomial")
.SetParam("mu", mu)
.SetParam("alpha", alpha)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// Draw random samples from a uniform distribution... note:: The existing alias ``uniform`` is deprecated.Samples are uniformly distributed over the half-open interval *[low, high)*(includes *low*, but excludes *high*).Example::   uniform(low=0, high=1, shape=(2,2)) = [[ 0.60276335,  0.85794562],                                          [ 0.54488319,  0.84725171]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L66
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="low">Lower bound of the distribution.</param>
/// <param name="high">Upper bound of the distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomUniform(NdArray @out,
float low=0f,
float high=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_uniform")
.SetParam("low", low)
.SetParam("high", high)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// Draw random samples from a uniform distribution... note:: The existing alias ``uniform`` is deprecated.Samples are uniformly distributed over the half-open interval *[low, high)*(includes *low*, but excludes *high*).Example::   uniform(low=0, high=1, shape=(2,2)) = [[ 0.60276335,  0.85794562],                                          [ 0.54488319,  0.84725171]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L66
/// </summary>
/// <param name="low">Lower bound of the distribution.</param>
/// <param name="high">Upper bound of the distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomUniform(float low=0f,
float high=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_uniform")
.SetParam("low", low)
.SetParam("high", high)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// Draw random samples from a normal (Gaussian) distribution... note:: The existing alias ``normal`` is deprecated.Samples are distributed according to a normal distribution parametrized by *loc* (mean) and *scale* (standard deviation).Example::   normal(loc=0, scale=1, shape=(2,2)) = [[ 1.89171135, -1.16881478],                                          [-1.23474145,  1.55807114]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L85
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="loc">Mean of the distribution.</param>
/// <param name="scale">Standard deviation of the distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomNormal(NdArray @out,
float loc=0f,
float scale=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_normal")
.SetParam("loc", loc)
.SetParam("scale", scale)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// Draw random samples from a normal (Gaussian) distribution... note:: The existing alias ``normal`` is deprecated.Samples are distributed according to a normal distribution parametrized by *loc* (mean) and *scale* (standard deviation).Example::   normal(loc=0, scale=1, shape=(2,2)) = [[ 1.89171135, -1.16881478],                                          [-1.23474145,  1.55807114]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L85
/// </summary>
/// <param name="loc">Mean of the distribution.</param>
/// <param name="scale">Standard deviation of the distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomNormal(float loc=0f,
float scale=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_normal")
.SetParam("loc", loc)
.SetParam("scale", scale)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// Draw random samples from a gamma distribution.Samples are distributed according to a gamma distribution parametrized by *alpha* (shape) and *beta* (scale).Example::   gamma(alpha=9, beta=0.5, shape=(2,2)) = [[ 7.10486984,  3.37695289],                                            [ 3.91697288,  3.65933681]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L100
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="alpha">Alpha parameter (shape) of the gamma distribution.</param>
/// <param name="beta">Beta parameter (scale) of the gamma distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomGamma(NdArray @out,
float alpha=1f,
float beta=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_gamma")
.SetParam("alpha", alpha)
.SetParam("beta", beta)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// Draw random samples from a gamma distribution.Samples are distributed according to a gamma distribution parametrized by *alpha* (shape) and *beta* (scale).Example::   gamma(alpha=9, beta=0.5, shape=(2,2)) = [[ 7.10486984,  3.37695289],                                            [ 3.91697288,  3.65933681]]Defined in I:\workspace\incubator-mxnet\src\operator\random\sample_op.cc:L100
/// </summary>
/// <param name="alpha">Alpha parameter (shape) of the gamma distribution.</param>
/// <param name="beta">Beta parameter (scale) of the gamma distribution.</param>
/// <param name="shape">Shape of the output.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
/// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
 /// <returns>returns new symbol</returns>
public static NdArray RandomGamma(float alpha=1f,
float beta=1f,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{
return new Operator("_random_gamma")
.SetParam("alpha", alpha)
.SetParam("beta", beta)
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// Returns indices of the maximum values along an axis.In the case of multiple occurrences of maximum values, the indices corresponding to the first occurrenceare returned.Examples::  x = [[ 0.,  1.,  2.],       [ 3.,  4.,  5.]]  // argmax along axis 0  argmax(x, axis=0) = [ 1.,  1.,  1.]  // argmax along axis 1  argmax(x, axis=1) = [ 2.,  2.]  // argmax along axis 1 keeping same dims as an input array  argmax(x, axis=1, keepdims=True) = [[ 2.],                                      [ 2.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L52
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis along which to perform the reduction. Negative values means indexing from right to left. ``Requires axis to be set as int, because global reduction is not supported yet.``</param>
/// <param name="keepdims">If this is set to `True`, the reduced axis is left in the result as dimension with size one.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Argmax(NdArray @out,
NdArray data,
int? axis=null,
bool keepdims=false)
{
return new Operator("argmax")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns indices of the maximum values along an axis.In the case of multiple occurrences of maximum values, the indices corresponding to the first occurrenceare returned.Examples::  x = [[ 0.,  1.,  2.],       [ 3.,  4.,  5.]]  // argmax along axis 0  argmax(x, axis=0) = [ 1.,  1.,  1.]  // argmax along axis 1  argmax(x, axis=1) = [ 2.,  2.]  // argmax along axis 1 keeping same dims as an input array  argmax(x, axis=1, keepdims=True) = [[ 2.],                                      [ 2.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L52
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis along which to perform the reduction. Negative values means indexing from right to left. ``Requires axis to be set as int, because global reduction is not supported yet.``</param>
/// <param name="keepdims">If this is set to `True`, the reduced axis is left in the result as dimension with size one.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Argmax(NdArray data,
int? axis=null,
bool keepdims=false)
{
return new Operator("argmax")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns indices of the minimum values along an axis.In the case of multiple occurrences of minimum values, the indices corresponding to the first occurrenceare returned.Examples::  x = [[ 0.,  1.,  2.],       [ 3.,  4.,  5.]]  // argmin along axis 0  argmin(x, axis=0) = [ 0.,  0.,  0.]  // argmin along axis 1  argmin(x, axis=1) = [ 0.,  0.]  // argmin along axis 1 keeping same dims as an input array  argmin(x, axis=1, keepdims=True) = [[ 0.],                                      [ 0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L77
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis along which to perform the reduction. Negative values means indexing from right to left. ``Requires axis to be set as int, because global reduction is not supported yet.``</param>
/// <param name="keepdims">If this is set to `True`, the reduced axis is left in the result as dimension with size one.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Argmin(NdArray @out,
NdArray data,
int? axis=null,
bool keepdims=false)
{
return new Operator("argmin")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns indices of the minimum values along an axis.In the case of multiple occurrences of minimum values, the indices corresponding to the first occurrenceare returned.Examples::  x = [[ 0.,  1.,  2.],       [ 3.,  4.,  5.]]  // argmin along axis 0  argmin(x, axis=0) = [ 0.,  0.,  0.]  // argmin along axis 1  argmin(x, axis=1) = [ 0.,  0.]  // argmin along axis 1 keeping same dims as an input array  argmin(x, axis=1, keepdims=True) = [[ 0.],                                      [ 0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L77
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis along which to perform the reduction. Negative values means indexing from right to left. ``Requires axis to be set as int, because global reduction is not supported yet.``</param>
/// <param name="keepdims">If this is set to `True`, the reduced axis is left in the result as dimension with size one.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Argmin(NdArray data,
int? axis=null,
bool keepdims=false)
{
return new Operator("argmin")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns argmax indices of each channel from the input array.The result will be an NDArray of shape (num_channel,).In case of multiple occurrences of the maximum values, the indices corresponding to the first occurrenceare returned.Examples::  x = [[ 0.,  1.,  2.],       [ 3.,  4.,  5.]]  argmax_channel(x) = [ 2.,  2.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L97
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array</param>
 /// <returns>returns new symbol</returns>
public static NdArray ArgmaxChannel(NdArray @out,
NdArray data)
{
return new Operator("argmax_channel")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns argmax indices of each channel from the input array.The result will be an NDArray of shape (num_channel,).In case of multiple occurrences of the maximum values, the indices corresponding to the first occurrenceare returned.Examples::  x = [[ 0.,  1.,  2.],       [ 3.,  4.,  5.]]  argmax_channel(x) = [ 2.,  2.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L97
/// </summary>
/// <param name="data">The input array</param>
 /// <returns>returns new symbol</returns>
public static NdArray ArgmaxChannel(NdArray data)
{
return new Operator("argmax_channel")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Picks elements from an input array according to the input indices along the given axis.Given an input array of shape ``(d0, d1)`` and indices of shape ``(i0,)``, the result will bean output array of shape ``(i0,)`` with::  output[i] = input[i, indices[i]]By default, if any index mentioned is too large, it is replaced by the index that addressesthe last element along an axis (the `clip` mode).This function supports n-dimensional input and (n-1)-dimensional indices arrays.Examples::  x = [[ 1.,  2.],       [ 3.,  4.],       [ 5.,  6.]]  // picks elements with specified indices along axis 0  pick(x, y=[0,1], 0) = [ 1.,  4.]  // picks elements with specified indices along axis 1  pick(x, y=[0,1,0], 1) = [ 1.,  4.,  5.]  y = [[ 1.],       [ 0.],       [ 2.]]  // picks elements with specified indices along axis 1 and dims are maintained  pick(x,y, 1, keepdims=True) = [[ 2.],                                 [ 3.],                                 [ 6.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L145
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array</param>
/// <param name="index">The index array</param>
/// <param name="axis">The axis along which to perform the reduction. Negative values means indexing from right to left. ``Requires axis to be set as int, because global reduction is not supported yet.``</param>
/// <param name="keepdims">If this is set to `True`, the reduced axis is left in the result as dimension with size one.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Pick(NdArray @out,
NdArray data,
NdArray index,
int? axis=null,
bool keepdims=false)
{
return new Operator("pick")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetInput("data", data)
.SetInput("index", index)
.Invoke(@out);
}
/// <summary>
/// Picks elements from an input array according to the input indices along the given axis.Given an input array of shape ``(d0, d1)`` and indices of shape ``(i0,)``, the result will bean output array of shape ``(i0,)`` with::  output[i] = input[i, indices[i]]By default, if any index mentioned is too large, it is replaced by the index that addressesthe last element along an axis (the `clip` mode).This function supports n-dimensional input and (n-1)-dimensional indices arrays.Examples::  x = [[ 1.,  2.],       [ 3.,  4.],       [ 5.,  6.]]  // picks elements with specified indices along axis 0  pick(x, y=[0,1], 0) = [ 1.,  4.]  // picks elements with specified indices along axis 1  pick(x, y=[0,1,0], 1) = [ 1.,  4.,  5.]  y = [[ 1.],       [ 0.],       [ 2.]]  // picks elements with specified indices along axis 1 and dims are maintained  pick(x,y, 1, keepdims=True) = [[ 2.],                                 [ 3.],                                 [ 6.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L145
/// </summary>
/// <param name="data">The input array</param>
/// <param name="index">The index array</param>
/// <param name="axis">The axis along which to perform the reduction. Negative values means indexing from right to left. ``Requires axis to be set as int, because global reduction is not supported yet.``</param>
/// <param name="keepdims">If this is set to `True`, the reduced axis is left in the result as dimension with size one.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Pick(NdArray data,
NdArray index,
int? axis=null,
bool keepdims=false)
{
return new Operator("pick")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetInput("data", data)
.SetInput("index", index)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPick(NdArray @out)
{
return new Operator("_backward_pick")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPick()
{
return new Operator("_backward_pick")
.Invoke();
}
/// <summary>
/// Computes the sum of array elements over given axes... Note::  `sum` and `sum_axis` are equivalent.  For ndarray of csr storage type summation along axis 0 and axis 1 is supported.  Setting keepdims or exclude to True will cause a fallback to dense operator.Example::  data = [[[1,2],[2,3],[1,3]],          [[1,4],[4,3],[5,2]],          [[7,1],[7,2],[7,3]]]  sum(data, axis=1)  [[  4.   8.]   [ 10.   9.]   [ 21.   6.]]  sum(data, axis=[1,2])  [ 12.  19.  27.]  data = [[1,2,0],          [3,0,1],          [4,1,0]]  csr = cast_storage(data, 'csr')  sum(csr, axis=0)  [ 8.  3.  1.]  sum(csr, axis=1)  [ 3.  4.  5.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L85
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sum(NdArray @out,
NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("sum")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the sum of array elements over given axes... Note::  `sum` and `sum_axis` are equivalent.  For ndarray of csr storage type summation along axis 0 and axis 1 is supported.  Setting keepdims or exclude to True will cause a fallback to dense operator.Example::  data = [[[1,2],[2,3],[1,3]],          [[1,4],[4,3],[5,2]],          [[7,1],[7,2],[7,3]]]  sum(data, axis=1)  [[  4.   8.]   [ 10.   9.]   [ 21.   6.]]  sum(data, axis=[1,2])  [ 12.  19.  27.]  data = [[1,2,0],          [3,0,1],          [4,1,0]]  csr = cast_storage(data, 'csr')  sum(csr, axis=0)  [ 8.  3.  1.]  sum(csr, axis=1)  [ 3.  4.  5.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L85
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sum(NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("sum")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSum(NdArray @out)
{
return new Operator("_backward_sum")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSum()
{
return new Operator("_backward_sum")
.Invoke();
}
/// <summary>
/// Computes the mean of array elements over given axes.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L101
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Mean(NdArray @out,
NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("mean")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the mean of array elements over given axes.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L101
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Mean(NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("mean")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMean(NdArray @out)
{
return new Operator("_backward_mean")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMean()
{
return new Operator("_backward_mean")
.Invoke();
}
/// <summary>
/// Computes the product of array elements over given axes.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L116
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Prod(NdArray @out,
NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("prod")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the product of array elements over given axes.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L116
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Prod(NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("prod")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardProd(NdArray @out)
{
return new Operator("_backward_prod")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardProd()
{
return new Operator("_backward_prod")
.Invoke();
}
/// <summary>
/// Computes the sum of array elements over given axes treating Not a Numbers (``NaN``) as zero.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L131
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Nansum(NdArray @out,
NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("nansum")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the sum of array elements over given axes treating Not a Numbers (``NaN``) as zero.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L131
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Nansum(NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("nansum")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardNansum(NdArray @out)
{
return new Operator("_backward_nansum")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardNansum()
{
return new Operator("_backward_nansum")
.Invoke();
}
/// <summary>
/// Computes the product of array elements over given axes treating Not a Numbers (``NaN``) as one.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L146
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Nanprod(NdArray @out,
NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("nanprod")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the product of array elements over given axes treating Not a Numbers (``NaN``) as one.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L146
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Nanprod(NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("nanprod")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardNanprod(NdArray @out)
{
return new Operator("_backward_nanprod")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardNanprod()
{
return new Operator("_backward_nanprod")
.Invoke();
}
/// <summary>
/// Computes the max of array elements over given axes.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L160
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Max(NdArray @out,
NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("max")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the max of array elements over given axes.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L160
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Max(NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("max")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMax(NdArray @out)
{
return new Operator("_backward_max")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMax()
{
return new Operator("_backward_max")
.Invoke();
}
/// <summary>
/// Computes the min of array elements over given axes.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L174
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Min(NdArray @out,
NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("min")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the min of array elements over given axes.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L174
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Min(NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("min")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMin(NdArray @out)
{
return new Operator("_backward_min")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMin()
{
return new Operator("_backward_min")
.Invoke();
}
/// <summary>
/// Broadcasts the input array over particular axes.Broadcasting is allowed on axes with size 1, such as from `(2,1,3,1)` to`(2,8,3,9)`. Elements will be duplicated on the broadcasted axes.Example::   // given x of shape (1,2,1)   x = [[[ 1.],         [ 2.]]]   // broadcast x on on axis 2   broadcast_axis(x, axis=2, size=3) = [[[ 1.,  1.,  1.],                                         [ 2.,  2.,  2.]]]   // broadcast x on on axes 0 and 2   broadcast_axis(x, axis=(0,2), size=(2,3)) = [[[ 1.,  1.,  1.],                                                 [ 2.,  2.,  2.]],                                                [[ 1.,  1.,  1.],                                                 [ 2.,  2.,  2.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L207
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axes to perform the broadcasting.</param>
/// <param name="size">Target sizes of the broadcasting axes.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastAxis(NdArray @out,
NdArray data,
Shape axis=null,
Shape size=null)
{
return new Operator("broadcast_axis")
.SetParam("axis", axis)
.SetParam("size", size)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Broadcasts the input array over particular axes.Broadcasting is allowed on axes with size 1, such as from `(2,1,3,1)` to`(2,8,3,9)`. Elements will be duplicated on the broadcasted axes.Example::   // given x of shape (1,2,1)   x = [[[ 1.],         [ 2.]]]   // broadcast x on on axis 2   broadcast_axis(x, axis=2, size=3) = [[[ 1.,  1.,  1.],                                         [ 2.,  2.,  2.]]]   // broadcast x on on axes 0 and 2   broadcast_axis(x, axis=(0,2), size=(2,3)) = [[[ 1.,  1.,  1.],                                                 [ 2.,  2.,  2.]],                                                [[ 1.,  1.,  1.],                                                 [ 2.,  2.,  2.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L207
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axes to perform the broadcasting.</param>
/// <param name="size">Target sizes of the broadcasting axes.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastAxis(NdArray data,
Shape axis=null,
Shape size=null)
{
return new Operator("broadcast_axis")
.SetParam("axis", axis)
.SetParam("size", size)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Broadcasts the input array to a new shape.Broadcasting is a mechanism that allows NDArrays to perform arithmetic operationswith arrays of different shapes efficiently without creating multiple copies of arrays.Also see, `Broadcasting <https://docs.scipy.org/doc/numpy/user/basics.broadcasting.html>`_ for more explanation.Broadcasting is allowed on axes with size 1, such as from `(2,1,3,1)` to`(2,8,3,9)`. Elements will be duplicated on the broadcasted axes.For example::   broadcast_to([[1,2,3]], shape=(2,3)) = [[ 1.,  2.,  3.],                                           [ 1.,  2.,  3.]])The dimension which you do not want to change can also be kept as `0` which means copy the original value.So with `shape=(2,0)`, we will obtain the same result as in the above example.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L231
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="shape">The shape of the desired array. We can set the dim to zero if it's same as the original. E.g `A = broadcast_to(B, shape=(10, 0, 0))` has the same meaning as `A = broadcast_axis(B, axis=0, size=10)`.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastTo(NdArray @out,
NdArray data,
Shape shape=null)
{
return new Operator("broadcast_to")
.SetParam("shape", shape)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Broadcasts the input array to a new shape.Broadcasting is a mechanism that allows NDArrays to perform arithmetic operationswith arrays of different shapes efficiently without creating multiple copies of arrays.Also see, `Broadcasting <https://docs.scipy.org/doc/numpy/user/basics.broadcasting.html>`_ for more explanation.Broadcasting is allowed on axes with size 1, such as from `(2,1,3,1)` to`(2,8,3,9)`. Elements will be duplicated on the broadcasted axes.For example::   broadcast_to([[1,2,3]], shape=(2,3)) = [[ 1.,  2.,  3.],                                           [ 1.,  2.,  3.]])The dimension which you do not want to change can also be kept as `0` which means copy the original value.So with `shape=(2,0)`, we will obtain the same result as in the above example.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L231
/// </summary>
/// <param name="data">The input</param>
/// <param name="shape">The shape of the desired array. We can set the dim to zero if it's same as the original. E.g `A = broadcast_to(B, shape=(10, 0, 0))` has the same meaning as `A = broadcast_axis(B, axis=0, size=10)`.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastTo(NdArray data,
Shape shape=null)
{
return new Operator("broadcast_to")
.SetParam("shape", shape)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastBackward(NdArray @out)
{
return new Operator("_broadcast_backward")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastBackward()
{
return new Operator("_broadcast_backward")
.Invoke();
}
/// <summary>
/// Flattens the input array and then computes the l2 norm.Examples::  x = [[1, 2],       [3, 4]]  norm(x) = [5.47722578]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L257
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Source input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Norm(NdArray @out,
NdArray data)
{
return new Operator("norm")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Flattens the input array and then computes the l2 norm.Examples::  x = [[1, 2],       [3, 4]]  norm(x) = [5.47722578]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L257
/// </summary>
/// <param name="data">Source input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Norm(NdArray data)
{
return new Operator("norm")
.SetInput("data", data)
.Invoke();
}
private static readonly List<string> CastStorageStypeConvert = new List<string>(){"csr","default","row_sparse"};
/// <summary>
/// Casts tensor storage type to the new type.When an NDArray with default storage type is cast to csr or row_sparse storage,the result is compact, which means:- for csr, zero values will not be retained- for row_sparse, row slices of all zeros will not be retainedThe storage type of ``cast_storage`` output depends on stype parameter:- cast_storage(csr, 'default') = default- cast_storage(row_sparse, 'default') = default- cast_storage(default, 'csr') = csr- cast_storage(default, 'row_sparse') = row_sparseExample::    dense = [[ 0.,  1.,  0.],             [ 2.,  0.,  3.],             [ 0.,  0.,  0.],             [ 0.,  0.,  0.]]    # cast to row_sparse storage type    rsp = cast_storage(dense, 'row_sparse')    rsp.indices = [0, 1]    rsp.values = [[ 0.,  1.,  0.],                  [ 2.,  0.,  3.]]    # cast to csr storage type    csr = cast_storage(dense, 'csr')    csr.indices = [1, 0, 2]    csr.values = [ 1.,  2.,  3.]    csr.indptr = [0, 1, 3, 3, 3]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\cast_storage.cc:L69
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input.</param>
/// <param name="stype">Output storage type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray CastStorage(NdArray @out,
NdArray data,
CastStorageStype stype)
{
return new Operator("cast_storage")
.SetParam("stype", Util.EnumToString<CastStorageStype>(stype,CastStorageStypeConvert))
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Casts tensor storage type to the new type.When an NDArray with default storage type is cast to csr or row_sparse storage,the result is compact, which means:- for csr, zero values will not be retained- for row_sparse, row slices of all zeros will not be retainedThe storage type of ``cast_storage`` output depends on stype parameter:- cast_storage(csr, 'default') = default- cast_storage(row_sparse, 'default') = default- cast_storage(default, 'csr') = csr- cast_storage(default, 'row_sparse') = row_sparseExample::    dense = [[ 0.,  1.,  0.],             [ 2.,  0.,  3.],             [ 0.,  0.,  0.],             [ 0.,  0.,  0.]]    # cast to row_sparse storage type    rsp = cast_storage(dense, 'row_sparse')    rsp.indices = [0, 1]    rsp.values = [[ 0.,  1.,  0.],                  [ 2.,  0.,  3.]]    # cast to csr storage type    csr = cast_storage(dense, 'csr')    csr.indices = [1, 0, 2]    csr.values = [ 1.,  2.,  3.]    csr.indptr = [0, 1, 3, 3, 3]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\cast_storage.cc:L69
/// </summary>
/// <param name="data">The input.</param>
/// <param name="stype">Output storage type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray CastStorage(NdArray data,
CastStorageStype stype)
{
return new Operator("cast_storage")
.SetParam("stype", Util.EnumToString<CastStorageStype>(stype,CastStorageStypeConvert))
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Given three ndarrays, condition, x, and y, return an ndarray with the elements from x or y, depending on the elements from condition are true or false. x and y must have the same shape. If condition has the same shape as x, each element in the output array is from x if the corresponding element in the condition is true, and from y if false. If condition does not have the same shape as x, it must be a 1D array whose size is the same as x's first dimension size. Each row of the output array is from x's row if the corresponding element from condition is true, and from y's row if false.From:I:\workspace\incubator-mxnet\src\operator\tensor\control_flow_op.cc:40
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="condition">condition array</param>
/// <param name="x"></param>
/// <param name="y"></param>
 /// <returns>returns new symbol</returns>
public static NdArray Where(NdArray @out,
NdArray condition,
NdArray x,
NdArray y)
{
return new Operator("where")
.SetInput("condition", condition)
.SetInput("x", x)
.SetInput("y", y)
.Invoke(@out);
}
/// <summary>
/// Given three ndarrays, condition, x, and y, return an ndarray with the elements from x or y, depending on the elements from condition are true or false. x and y must have the same shape. If condition has the same shape as x, each element in the output array is from x if the corresponding element in the condition is true, and from y if false. If condition does not have the same shape as x, it must be a 1D array whose size is the same as x's first dimension size. Each row of the output array is from x's row if the corresponding element from condition is true, and from y's row if false.From:I:\workspace\incubator-mxnet\src\operator\tensor\control_flow_op.cc:40
/// </summary>
/// <param name="condition">condition array</param>
/// <param name="x"></param>
/// <param name="y"></param>
 /// <returns>returns new symbol</returns>
public static NdArray Where(NdArray condition,
NdArray x,
NdArray y)
{
return new Operator("where")
.SetInput("condition", condition)
.SetInput("x", x)
.SetInput("y", y)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardWhere(NdArray @out)
{
return new Operator("_backward_where")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardWhere()
{
return new Operator("_backward_where")
.Invoke();
}
/// <summary>
/// Dot product of two arrays.``dot``'s behavior depends on the input array dimensions:- 1-D arrays: inner product of vectors- 2-D arrays: matrix multiplication- N-D arrays: a sum product over the last axis of the first input and the first  axis of the second input  For example, given 3-D ``x`` with shape `(n,m,k)` and ``y`` with shape `(k,r,s)`, the  result array will have shape `(n,m,r,s)`. It is computed by::    dot(x,y)[i,j,a,b] = sum(x[i,j,:]*y[:,a,b])  Example::    x = reshape([0,1,2,3,4,5,6,7], shape=(2,2,2))    y = reshape([7,6,5,4,3,2,1,0], shape=(2,2,2))    dot(x,y)[0,0,1,1] = 0    sum(x[0,0,:]*y[:,1,1]) = 0The storage type of ``dot`` output depends on storage types of inputs and transpose options:- dot(csr, default) = default- dot(csr.T, default) = row_sparse- dot(csr, row_sparse) = default- dot(default, csr) = csr- otherwise, ``dot`` generates output with default storageDefined in I:\workspace\incubator-mxnet\src\operator\tensor\dot.cc:L62
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">The first input</param>
/// <param name="rhs">The second input</param>
/// <param name="transpose_a">If true then transpose the first input before dot.</param>
/// <param name="transpose_b">If true then transpose the second input before dot.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Dot(NdArray @out,
NdArray lhs,
NdArray rhs,
bool transpose_a=false,
bool transpose_b=false)
{
return new Operator("dot")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Dot product of two arrays.``dot``'s behavior depends on the input array dimensions:- 1-D arrays: inner product of vectors- 2-D arrays: matrix multiplication- N-D arrays: a sum product over the last axis of the first input and the first  axis of the second input  For example, given 3-D ``x`` with shape `(n,m,k)` and ``y`` with shape `(k,r,s)`, the  result array will have shape `(n,m,r,s)`. It is computed by::    dot(x,y)[i,j,a,b] = sum(x[i,j,:]*y[:,a,b])  Example::    x = reshape([0,1,2,3,4,5,6,7], shape=(2,2,2))    y = reshape([7,6,5,4,3,2,1,0], shape=(2,2,2))    dot(x,y)[0,0,1,1] = 0    sum(x[0,0,:]*y[:,1,1]) = 0The storage type of ``dot`` output depends on storage types of inputs and transpose options:- dot(csr, default) = default- dot(csr.T, default) = row_sparse- dot(csr, row_sparse) = default- dot(default, csr) = csr- otherwise, ``dot`` generates output with default storageDefined in I:\workspace\incubator-mxnet\src\operator\tensor\dot.cc:L62
/// </summary>
/// <param name="lhs">The first input</param>
/// <param name="rhs">The second input</param>
/// <param name="transpose_a">If true then transpose the first input before dot.</param>
/// <param name="transpose_b">If true then transpose the second input before dot.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Dot(NdArray lhs,
NdArray rhs,
bool transpose_a=false,
bool transpose_b=false)
{
return new Operator("dot")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="transpose_a">If true then transpose the first input before dot.</param>
/// <param name="transpose_b">If true then transpose the second input before dot.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDot(NdArray @out,
bool transpose_a=false,
bool transpose_b=false)
{
return new Operator("_backward_dot")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="transpose_a">If true then transpose the first input before dot.</param>
/// <param name="transpose_b">If true then transpose the second input before dot.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDot(bool transpose_a=false,
bool transpose_b=false)
{
return new Operator("_backward_dot")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.Invoke();
}
/// <summary>
/// Batchwise dot product.``batch_dot`` is used to compute dot product of ``x`` and ``y`` when ``x`` and``y`` are data in batch, namely 3D arrays in shape of `(batch_size, :, :)`.For example, given ``x`` with shape `(batch_size, n, m)` and ``y`` with shape`(batch_size, m, k)`, the result array will have shape `(batch_size, n, k)`,which is computed by::   batch_dot(x,y)[i,:,:] = dot(x[i,:,:], y[i,:,:])Defined in I:\workspace\incubator-mxnet\src\operator\tensor\dot.cc:L110
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">The first input</param>
/// <param name="rhs">The second input</param>
/// <param name="transpose_a">If true then transpose the first input before dot.</param>
/// <param name="transpose_b">If true then transpose the second input before dot.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BatchDot(NdArray @out,
NdArray lhs,
NdArray rhs,
bool transpose_a=false,
bool transpose_b=false)
{
return new Operator("batch_dot")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Batchwise dot product.``batch_dot`` is used to compute dot product of ``x`` and ``y`` when ``x`` and``y`` are data in batch, namely 3D arrays in shape of `(batch_size, :, :)`.For example, given ``x`` with shape `(batch_size, n, m)` and ``y`` with shape`(batch_size, m, k)`, the result array will have shape `(batch_size, n, k)`,which is computed by::   batch_dot(x,y)[i,:,:] = dot(x[i,:,:], y[i,:,:])Defined in I:\workspace\incubator-mxnet\src\operator\tensor\dot.cc:L110
/// </summary>
/// <param name="lhs">The first input</param>
/// <param name="rhs">The second input</param>
/// <param name="transpose_a">If true then transpose the first input before dot.</param>
/// <param name="transpose_b">If true then transpose the second input before dot.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BatchDot(NdArray lhs,
NdArray rhs,
bool transpose_a=false,
bool transpose_b=false)
{
return new Operator("batch_dot")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBatchDot(NdArray @out)
{
return new Operator("_backward_batch_dot")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBatchDot()
{
return new Operator("_backward_batch_dot")
.Invoke();
}
/// <summary>
/// Returns element-wise sum of the input arrays with broadcasting.`broadcast_plus` is an alias to the function `broadcast_add`.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_add(x, y) = [[ 1.,  1.,  1.],                          [ 2.,  2.,  2.]]   broadcast_plus(x, y) = [[ 1.,  1.,  1.],                           [ 2.,  2.,  2.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L51
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastAdd(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_add")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise sum of the input arrays with broadcasting.`broadcast_plus` is an alias to the function `broadcast_add`.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_add(x, y) = [[ 1.,  1.,  1.],                          [ 2.,  2.,  2.]]   broadcast_plus(x, y) = [[ 1.,  1.,  1.],                           [ 2.,  2.,  2.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L51
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastAdd(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_add")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastAdd(NdArray @out)
{
return new Operator("_backward_broadcast_add")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastAdd()
{
return new Operator("_backward_broadcast_add")
.Invoke();
}
/// <summary>
/// Returns element-wise difference of the input arrays with broadcasting.`broadcast_minus` is an alias to the function `broadcast_sub`.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_sub(x, y) = [[ 1.,  1.,  1.],                          [ 0.,  0.,  0.]]   broadcast_minus(x, y) = [[ 1.,  1.,  1.],                            [ 0.,  0.,  0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L90
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastSub(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_sub")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise difference of the input arrays with broadcasting.`broadcast_minus` is an alias to the function `broadcast_sub`.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_sub(x, y) = [[ 1.,  1.,  1.],                          [ 0.,  0.,  0.]]   broadcast_minus(x, y) = [[ 1.,  1.,  1.],                            [ 0.,  0.,  0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L90
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastSub(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_sub")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastSub(NdArray @out)
{
return new Operator("_backward_broadcast_sub")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastSub()
{
return new Operator("_backward_broadcast_sub")
.Invoke();
}
/// <summary>
/// Returns element-wise product of the input arrays with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_mul(x, y) = [[ 0.,  0.,  0.],                          [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L123
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastMul(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_mul")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise product of the input arrays with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_mul(x, y) = [[ 0.,  0.,  0.],                          [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L123
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastMul(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_mul")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastMul(NdArray @out)
{
return new Operator("_backward_broadcast_mul")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastMul()
{
return new Operator("_backward_broadcast_mul")
.Invoke();
}
/// <summary>
/// Returns element-wise division of the input arrays with broadcasting.Example::   x = [[ 6.,  6.,  6.],        [ 6.,  6.,  6.]]   y = [[ 2.],        [ 3.]]   broadcast_div(x, y) = [[ 3.,  3.,  3.],                          [ 2.,  2.,  2.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L157
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastDiv(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_div")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise division of the input arrays with broadcasting.Example::   x = [[ 6.,  6.,  6.],        [ 6.,  6.,  6.]]   y = [[ 2.],        [ 3.]]   broadcast_div(x, y) = [[ 3.,  3.,  3.],                          [ 2.,  2.,  2.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L157
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastDiv(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_div")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastDiv(NdArray @out)
{
return new Operator("_backward_broadcast_div")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastDiv()
{
return new Operator("_backward_broadcast_div")
.Invoke();
}
/// <summary>
/// Returns element-wise modulo of the input arrays with broadcasting.Example::   x = [[ 8.,  8.,  8.],        [ 8.,  8.,  8.]]   y = [[ 2.],        [ 3.]]   broadcast_mod(x, y) = [[ 0.,  0.,  0.],                          [ 2.,  2.,  2.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L190
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastMod(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_mod")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise modulo of the input arrays with broadcasting.Example::   x = [[ 8.,  8.,  8.],        [ 8.,  8.,  8.]]   y = [[ 2.],        [ 3.]]   broadcast_mod(x, y) = [[ 0.,  0.,  0.],                          [ 2.,  2.,  2.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L190
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastMod(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_mod")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastMod(NdArray @out)
{
return new Operator("_backward_broadcast_mod")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastMod()
{
return new Operator("_backward_broadcast_mod")
.Invoke();
}
/// <summary>
/// Returns result of first array elements raised to powers from second array, element-wise with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_power(x, y) = [[ 2.,  2.,  2.],                            [ 4.,  4.,  4.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L45
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastPower(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_power")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns result of first array elements raised to powers from second array, element-wise with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_power(x, y) = [[ 2.,  2.,  2.],                            [ 4.,  4.,  4.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L45
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastPower(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_power")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastPower(NdArray @out)
{
return new Operator("_backward_broadcast_power")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastPower()
{
return new Operator("_backward_broadcast_power")
.Invoke();
}
/// <summary>
/// Returns element-wise maximum of the input arrays with broadcasting.This function compares two input arrays and returns a new array having the element-wise maxima.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_maximum(x, y) = [[ 1.,  1.,  1.],                              [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L80
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastMaximum(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_maximum")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise maximum of the input arrays with broadcasting.This function compares two input arrays and returns a new array having the element-wise maxima.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_maximum(x, y) = [[ 1.,  1.,  1.],                              [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L80
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastMaximum(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_maximum")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastMaximum(NdArray @out)
{
return new Operator("_backward_broadcast_maximum")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastMaximum()
{
return new Operator("_backward_broadcast_maximum")
.Invoke();
}
/// <summary>
/// Returns element-wise minimum of the input arrays with broadcasting.This function compares two input arrays and returns a new array having the element-wise minima.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_maximum(x, y) = [[ 0.,  0.,  0.],                              [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L115
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastMinimum(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_minimum")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise minimum of the input arrays with broadcasting.This function compares two input arrays and returns a new array having the element-wise minima.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_maximum(x, y) = [[ 0.,  0.,  0.],                              [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L115
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastMinimum(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_minimum")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastMinimum(NdArray @out)
{
return new Operator("_backward_broadcast_minimum")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastMinimum()
{
return new Operator("_backward_broadcast_minimum")
.Invoke();
}
/// <summary>
///  Returns the hypotenuse of a right angled triangle, given its "legs"with broadcasting.It is equivalent to doing :math:`sqrt(x_1^2 + x_2^2)`.Example::   x = [[ 3.,  3.,  3.]]   y = [[ 4.],        [ 4.]]   broadcast_hypot(x, y) = [[ 5.,  5.,  5.],                            [ 5.,  5.,  5.]]   z = [[ 0.],        [ 4.]]   broadcast_hypot(x, z) = [[ 3.,  3.,  3.],                            [ 5.,  5.,  5.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L156
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastHypot(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_hypot")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
///  Returns the hypotenuse of a right angled triangle, given its "legs"with broadcasting.It is equivalent to doing :math:`sqrt(x_1^2 + x_2^2)`.Example::   x = [[ 3.,  3.,  3.]]   y = [[ 4.],        [ 4.]]   broadcast_hypot(x, y) = [[ 5.,  5.,  5.],                            [ 5.,  5.,  5.]]   z = [[ 0.],        [ 4.]]   broadcast_hypot(x, z) = [[ 3.,  3.,  3.],                            [ 5.,  5.,  5.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L156
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastHypot(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_hypot")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastHypot(NdArray @out)
{
return new Operator("_backward_broadcast_hypot")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBroadcastHypot()
{
return new Operator("_backward_broadcast_hypot")
.Invoke();
}
/// <summary>
/// Returns the result of element-wise **equal to** (==) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_equal(x, y) = [[ 0.,  0.,  0.],                            [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L46
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastEqual(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns the result of element-wise **equal to** (==) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_equal(x, y) = [[ 0.,  0.,  0.],                            [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L46
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastEqual(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the result of element-wise **not equal to** (!=) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_not_equal(x, y) = [[ 1.,  1.,  1.],                                [ 0.,  0.,  0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L64
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastNotEqual(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_not_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns the result of element-wise **not equal to** (!=) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_not_equal(x, y) = [[ 1.,  1.,  1.],                                [ 0.,  0.,  0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L64
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastNotEqual(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_not_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the result of element-wise **greater than** (>) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_greater(x, y) = [[ 1.,  1.,  1.],                              [ 0.,  0.,  0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L82
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastGreater(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_greater")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns the result of element-wise **greater than** (>) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_greater(x, y) = [[ 1.,  1.,  1.],                              [ 0.,  0.,  0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L82
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastGreater(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_greater")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the result of element-wise **greater than or equal to** (>=) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_greater_equal(x, y) = [[ 1.,  1.,  1.],                                    [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L100
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastGreaterEqual(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_greater_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns the result of element-wise **greater than or equal to** (>=) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_greater_equal(x, y) = [[ 1.,  1.,  1.],                                    [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L100
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastGreaterEqual(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_greater_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the result of element-wise **lesser than** (<) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_lesser(x, y) = [[ 0.,  0.,  0.],                             [ 0.,  0.,  0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L118
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastLesser(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_lesser")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns the result of element-wise **lesser than** (<) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_lesser(x, y) = [[ 0.,  0.,  0.],                             [ 0.,  0.,  0.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L118
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastLesser(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_lesser")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the result of element-wise **lesser than or equal to** (<=) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_lesser_equal(x, y) = [[ 0.,  0.,  0.],                                   [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L136
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastLesserEqual(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_lesser_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Returns the result of element-wise **lesser than or equal to** (<=) comparison operation with broadcasting.Example::   x = [[ 1.,  1.,  1.],        [ 1.,  1.,  1.]]   y = [[ 0.],        [ 1.]]   broadcast_lesser_equal(x, y) = [[ 0.,  0.,  0.],                                   [ 1.,  1.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L136
/// </summary>
/// <param name="lhs">First input to the function</param>
/// <param name="rhs">Second input to the function</param>
 /// <returns>returns new symbol</returns>
public static NdArray BroadcastLesserEqual(NdArray lhs,
NdArray rhs)
{
return new Operator("broadcast_lesser_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Adds arguments element-wise.The storage type of ``elemwise_add`` output depends on storage types of inputs   - elemwise_add(row_sparse, row_sparse) = row_sparse   - elemwise_add(csr, csr) = csr   - otherwise, ``elemwise_add`` generates output with default storage
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ElemwiseAdd(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("elemwise_add")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Adds arguments element-wise.The storage type of ``elemwise_add`` output depends on storage types of inputs   - elemwise_add(row_sparse, row_sparse) = row_sparse   - elemwise_add(csr, csr) = csr   - otherwise, ``elemwise_add`` generates output with default storage
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ElemwiseAdd(NdArray lhs,
NdArray rhs)
{
return new Operator("elemwise_add")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray GradAdd(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_grad_add")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray GradAdd(NdArray lhs,
NdArray rhs)
{
return new Operator("_grad_add")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardAdd(NdArray @out)
{
return new Operator("_backward_add")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardAdd()
{
return new Operator("_backward_add")
.Invoke();
}
/// <summary>
/// Subtracts arguments element-wise.The storage type of ``elemwise_sub`` output depends on storage types of inputs   - elemwise_sub(row_sparse, row_sparse) = row_sparse   - elemwise_sub(csr, csr) = csr   - otherwise, ``elemwise_sub`` generates output with default storage
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ElemwiseSub(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("elemwise_sub")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Subtracts arguments element-wise.The storage type of ``elemwise_sub`` output depends on storage types of inputs   - elemwise_sub(row_sparse, row_sparse) = row_sparse   - elemwise_sub(csr, csr) = csr   - otherwise, ``elemwise_sub`` generates output with default storage
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ElemwiseSub(NdArray lhs,
NdArray rhs)
{
return new Operator("elemwise_sub")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSub(NdArray @out)
{
return new Operator("_backward_sub")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSub()
{
return new Operator("_backward_sub")
.Invoke();
}
/// <summary>
/// Multiplies arguments element-wise.The storage type of ``elemwise_mul`` output depends on storage types of inputs   - elemwise_mul(default, default) = default   - elemwise_mul(row_sparse, row_sparse) = row_sparse   - elemwise_mul(default, row_sparse) = default   - elemwise_mul(row_sparse, default) = default   - elemwise_mul(csr, csr) = csr   - otherwise, ``elemwise_mul`` generates output with default storage
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ElemwiseMul(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("elemwise_mul")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Multiplies arguments element-wise.The storage type of ``elemwise_mul`` output depends on storage types of inputs   - elemwise_mul(default, default) = default   - elemwise_mul(row_sparse, row_sparse) = row_sparse   - elemwise_mul(default, row_sparse) = default   - elemwise_mul(row_sparse, default) = default   - elemwise_mul(csr, csr) = csr   - otherwise, ``elemwise_mul`` generates output with default storage
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ElemwiseMul(NdArray lhs,
NdArray rhs)
{
return new Operator("elemwise_mul")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMul(NdArray @out)
{
return new Operator("_backward_mul")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMul()
{
return new Operator("_backward_mul")
.Invoke();
}
/// <summary>
/// Divides arguments element-wise.The storage type of ``elemwise_div`` output is always dense
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ElemwiseDiv(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("elemwise_div")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Divides arguments element-wise.The storage type of ``elemwise_div`` output is always dense
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ElemwiseDiv(NdArray lhs,
NdArray rhs)
{
return new Operator("elemwise_div")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDiv(NdArray @out)
{
return new Operator("_backward_div")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDiv()
{
return new Operator("_backward_div")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Mod(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_mod")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Mod(NdArray lhs,
NdArray rhs)
{
return new Operator("_mod")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMod(NdArray @out)
{
return new Operator("_backward_mod")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMod()
{
return new Operator("_backward_mod")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Power(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_power")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Power(NdArray lhs,
NdArray rhs)
{
return new Operator("_power")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPower(NdArray @out)
{
return new Operator("_backward_power")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPower()
{
return new Operator("_backward_power")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Maximum(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_maximum")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Maximum(NdArray lhs,
NdArray rhs)
{
return new Operator("_maximum")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMaximum(NdArray @out)
{
return new Operator("_backward_maximum")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMaximum()
{
return new Operator("_backward_maximum")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Minimum(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_minimum")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Minimum(NdArray lhs,
NdArray rhs)
{
return new Operator("_minimum")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMinimum(NdArray @out)
{
return new Operator("_backward_minimum")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMinimum()
{
return new Operator("_backward_minimum")
.Invoke();
}
/// <summary>
/// Given the "legs" of a right triangle, return its hypotenuse.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_op_extended.cc:L79
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Hypot(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_hypot")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Given the "legs" of a right triangle, return its hypotenuse.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_op_extended.cc:L79
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Hypot(NdArray lhs,
NdArray rhs)
{
return new Operator("_hypot")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardHypot(NdArray @out)
{
return new Operator("_backward_hypot")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardHypot()
{
return new Operator("_backward_hypot")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Equal(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Equal(NdArray lhs,
NdArray rhs)
{
return new Operator("_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray NotEqual(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_not_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray NotEqual(NdArray lhs,
NdArray rhs)
{
return new Operator("_not_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Greater(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_greater")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Greater(NdArray lhs,
NdArray rhs)
{
return new Operator("_greater")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray GreaterEqual(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_greater_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray GreaterEqual(NdArray lhs,
NdArray rhs)
{
return new Operator("_greater_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Lesser(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_lesser")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray Lesser(NdArray lhs,
NdArray rhs)
{
return new Operator("_lesser")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray LesserEqual(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_lesser_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray LesserEqual(NdArray lhs,
NdArray rhs)
{
return new Operator("_lesser_equal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Multiply an array with a scalar.``_mul_scalar`` only operates on data array of input if input is sparse.For example, if input of shape (100, 100) has only 2 non zero elements,i.e. input.data = [5, 6], scalar = nan,it will result output.data = [nan, nan] instead of 10000 nans.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_scalar_op_basic.cc:L153
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray MulScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_mul_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Multiply an array with a scalar.``_mul_scalar`` only operates on data array of input if input is sparse.For example, if input of shape (100, 100) has only 2 non zero elements,i.e. input.data = [5, 6], scalar = nan,it will result output.data = [nan, nan] instead of 10000 nans.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_scalar_op_basic.cc:L153
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray MulScalar(NdArray data,
float scalar)
{
return new Operator("_mul_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMulScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_backward_mul_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMulScalar(NdArray data,
float scalar)
{
return new Operator("_backward_mul_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Divide an array with a scalar.``_div_scalar`` only operates on data array of input if input is sparse.For example, if input of shape (100, 100) has only 2 non zero elements,i.e. input.data = [5, 6], scalar = nan,it will result output.data = [nan, nan] instead of 10000 nans.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_scalar_op_basic.cc:L175
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray DivScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_div_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Divide an array with a scalar.``_div_scalar`` only operates on data array of input if input is sparse.For example, if input of shape (100, 100) has only 2 non zero elements,i.e. input.data = [5, 6], scalar = nan,it will result output.data = [nan, nan] instead of 10000 nans.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_scalar_op_basic.cc:L175
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray DivScalar(NdArray data,
float scalar)
{
return new Operator("_div_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDivScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_backward_div_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDivScalar(NdArray data,
float scalar)
{
return new Operator("_backward_div_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray RdivScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_rdiv_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray RdivScalar(NdArray data,
float scalar)
{
return new Operator("_rdiv_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRdivScalar(NdArray @out,
NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_rdiv_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRdivScalar(NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_rdiv_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ModScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_mod_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ModScalar(NdArray data,
float scalar)
{
return new Operator("_mod_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardModScalar(NdArray @out,
NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_mod_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardModScalar(NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_mod_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray RmodScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_rmod_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray RmodScalar(NdArray data,
float scalar)
{
return new Operator("_rmod_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRmodScalar(NdArray @out,
NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_rmod_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRmodScalar(NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_rmod_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray PlusScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_plus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray PlusScalar(NdArray data,
float scalar)
{
return new Operator("_plus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray MinusScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_minus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray MinusScalar(NdArray data,
float scalar)
{
return new Operator("_minus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray RminusScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_rminus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray RminusScalar(NdArray data,
float scalar)
{
return new Operator("_rminus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray MaximumScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_maximum_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray MaximumScalar(NdArray data,
float scalar)
{
return new Operator("_maximum_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMaximumScalar(NdArray @out,
NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_maximum_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMaximumScalar(NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_maximum_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray MinimumScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_minimum_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray MinimumScalar(NdArray data,
float scalar)
{
return new Operator("_minimum_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMinimumScalar(NdArray @out,
NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_minimum_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMinimumScalar(NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_minimum_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray PowerScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_power_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray PowerScalar(NdArray data,
float scalar)
{
return new Operator("_power_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPowerScalar(NdArray @out,
NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_power_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPowerScalar(NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_power_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray RpowerScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_rpower_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray RpowerScalar(NdArray data,
float scalar)
{
return new Operator("_rpower_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRpowerScalar(NdArray @out,
NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_rpower_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRpowerScalar(NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_rpower_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray HypotScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_hypot_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray HypotScalar(NdArray data,
float scalar)
{
return new Operator("_hypot_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardHypotScalar(NdArray @out,
NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_hypot_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
/// <param name="scalar">scalar value</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardHypotScalar(NdArray lhs,
NdArray rhs,
float scalar)
{
return new Operator("_backward_hypot_scalar")
.SetParam("scalar", scalar)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Calculate Smooth L1 Loss(lhs, scalar) by summing.. math::    f(x) =    \begin{cases}    (\sigma x)^2/2,& \text{if }x < 1/\sigma^2\\    |x|-0.5/\sigma^2,& \text{otherwise}    \end{cases}where :math:`x` is an element of the tensor *lhs* and :math:`\sigma` is the scalar.Example::  smooth_l1([1, 2, 3, 4], sigma=1) = [0.5, 1.5, 2.5, 3.5]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_scalar_op_extended.cc:L103
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray SmoothL1(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("smooth_l1")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Calculate Smooth L1 Loss(lhs, scalar) by summing.. math::    f(x) =    \begin{cases}    (\sigma x)^2/2,& \text{if }x < 1/\sigma^2\\    |x|-0.5/\sigma^2,& \text{otherwise}    \end{cases}where :math:`x` is an element of the tensor *lhs* and :math:`\sigma` is the scalar.Example::  smooth_l1([1, 2, 3, 4], sigma=1) = [0.5, 1.5, 2.5, 3.5]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_binary_scalar_op_extended.cc:L103
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray SmoothL1(NdArray data,
float scalar)
{
return new Operator("smooth_l1")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSmoothL1(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_smooth_l1")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSmoothL1(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_smooth_l1")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray EqualScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_equal_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray EqualScalar(NdArray data,
float scalar)
{
return new Operator("_equal_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray NotEqualScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_not_equal_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray NotEqualScalar(NdArray data,
float scalar)
{
return new Operator("_not_equal_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray GreaterScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_greater_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray GreaterScalar(NdArray data,
float scalar)
{
return new Operator("_greater_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray GreaterEqualScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_greater_equal_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray GreaterEqualScalar(NdArray data,
float scalar)
{
return new Operator("_greater_equal_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray LesserScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_lesser_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray LesserScalar(NdArray data,
float scalar)
{
return new Operator("_lesser_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray LesserEqualScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_lesser_equal_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray LesserEqualScalar(NdArray data,
float scalar)
{
return new Operator("_lesser_equal_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Subtracts a scalar to a tensor element-wise.  If the left-hand-side input is'row_sparse' or 'csr', then only the values which exist in the left-hand sparse array are computed.The 'missing' values are ignored.The storage type of ``_scatter_minus_scalar`` output depends on storage types of inputs- _scatter_minus_scalar(row_sparse, scalar) = row_sparse- _scatter_minus_scalar(csr, scalar) = csr- otherwise, ``_scatter_minus_scalar`` behaves exactly like _minus_scalar and generates outputwith default storage
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterMinusScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_scatter_minus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Subtracts a scalar to a tensor element-wise.  If the left-hand-side input is'row_sparse' or 'csr', then only the values which exist in the left-hand sparse array are computed.The 'missing' values are ignored.The storage type of ``_scatter_minus_scalar`` output depends on storage types of inputs- _scatter_minus_scalar(row_sparse, scalar) = row_sparse- _scatter_minus_scalar(csr, scalar) = csr- otherwise, ``_scatter_minus_scalar`` behaves exactly like _minus_scalar and generates outputwith default storage
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterMinusScalar(NdArray data,
float scalar)
{
return new Operator("_scatter_minus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Divides arguments element-wise.  If the left-hand-side input is 'row_sparse', thenonly the values which exist in the left-hand sparse array are computed.  The 'missing' valuesare ignored.The storage type of ``_scatter_elemwise_div`` output depends on storage types of inputs- _scatter_elemwise_div(row_sparse, row_sparse) = row_sparse- _scatter_elemwise_div(row_sparse, dense) = row_sparse- _scatter_elemwise_div(row_sparse, csr) = row_sparse- otherwise, ``_scatter_elemwise_div`` behaves exactly like elemwise_div and generates outputwith default storage
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterElemwiseDiv(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_scatter_elemwise_div")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Divides arguments element-wise.  If the left-hand-side input is 'row_sparse', thenonly the values which exist in the left-hand sparse array are computed.  The 'missing' valuesare ignored.The storage type of ``_scatter_elemwise_div`` output depends on storage types of inputs- _scatter_elemwise_div(row_sparse, row_sparse) = row_sparse- _scatter_elemwise_div(row_sparse, dense) = row_sparse- _scatter_elemwise_div(row_sparse, csr) = row_sparse- otherwise, ``_scatter_elemwise_div`` behaves exactly like elemwise_div and generates outputwith default storage
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterElemwiseDiv(NdArray lhs,
NdArray rhs)
{
return new Operator("_scatter_elemwise_div")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Adds a scalar to a tensor element-wise.  If the left-hand-side input is'row_sparse' or 'csr', then only the values which exist in the left-hand sparse array are computed.The 'missing' values are ignored.The storage type of ``_scatter_plus_scalar`` output depends on storage types of inputs- _scatter_plus_scalar(row_sparse, scalar) = row_sparse- _scatter_plus_scalar(csr, scalar) = csr- otherwise, ``_scatter_plus_scalar`` behaves exactly like _plus_scalar and generates outputwith default storage
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterPlusScalar(NdArray @out,
NdArray data,
float scalar)
{
return new Operator("_scatter_plus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Adds a scalar to a tensor element-wise.  If the left-hand-side input is'row_sparse' or 'csr', then only the values which exist in the left-hand sparse array are computed.The 'missing' values are ignored.The storage type of ``_scatter_plus_scalar`` output depends on storage types of inputs- _scatter_plus_scalar(row_sparse, scalar) = row_sparse- _scatter_plus_scalar(csr, scalar) = csr- otherwise, ``_scatter_plus_scalar`` behaves exactly like _plus_scalar and generates outputwith default storage
/// </summary>
/// <param name="data">source input</param>
/// <param name="scalar">scalar input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterPlusScalar(NdArray data,
float scalar)
{
return new Operator("_scatter_plus_scalar")
.SetParam("scalar", scalar)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Adds all input arguments element-wise... math::   add\_n(a_1, a_2, ..., a_n) = a_1 + a_2 + ... + a_n``add_n`` is potentially more efficient than calling ``add`` by `n` times.The storage type of ``add_n`` output depends on storage types of inputs- add_n(row_sparse, row_sparse, ..) = row_sparse- otherwise, ``add_n`` generates output with default storageDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_sum.cc:L123
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="args">Positional input arguments</param>
 /// <returns>returns new symbol</returns>
public static NdArray AddN(NdArray @out,
NdArray[] args)
{
return new Operator("add_n")
.AddInput(args)
.Invoke(@out);
}
/// <summary>
/// Adds all input arguments element-wise... math::   add\_n(a_1, a_2, ..., a_n) = a_1 + a_2 + ... + a_n``add_n`` is potentially more efficient than calling ``add`` by `n` times.The storage type of ``add_n`` output depends on storage types of inputs- add_n(row_sparse, row_sparse, ..) = row_sparse- otherwise, ``add_n`` generates output with default storageDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_sum.cc:L123
/// </summary>
/// <param name="args">Positional input arguments</param>
 /// <returns>returns new symbol</returns>
public static NdArray AddN(NdArray[] args)
{
return new Operator("add_n")
.AddInput(args)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSigmoid(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sigmoid")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSigmoid(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sigmoid")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns a copy of the input.From:I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:112
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Copy(NdArray @out,
NdArray data)
{
return new Operator("_copy")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns a copy of the input.From:I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:112
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Copy(NdArray data)
{
return new Operator("_copy")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCopy(NdArray @out)
{
return new Operator("_backward_copy")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCopy()
{
return new Operator("_backward_copy")
.Invoke();
}
/// <summary>
/// Stops gradient computation.Stops the accumulated gradient of the inputs from flowing through this operatorin the backward direction. In other words, this operator prevents the contributionof its inputs to be taken into account for computing gradients.Example::  v1 = [1, 2]  v2 = [0, 1]  a = Variable('a')  b = Variable('b')  b_stop_grad = stop_gradient(3 * b)  loss = MakeLoss(b_stop_grad + a)  executor = loss.simple_bind(ctx=cpu(), a=(1,2), b=(1,2))  executor.forward(is_train=True, a=v1, b=v2)  executor.outputs  [ 1.  5.]  executor.backward()  executor.grad_arrays  [ 0.  0.]  [ 1.  1.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L167
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BlockGrad(NdArray @out,
NdArray data)
{
return new Operator("BlockGrad")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Stops gradient computation.Stops the accumulated gradient of the inputs from flowing through this operatorin the backward direction. In other words, this operator prevents the contributionof its inputs to be taken into account for computing gradients.Example::  v1 = [1, 2]  v2 = [0, 1]  a = Variable('a')  b = Variable('b')  b_stop_grad = stop_gradient(3 * b)  loss = MakeLoss(b_stop_grad + a)  executor = loss.simple_bind(ctx=cpu(), a=(1,2), b=(1,2))  executor.forward(is_train=True, a=v1, b=v2)  executor.outputs  [ 1.  5.]  executor.backward()  executor.grad_arrays  [ 0.  0.]  [ 1.  1.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L167
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BlockGrad(NdArray data)
{
return new Operator("BlockGrad")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Make your own loss function in network construction.This operator accepts a customized loss function symbol as a terminal loss andthe symbol should be an operator with no backward dependency.The output of this function is the gradient of loss with respect to the input data.For example, if you are a making a cross entropy loss function. Assume ``out`` is thepredicted output and ``label`` is the true label, then the cross entropy can be defined as::  cross_entropy = label * log(out) + (1 - label) * log(1 - out)  loss = make_loss(cross_entropy)We will need to use ``make_loss`` when we are creating our own loss function or we want tocombine multiple loss functions. Also we may want to stop some variables' gradientsfrom backpropagation. See more detail in ``BlockGrad`` or ``stop_gradient``.The storage type of ``make_loss`` output depends upon the input storage type:   - make_loss(default) = default   - make_loss(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L200
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray MakeLoss(NdArray @out,
NdArray data)
{
return new Operator("make_loss")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Make your own loss function in network construction.This operator accepts a customized loss function symbol as a terminal loss andthe symbol should be an operator with no backward dependency.The output of this function is the gradient of loss with respect to the input data.For example, if you are a making a cross entropy loss function. Assume ``out`` is thepredicted output and ``label`` is the true label, then the cross entropy can be defined as::  cross_entropy = label * log(out) + (1 - label) * log(1 - out)  loss = make_loss(cross_entropy)We will need to use ``make_loss`` when we are creating our own loss function or we want tocombine multiple loss functions. Also we may want to stop some variables' gradientsfrom backpropagation. See more detail in ``BlockGrad`` or ``stop_gradient``.The storage type of ``make_loss`` output depends upon the input storage type:   - make_loss(default) = default   - make_loss(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L200
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray MakeLoss(NdArray data)
{
return new Operator("make_loss")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input.</param>
/// <param name="rhs">Second input.</param>
 /// <returns>returns new symbol</returns>
public static NdArray IdentityWithAttrLikeRhs(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_identity_with_attr_like_rhs")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">First input.</param>
/// <param name="rhs">Second input.</param>
 /// <returns>returns new symbol</returns>
public static NdArray IdentityWithAttrLikeRhs(NdArray lhs,
NdArray rhs)
{
return new Operator("_identity_with_attr_like_rhs")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Reshape lhs to have the same shape as rhs.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">First input.</param>
/// <param name="rhs">Second input.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ReshapeLike(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("reshape_like")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Reshape lhs to have the same shape as rhs.
/// </summary>
/// <param name="lhs">First input.</param>
/// <param name="rhs">Second input.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ReshapeLike(NdArray lhs,
NdArray rhs)
{
return new Operator("reshape_like")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Casts all elements of the input to a new type... note:: ``Cast`` is deprecated. Use ``cast`` instead.Example::   cast([0.9, 1.3], dtype='int32') = [0, 1]   cast([1e20, 11.1], dtype='float16') = [inf, 11.09375]   cast([300, 11.1, 10.9, -1, -3], dtype='uint8') = [44, 11, 10, 255, 253]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L311
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input.</param>
/// <param name="dtype">Output data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cast(NdArray @out,
NdArray data,
Dtype dtype)
{
return new Operator("Cast")
.SetParam("dtype", dtype)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Casts all elements of the input to a new type... note:: ``Cast`` is deprecated. Use ``cast`` instead.Example::   cast([0.9, 1.3], dtype='int32') = [0, 1]   cast([1e20, 11.1], dtype='float16') = [inf, 11.09375]   cast([300, 11.1, 10.9, -1, -3], dtype='uint8') = [44, 11, 10, 255, 253]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L311
/// </summary>
/// <param name="data">The input.</param>
/// <param name="dtype">Output data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cast(NdArray data,
Dtype dtype)
{
return new Operator("Cast")
.SetParam("dtype", dtype)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCast(NdArray @out)
{
return new Operator("_backward_cast")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCast()
{
return new Operator("_backward_cast")
.Invoke();
}
/// <summary>
/// Numerical negative of the argument, element-wise.The storage type of ``negative`` output depends upon the input storage type:   - negative(default) = default   - negative(row_sparse) = row_sparse   - negative(csr) = csr
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Negative(NdArray @out,
NdArray data)
{
return new Operator("negative")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Numerical negative of the argument, element-wise.The storage type of ``negative`` output depends upon the input storage type:   - negative(default) = default   - negative(row_sparse) = row_sparse   - negative(csr) = csr
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Negative(NdArray data)
{
return new Operator("negative")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns the reciprocal of the argument, element-wise.Calculates 1/x.Example::    reciprocal([-2, 1, 3, 1.6, 0.2]) = [-0.5, 1.0, 0.33333334, 0.625, 5.0]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L364
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Reciprocal(NdArray @out,
NdArray data)
{
return new Operator("reciprocal")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the reciprocal of the argument, element-wise.Calculates 1/x.Example::    reciprocal([-2, 1, 3, 1.6, 0.2]) = [-0.5, 1.0, 0.33333334, 0.625, 5.0]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L364
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Reciprocal(NdArray data)
{
return new Operator("reciprocal")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardReciprocal(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_reciprocal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardReciprocal(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_reciprocal")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise absolute value of the input.Example::   abs([-2, 0, 3]) = [2, 0, 3]The storage type of ``abs`` output depends upon the input storage type:   - abs(default) = default   - abs(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L386
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Abs(NdArray @out,
NdArray data)
{
return new Operator("abs")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise absolute value of the input.Example::   abs([-2, 0, 3]) = [2, 0, 3]The storage type of ``abs`` output depends upon the input storage type:   - abs(default) = default   - abs(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L386
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Abs(NdArray data)
{
return new Operator("abs")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardAbs(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_abs")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardAbs(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_abs")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise sign of the input.Example::   sign([-2, 0, 3]) = [-1, 0, 1]The storage type of ``sign`` output depends upon the input storage type:   - sign(default) = default   - sign(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L405
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sign(NdArray @out,
NdArray data)
{
return new Operator("sign")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise sign of the input.Example::   sign([-2, 0, 3]) = [-1, 0, 1]The storage type of ``sign`` output depends upon the input storage type:   - sign(default) = default   - sign(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L405
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sign(NdArray data)
{
return new Operator("sign")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSign(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sign")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSign(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sign")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise rounded value to the nearest integer of the input.Example::   round([-1.5, 1.5, -1.9, 1.9, 2.1]) = [-2.,  2., -2.,  2.,  2.]The storage type of ``round`` output depends upon the input storage type:  - round(default) = default  - round(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L424
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Round(NdArray @out,
NdArray data)
{
return new Operator("round")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise rounded value to the nearest integer of the input.Example::   round([-1.5, 1.5, -1.9, 1.9, 2.1]) = [-2.,  2., -2.,  2.,  2.]The storage type of ``round`` output depends upon the input storage type:  - round(default) = default  - round(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L424
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Round(NdArray data)
{
return new Operator("round")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns element-wise ceiling of the input.The ceil of the scalar x is the smallest integer i, such that i >= x.Example::   ceil([-2.1, -1.9, 1.5, 1.9, 2.1]) = [-2., -1.,  2.,  2.,  3.]The storage type of ``ceil`` output depends upon the input storage type:   - ceil(default) = default   - ceil(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L464
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Ceil(NdArray @out,
NdArray data)
{
return new Operator("ceil")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise ceiling of the input.The ceil of the scalar x is the smallest integer i, such that i >= x.Example::   ceil([-2.1, -1.9, 1.5, 1.9, 2.1]) = [-2., -1.,  2.,  2.,  3.]The storage type of ``ceil`` output depends upon the input storage type:   - ceil(default) = default   - ceil(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L464
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Ceil(NdArray data)
{
return new Operator("ceil")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns element-wise floor of the input.The floor of the scalar x is the largest integer i, such that i <= x.Example::   floor([-2.1, -1.9, 1.5, 1.9, 2.1]) = [-3., -2.,  1.,  1.,  2.]The storage type of ``floor`` output depends upon the input storage type:   - floor(default) = default   - floor(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L483
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Floor(NdArray @out,
NdArray data)
{
return new Operator("floor")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise floor of the input.The floor of the scalar x is the largest integer i, such that i <= x.Example::   floor([-2.1, -1.9, 1.5, 1.9, 2.1]) = [-3., -2.,  1.,  1.,  2.]The storage type of ``floor`` output depends upon the input storage type:   - floor(default) = default   - floor(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L483
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Floor(NdArray data)
{
return new Operator("floor")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Return the element-wise truncated value of the input.The truncated value of the scalar x is the nearest integer i which is closer tozero than x is. In short, the fractional part of the signed number x is discarded.Example::   trunc([-2.1, -1.9, 1.5, 1.9, 2.1]) = [-2., -1.,  1.,  1.,  2.]The storage type of ``trunc`` output depends upon the input storage type:   - trunc(default) = default   - trunc(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L503
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Trunc(NdArray @out,
NdArray data)
{
return new Operator("trunc")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Return the element-wise truncated value of the input.The truncated value of the scalar x is the nearest integer i which is closer tozero than x is. In short, the fractional part of the signed number x is discarded.Example::   trunc([-2.1, -1.9, 1.5, 1.9, 2.1]) = [-2., -1.,  1.,  1.,  2.]The storage type of ``trunc`` output depends upon the input storage type:   - trunc(default) = default   - trunc(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L503
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Trunc(NdArray data)
{
return new Operator("trunc")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns element-wise rounded value to the nearest integer of the input... note::   - For input ``n.5`` ``rint`` returns ``n`` while ``round`` returns ``n+1``.   - For input ``-n.5`` both ``rint`` and ``round`` returns ``-n-1``.Example::   rint([-1.5, 1.5, -1.9, 1.9, 2.1]) = [-2.,  1., -2.,  2.,  2.]The storage type of ``rint`` output depends upon the input storage type:   - rint(default) = default   - rint(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L445
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Rint(NdArray @out,
NdArray data)
{
return new Operator("rint")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise rounded value to the nearest integer of the input... note::   - For input ``n.5`` ``rint`` returns ``n`` while ``round`` returns ``n+1``.   - For input ``-n.5`` both ``rint`` and ``round`` returns ``-n-1``.Example::   rint([-1.5, 1.5, -1.9, 1.9, 2.1]) = [-2.,  1., -2.,  2.,  2.]The storage type of ``rint`` output depends upon the input storage type:   - rint(default) = default   - rint(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L445
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Rint(NdArray data)
{
return new Operator("rint")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns element-wise rounded value to the nearest \integer towards zero of the input.Example::   fix([-2.1, -1.9, 1.9, 2.1]) = [-2., -1.,  1., 2.]The storage type of ``fix`` output depends upon the input storage type:   - fix(default) = default   - fix(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L521
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Fix(NdArray @out,
NdArray data)
{
return new Operator("fix")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise rounded value to the nearest \integer towards zero of the input.Example::   fix([-2.1, -1.9, 1.9, 2.1]) = [-2., -1.,  1., 2.]The storage type of ``fix`` output depends upon the input storage type:   - fix(default) = default   - fix(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L521
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Fix(NdArray data)
{
return new Operator("fix")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns element-wise squared value of the input... math::   square(x) = x^2Example::   square([2, 3, 4]) = [4, 9, 16]The storage type of ``square`` output depends upon the input storage type:   - square(default) = default   - square(row_sparse) = row_sparse   - square(csr) = csrDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L542
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Square(NdArray @out,
NdArray data)
{
return new Operator("square")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise squared value of the input... math::   square(x) = x^2Example::   square([2, 3, 4]) = [4, 9, 16]The storage type of ``square`` output depends upon the input storage type:   - square(default) = default   - square(row_sparse) = row_sparse   - square(csr) = csrDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L542
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Square(NdArray data)
{
return new Operator("square")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSquare(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_square")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSquare(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_square")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise square-root value of the input... math::   \textrm{sqrt}(x) = \sqrt{x}Example::   sqrt([4, 9, 16]) = [2, 3, 4]The storage type of ``sqrt`` output depends upon the input storage type:   - sqrt(default) = default   - sqrt(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L565
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sqrt(NdArray @out,
NdArray data)
{
return new Operator("sqrt")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise square-root value of the input... math::   \textrm{sqrt}(x) = \sqrt{x}Example::   sqrt([4, 9, 16]) = [2, 3, 4]The storage type of ``sqrt`` output depends upon the input storage type:   - sqrt(default) = default   - sqrt(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L565
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sqrt(NdArray data)
{
return new Operator("sqrt")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSqrt(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sqrt")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSqrt(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sqrt")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise inverse square-root value of the input... math::   rsqrt(x) = 1/\sqrt{x}Example::   rsqrt([4,9,16]) = [0.5, 0.33333334, 0.25]The storage type of ``rsqrt`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L585
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Rsqrt(NdArray @out,
NdArray data)
{
return new Operator("rsqrt")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise inverse square-root value of the input... math::   rsqrt(x) = 1/\sqrt{x}Example::   rsqrt([4,9,16]) = [0.5, 0.33333334, 0.25]The storage type of ``rsqrt`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L585
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Rsqrt(NdArray data)
{
return new Operator("rsqrt")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRsqrt(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_rsqrt")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRsqrt(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_rsqrt")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise cube-root value of the input... math::   cbrt(x) = \sqrt[3]{x}Example::   cbrt([1, 8, -125]) = [1, 2, -5]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L602
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cbrt(NdArray @out,
NdArray data)
{
return new Operator("cbrt")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise cube-root value of the input... math::   cbrt(x) = \sqrt[3]{x}Example::   cbrt([1, 8, -125]) = [1, 2, -5]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L602
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cbrt(NdArray data)
{
return new Operator("cbrt")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCbrt(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_cbrt")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCbrt(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_cbrt")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise inverse cube-root value of the input... math::   rcbrt(x) = 1/\sqrt[3]{x}Example::   rcbrt([1,8,-125]) = [1.0, 0.5, -0.2]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L619
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Rcbrt(NdArray @out,
NdArray data)
{
return new Operator("rcbrt")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise inverse cube-root value of the input... math::   rcbrt(x) = 1/\sqrt[3]{x}Example::   rcbrt([1,8,-125]) = [1.0, 0.5, -0.2]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L619
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Rcbrt(NdArray data)
{
return new Operator("rcbrt")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRcbrt(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_rcbrt")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRcbrt(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_rcbrt")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise exponential value of the input... math::   exp(x) = e^x \approx 2.718^xExample::   exp([0, 1, 2]) = [1., 2.71828175, 7.38905621]The storage type of ``exp`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L642
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Exp(NdArray @out,
NdArray data)
{
return new Operator("exp")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise exponential value of the input... math::   exp(x) = e^x \approx 2.718^xExample::   exp([0, 1, 2]) = [1., 2.71828175, 7.38905621]The storage type of ``exp`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L642
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Exp(NdArray data)
{
return new Operator("exp")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns element-wise Natural logarithmic value of the input.The natural logarithm is logarithm in base *e*, so that ``log(exp(x)) = x``The storage type of ``log`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L654
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Log(NdArray @out,
NdArray data)
{
return new Operator("log")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise Natural logarithmic value of the input.The natural logarithm is logarithm in base *e*, so that ``log(exp(x)) = x``The storage type of ``log`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L654
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Log(NdArray data)
{
return new Operator("log")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns element-wise Base-10 logarithmic value of the input.``10**log10(x) = x``The storage type of ``log10`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L666
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Log10(NdArray @out,
NdArray data)
{
return new Operator("log10")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise Base-10 logarithmic value of the input.``10**log10(x) = x``The storage type of ``log10`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L666
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Log10(NdArray data)
{
return new Operator("log10")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns element-wise Base-2 logarithmic value of the input.``2**log2(x) = x``The storage type of ``log2`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L678
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Log2(NdArray @out,
NdArray data)
{
return new Operator("log2")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise Base-2 logarithmic value of the input.``2**log2(x) = x``The storage type of ``log2`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L678
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Log2(NdArray data)
{
return new Operator("log2")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLog(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLog(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLog10(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log10")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLog10(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log10")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLog2(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log2")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLog2(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log2")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise ``log(1 + x)`` value of the input.This function is more accurate than ``log(1 + x)``  for small ``x`` so that:math:`1+x\approx 1`The storage type of ``log1p`` output depends upon the input storage type:   - log1p(default) = default   - log1p(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L703
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Log1P(NdArray @out,
NdArray data)
{
return new Operator("log1p")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise ``log(1 + x)`` value of the input.This function is more accurate than ``log(1 + x)``  for small ``x`` so that:math:`1+x\approx 1`The storage type of ``log1p`` output depends upon the input storage type:   - log1p(default) = default   - log1p(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L703
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Log1P(NdArray data)
{
return new Operator("log1p")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLog1P(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log1p")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLog1P(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_log1p")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns ``exp(x) - 1`` computed element-wise on the input.This function provides greater precision than ``exp(x) - 1`` for small values of ``x``.The storage type of ``expm1`` output depends upon the input storage type:   - expm1(default) = default   - expm1(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L721
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Expm1(NdArray @out,
NdArray data)
{
return new Operator("expm1")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns ``exp(x) - 1`` computed element-wise on the input.This function provides greater precision than ``exp(x) - 1`` for small values of ``x``.The storage type of ``expm1`` output depends upon the input storage type:   - expm1(default) = default   - expm1(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L721
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Expm1(NdArray data)
{
return new Operator("expm1")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardExpm1(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_expm1")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardExpm1(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_expm1")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the gamma function (extension of the factorial function \to the reals), computed element-wise on the input array.The storage type of ``gamma`` output is always dense
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Gamma(NdArray @out,
NdArray data)
{
return new Operator("gamma")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the gamma function (extension of the factorial function \to the reals), computed element-wise on the input array.The storage type of ``gamma`` output is always dense
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Gamma(NdArray data)
{
return new Operator("gamma")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardGamma(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_gamma")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardGamma(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_gamma")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise log of the absolute value of the gamma function \of the input.The storage type of ``gammaln`` output is always dense
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Gammaln(NdArray @out,
NdArray data)
{
return new Operator("gammaln")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise log of the absolute value of the gamma function \of the input.The storage type of ``gammaln`` output is always dense
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Gammaln(NdArray data)
{
return new Operator("gammaln")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardGammaln(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_gammaln")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardGammaln(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_gammaln")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Computes rectified linear... math::   max(features, 0)The storage type of ``relu`` output depends upon the input storage type:   - relu(default) = default   - relu(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L84
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Relu(NdArray @out,
NdArray data)
{
return new Operator("relu")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes rectified linear... math::   max(features, 0)The storage type of ``relu`` output depends upon the input storage type:   - relu(default) = default   - relu(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L84
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Relu(NdArray data)
{
return new Operator("relu")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRelu(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_relu")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRelu(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_relu")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Computes sigmoid of x element-wise... math::   y = 1 / (1 + exp(-x))The storage type of ``sigmoid`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L103
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sigmoid(NdArray @out,
NdArray data)
{
return new Operator("sigmoid")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes sigmoid of x element-wise... math::   y = 1 / (1 + exp(-x))The storage type of ``sigmoid`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L103
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sigmoid(NdArray data)
{
return new Operator("sigmoid")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCos(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_cos")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCos(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_cos")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Computes the element-wise tangent of the input array.The input should be in radians (:math:`2\pi` rad equals 360 degrees)... math::   tan([0, \pi/4, \pi/2]) = [0, 1, -inf]The storage type of ``tan`` output depends upon the input storage type:   - tan(default) = default   - tan(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L83
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Tan(NdArray @out,
NdArray data)
{
return new Operator("tan")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the element-wise tangent of the input array.The input should be in radians (:math:`2\pi` rad equals 360 degrees)... math::   tan([0, \pi/4, \pi/2]) = [0, 1, -inf]The storage type of ``tan`` output depends upon the input storage type:   - tan(default) = default   - tan(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L83
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Tan(NdArray data)
{
return new Operator("tan")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTan(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_tan")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTan(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_tan")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise inverse sine of the input array.The input should be in the range `[-1, 1]`.The output is in the closed interval of [:math:`-\pi/2`, :math:`\pi/2`]... math::   arcsin([-1, -.707, 0, .707, 1]) = [-\pi/2, -\pi/4, 0, \pi/4, \pi/2]The storage type of ``arcsin`` output depends upon the input storage type:   - arcsin(default) = default   - arcsin(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L104
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arcsin(NdArray @out,
NdArray data)
{
return new Operator("arcsin")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise inverse sine of the input array.The input should be in the range `[-1, 1]`.The output is in the closed interval of [:math:`-\pi/2`, :math:`\pi/2`]... math::   arcsin([-1, -.707, 0, .707, 1]) = [-\pi/2, -\pi/4, 0, \pi/4, \pi/2]The storage type of ``arcsin`` output depends upon the input storage type:   - arcsin(default) = default   - arcsin(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L104
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arcsin(NdArray data)
{
return new Operator("arcsin")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArcsin(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arcsin")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArcsin(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arcsin")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise inverse cosine of the input array.The input should be in range `[-1, 1]`.The output is in the closed interval :math:`[0, \pi]`.. math::   arccos([-1, -.707, 0, .707, 1]) = [\pi, 3\pi/4, \pi/2, \pi/4, 0]The storage type of ``arccos`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L123
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arccos(NdArray @out,
NdArray data)
{
return new Operator("arccos")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise inverse cosine of the input array.The input should be in range `[-1, 1]`.The output is in the closed interval :math:`[0, \pi]`.. math::   arccos([-1, -.707, 0, .707, 1]) = [\pi, 3\pi/4, \pi/2, \pi/4, 0]The storage type of ``arccos`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L123
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arccos(NdArray data)
{
return new Operator("arccos")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArccos(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arccos")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArccos(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arccos")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns element-wise inverse tangent of the input array.The output is in the closed interval :math:`[-\pi/2, \pi/2]`.. math::   arctan([-1, 0, 1]) = [-\pi/4, 0, \pi/4]The storage type of ``arctan`` output depends upon the input storage type:   - arctan(default) = default   - arctan(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L144
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arctan(NdArray @out,
NdArray data)
{
return new Operator("arctan")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns element-wise inverse tangent of the input array.The output is in the closed interval :math:`[-\pi/2, \pi/2]`.. math::   arctan([-1, 0, 1]) = [-\pi/4, 0, \pi/4]The storage type of ``arctan`` output depends upon the input storage type:   - arctan(default) = default   - arctan(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L144
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arctan(NdArray data)
{
return new Operator("arctan")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArctan(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arctan")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArctan(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arctan")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Converts each element of the input array from radians to degrees... math::   degrees([0, \pi/2, \pi, 3\pi/2, 2\pi]) = [0, 90, 180, 270, 360]The storage type of ``degrees`` output depends upon the input storage type:   - degrees(default) = default   - degrees(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L163
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Degrees(NdArray @out,
NdArray data)
{
return new Operator("degrees")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Converts each element of the input array from radians to degrees... math::   degrees([0, \pi/2, \pi, 3\pi/2, 2\pi]) = [0, 90, 180, 270, 360]The storage type of ``degrees`` output depends upon the input storage type:   - degrees(default) = default   - degrees(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L163
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Degrees(NdArray data)
{
return new Operator("degrees")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDegrees(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_degrees")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDegrees(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_degrees")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Converts each element of the input array from degrees to radians... math::   radians([0, 90, 180, 270, 360]) = [0, \pi/2, \pi, 3\pi/2, 2\pi]The storage type of ``radians`` output depends upon the input storage type:   - radians(default) = default   - radians(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L182
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Radians(NdArray @out,
NdArray data)
{
return new Operator("radians")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Converts each element of the input array from degrees to radians... math::   radians([0, 90, 180, 270, 360]) = [0, \pi/2, \pi, 3\pi/2, 2\pi]The storage type of ``radians`` output depends upon the input storage type:   - radians(default) = default   - radians(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L182
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Radians(NdArray data)
{
return new Operator("radians")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRadians(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_radians")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRadians(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_radians")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the hyperbolic cosine  of the input array, computed element-wise... math::   cosh(x) = 0.5\times(exp(x) + exp(-x))The storage type of ``cosh`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L216
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cosh(NdArray @out,
NdArray data)
{
return new Operator("cosh")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the hyperbolic cosine  of the input array, computed element-wise... math::   cosh(x) = 0.5\times(exp(x) + exp(-x))The storage type of ``cosh`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L216
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cosh(NdArray data)
{
return new Operator("cosh")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCosh(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_cosh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCosh(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_cosh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the hyperbolic sine of the input array, computed element-wise... math::   sinh(x) = 0.5\times(exp(x) - exp(-x))The storage type of ``sinh`` output depends upon the input storage type:   - sinh(default) = default   - sinh(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L201
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sinh(NdArray @out,
NdArray data)
{
return new Operator("sinh")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the hyperbolic sine of the input array, computed element-wise... math::   sinh(x) = 0.5\times(exp(x) - exp(-x))The storage type of ``sinh`` output depends upon the input storage type:   - sinh(default) = default   - sinh(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L201
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sinh(NdArray data)
{
return new Operator("sinh")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSinh(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sinh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSinh(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sinh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the hyperbolic tangent of the input array, computed element-wise... math::   tanh(x) = sinh(x) / cosh(x)The storage type of ``tanh`` output depends upon the input storage type:   - tanh(default) = default   - tanh(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L234
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Tanh(NdArray @out,
NdArray data)
{
return new Operator("tanh")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the hyperbolic tangent of the input array, computed element-wise... math::   tanh(x) = sinh(x) / cosh(x)The storage type of ``tanh`` output depends upon the input storage type:   - tanh(default) = default   - tanh(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L234
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Tanh(NdArray data)
{
return new Operator("tanh")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTanh(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_tanh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTanh(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_tanh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the element-wise inverse hyperbolic sine of the input array, \computed element-wise.The storage type of ``arcsinh`` output depends upon the input storage type:   - arcsinh(default) = default   - arcsinh(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L250
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arcsinh(NdArray @out,
NdArray data)
{
return new Operator("arcsinh")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the element-wise inverse hyperbolic sine of the input array, \computed element-wise.The storage type of ``arcsinh`` output depends upon the input storage type:   - arcsinh(default) = default   - arcsinh(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L250
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arcsinh(NdArray data)
{
return new Operator("arcsinh")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArcsinh(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arcsinh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArcsinh(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arcsinh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the element-wise inverse hyperbolic cosine of the input array, \computed element-wise.The storage type of ``arccosh`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L264
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arccosh(NdArray @out,
NdArray data)
{
return new Operator("arccosh")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the element-wise inverse hyperbolic cosine of the input array, \computed element-wise.The storage type of ``arccosh`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L264
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arccosh(NdArray data)
{
return new Operator("arccosh")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArccosh(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arccosh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArccosh(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arccosh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Returns the element-wise inverse hyperbolic tangent of the input array, \computed element-wise.The storage type of ``arctanh`` output depends upon the input storage type:   - arctanh(default) = default   - arctanh(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L281
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arctanh(NdArray @out,
NdArray data)
{
return new Operator("arctanh")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the element-wise inverse hyperbolic tangent of the input array, \computed element-wise.The storage type of ``arctanh`` output depends upon the input storage type:   - arctanh(default) = default   - arctanh(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L281
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arctanh(NdArray data)
{
return new Operator("arctanh")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArctanh(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arctanh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardArctanh(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_arctanh")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Computes the element-wise sine of the input array.The input should be in radians (:math:`2\pi` rad equals 360 degrees)... math::   sin([0, \pi/4, \pi/2]) = [0, 0.707, 1]The storage type of ``sin`` output depends upon the input storage type:   - sin(default) = default   - sin(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L46
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sin(NdArray @out,
NdArray data)
{
return new Operator("sin")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the element-wise sine of the input array.The input should be in radians (:math:`2\pi` rad equals 360 degrees)... math::   sin([0, \pi/4, \pi/2]) = [0, 0.707, 1]The storage type of ``sin`` output depends upon the input storage type:   - sin(default) = default   - sin(row_sparse) = row_sparseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L46
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sin(NdArray data)
{
return new Operator("sin")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSin(NdArray @out,
NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sin")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">first input</param>
/// <param name="rhs">second input</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSin(NdArray lhs,
NdArray rhs)
{
return new Operator("_backward_sin")
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// Computes the element-wise cosine of the input array.The input should be in radians (:math:`2\pi` rad equals 360 degrees)... math::   cos([0, \pi/4, \pi/2]) = [1, 0.707, 0]The storage type of ``cos`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L63
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cos(NdArray @out,
NdArray data)
{
return new Operator("cos")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the element-wise cosine of the input array.The input should be in radians (:math:`2\pi` rad equals 360 degrees)... math::   cos([0, \pi/4, \pi/2]) = [1, 0.707, 0]The storage type of ``cos`` output is always denseDefined in I:\workspace\incubator-mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L63
/// </summary>
/// <param name="data">The input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cos(NdArray data)
{
return new Operator("cos")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Maps integer indices to vector representations (embeddings).This operator maps words to real-valued vectors in a high-dimensional space,called word embeddings. These embeddings can capture semantic and syntactic properties of the words.For example, it has been noted that in the learned embedding spaces, similar words tendto be close to each other and dissimilar words far apart.For an input array of shape (d1, ..., dK),the shape of an output array is (d1, ..., dK, output_dim).All the input values should be integers in the range [0, input_dim).If the input_dim is ip0 and output_dim is op0, then shape of the embedding weight matrix must be(ip0, op0).The storage type of weight must be `row_sparse`, and the gradient of the weight will be of`row_sparse` storage type, too... Note::    `SparseEmbedding` is designed for the use case where `input_dim` is very large (e.g. 100k).    The operator is available on both CPU and GPU.Examples::  input_dim = 4  output_dim = 5  // Each row in weight matrix y represents a word. So, y = (w0,w1,w2,w3)  y = [[  0.,   1.,   2.,   3.,   4.],       [  5.,   6.,   7.,   8.,   9.],       [ 10.,  11.,  12.,  13.,  14.],       [ 15.,  16.,  17.,  18.,  19.]]  // Input array x represents n-grams(2-gram). So, x = [(w1,w3), (w0,w2)]  x = [[ 1.,  3.],       [ 0.,  2.]]  // Mapped input x to its vector representation y.  SparseEmbedding(x, y, 4, 5) = [[[  5.,   6.,   7.,   8.,   9.],                                 [ 15.,  16.,  17.,  18.,  19.]],                                [[  0.,   1.,   2.,   3.,   4.],                                 [ 10.,  11.,  12.,  13.,  14.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L294
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array to the embedding operator.</param>
/// <param name="input_dim">Vocabulary size of the input indices.</param>
/// <param name="output_dim">Dimension of the embedding vectors.</param>
/// <param name="weight">The embedding weight matrix.</param>
/// <param name="dtype">Data type of weight.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribSparseEmbedding(NdArray @out,
NdArray data,
int input_dim,
int output_dim,
NdArray weight=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_contrib_SparseEmbedding")
.SetParam("input_dim", input_dim)
.SetParam("output_dim", output_dim)
.SetParam("dtype", dtype)
.SetInput("data", data)
.SetInput("weight", weight)
.Invoke(@out);
}
/// <summary>
/// Maps integer indices to vector representations (embeddings).This operator maps words to real-valued vectors in a high-dimensional space,called word embeddings. These embeddings can capture semantic and syntactic properties of the words.For example, it has been noted that in the learned embedding spaces, similar words tendto be close to each other and dissimilar words far apart.For an input array of shape (d1, ..., dK),the shape of an output array is (d1, ..., dK, output_dim).All the input values should be integers in the range [0, input_dim).If the input_dim is ip0 and output_dim is op0, then shape of the embedding weight matrix must be(ip0, op0).The storage type of weight must be `row_sparse`, and the gradient of the weight will be of`row_sparse` storage type, too... Note::    `SparseEmbedding` is designed for the use case where `input_dim` is very large (e.g. 100k).    The operator is available on both CPU and GPU.Examples::  input_dim = 4  output_dim = 5  // Each row in weight matrix y represents a word. So, y = (w0,w1,w2,w3)  y = [[  0.,   1.,   2.,   3.,   4.],       [  5.,   6.,   7.,   8.,   9.],       [ 10.,  11.,  12.,  13.,  14.],       [ 15.,  16.,  17.,  18.,  19.]]  // Input array x represents n-grams(2-gram). So, x = [(w1,w3), (w0,w2)]  x = [[ 1.,  3.],       [ 0.,  2.]]  // Mapped input x to its vector representation y.  SparseEmbedding(x, y, 4, 5) = [[[  5.,   6.,   7.,   8.,   9.],                                 [ 15.,  16.,  17.,  18.,  19.]],                                [[  0.,   1.,   2.,   3.,   4.],                                 [ 10.,  11.,  12.,  13.,  14.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L294
/// </summary>
/// <param name="data">The input array to the embedding operator.</param>
/// <param name="input_dim">Vocabulary size of the input indices.</param>
/// <param name="output_dim">Dimension of the embedding vectors.</param>
/// <param name="weight">The embedding weight matrix.</param>
/// <param name="dtype">Data type of weight.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribSparseEmbedding(NdArray data,
int input_dim,
int output_dim,
NdArray weight=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_contrib_SparseEmbedding")
.SetParam("input_dim", input_dim)
.SetParam("output_dim", output_dim)
.SetParam("dtype", dtype)
.SetInput("data", data)
.SetInput("weight", weight)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardEmbedding(NdArray @out)
{
return new Operator("_backward_Embedding")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardEmbedding()
{
return new Operator("_backward_Embedding")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSparseEmbedding(NdArray @out)
{
return new Operator("_backward_SparseEmbedding")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSparseEmbedding()
{
return new Operator("_backward_SparseEmbedding")
.Invoke();
}
private static readonly List<string> TakeModeConvert = new List<string>(){"clip","raise","wrap"};
/// <summary>
/// Takes elements from an input array along the given axis.This function slices the input array along a particular axis with the provided indices.Given an input array with shape ``(d0, d1, d2)`` and indices with shape ``(i0, i1)``, the outputwill have shape ``(i0, i1, d1, d2)``, computed by::  output[i,j,:,:] = input[indices[i,j],:,:].. note::   - `axis`- Only slicing along axis 0 is supported for now.   - `mode`- Only `clip` mode is supported for now.Examples::  x = [[ 1.,  2.],       [ 3.,  4.],       [ 5.,  6.]]  // takes elements with specified indices along axis 0  take(x, [[0,1],[1,2]]) = [[[ 1.,  2.],                             [ 3.,  4.]],                            [[ 3.,  4.],                             [ 5.,  6.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L367
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="a">The input array.</param>
/// <param name="indices">The indices of the values to be extracted.</param>
/// <param name="axis">The axis of input array to be taken.</param>
/// <param name="mode">Specify how out-of-bound indices bahave. "clip" means clip to the range. So, if all indices mentioned are too large, they are replaced by the index that addresses the last element along an axis.  "wrap" means to wrap around.  "raise" means to raise an error. </param>
 /// <returns>returns new symbol</returns>
public static NdArray Take(NdArray @out,
NdArray a,
NdArray indices,
int axis=0,
TakeMode mode=TakeMode.Clip)
{
return new Operator("take")
.SetParam("axis", axis)
.SetParam("mode", Util.EnumToString<TakeMode>(mode,TakeModeConvert))
.SetInput("a", a)
.SetInput("indices", indices)
.Invoke(@out);
}
/// <summary>
/// Takes elements from an input array along the given axis.This function slices the input array along a particular axis with the provided indices.Given an input array with shape ``(d0, d1, d2)`` and indices with shape ``(i0, i1)``, the outputwill have shape ``(i0, i1, d1, d2)``, computed by::  output[i,j,:,:] = input[indices[i,j],:,:].. note::   - `axis`- Only slicing along axis 0 is supported for now.   - `mode`- Only `clip` mode is supported for now.Examples::  x = [[ 1.,  2.],       [ 3.,  4.],       [ 5.,  6.]]  // takes elements with specified indices along axis 0  take(x, [[0,1],[1,2]]) = [[[ 1.,  2.],                             [ 3.,  4.]],                            [[ 3.,  4.],                             [ 5.,  6.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L367
/// </summary>
/// <param name="a">The input array.</param>
/// <param name="indices">The indices of the values to be extracted.</param>
/// <param name="axis">The axis of input array to be taken.</param>
/// <param name="mode">Specify how out-of-bound indices bahave. "clip" means clip to the range. So, if all indices mentioned are too large, they are replaced by the index that addresses the last element along an axis.  "wrap" means to wrap around.  "raise" means to raise an error. </param>
 /// <returns>returns new symbol</returns>
public static NdArray Take(NdArray a,
NdArray indices,
int axis=0,
TakeMode mode=TakeMode.Clip)
{
return new Operator("take")
.SetParam("axis", axis)
.SetParam("mode", Util.EnumToString<TakeMode>(mode,TakeModeConvert))
.SetInput("a", a)
.SetInput("indices", indices)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTake(NdArray @out)
{
return new Operator("_backward_take")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTake()
{
return new Operator("_backward_take")
.Invoke();
}
/// <summary>
/// Takes elements from a data batch... note::  `batch_take` is deprecated. Use `pick` instead.Given an input array of shape ``(d0, d1)`` and indices of shape ``(i0,)``, the result will bean output array of shape ``(i0,)`` with::  output[i] = input[i, indices[i]]Examples::  x = [[ 1.,  2.],       [ 3.,  4.],       [ 5.,  6.]]  // takes elements with specified indices  batch_take(x, [0,1,0]) = [ 1.  4.  5.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L422
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="a">The input array</param>
/// <param name="indices">The index array</param>
 /// <returns>returns new symbol</returns>
public static NdArray BatchTake(NdArray @out,
NdArray a,
NdArray indices)
{
return new Operator("batch_take")
.SetInput("a", a)
.SetInput("indices", indices)
.Invoke(@out);
}
/// <summary>
/// Takes elements from a data batch... note::  `batch_take` is deprecated. Use `pick` instead.Given an input array of shape ``(d0, d1)`` and indices of shape ``(i0,)``, the result will bean output array of shape ``(i0,)`` with::  output[i] = input[i, indices[i]]Examples::  x = [[ 1.,  2.],       [ 3.,  4.],       [ 5.,  6.]]  // takes elements with specified indices  batch_take(x, [0,1,0]) = [ 1.  4.  5.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L422
/// </summary>
/// <param name="a">The input array</param>
/// <param name="indices">The index array</param>
 /// <returns>returns new symbol</returns>
public static NdArray BatchTake(NdArray a,
NdArray indices)
{
return new Operator("batch_take")
.SetInput("a", a)
.SetInput("indices", indices)
.Invoke();
}
/// <summary>
/// Returns a one-hot array.The locations represented by `indices` take value `on_value`, while allother locations take value `off_value`.`one_hot` operation with `indices` of shape ``(i0, i1)`` and `depth`  of ``d`` would resultin an output array of shape ``(i0, i1, d)`` with::  output[i,j,:] = off_value  output[i,j,indices[i,j]] = on_valueExamples::  one_hot([1,0,2,0], 3) = [[ 0.  1.  0.]                           [ 1.  0.  0.]                           [ 0.  0.  1.]                           [ 1.  0.  0.]]  one_hot([1,0,2,0], 3, on_value=8, off_value=1,          dtype='int32') = [[1 8 1]                            [8 1 1]                            [1 1 8]                            [8 1 1]]  one_hot([[1,0],[1,0],[2,0]], 3) = [[[ 0.  1.  0.]                                      [ 1.  0.  0.]]                                     [[ 0.  1.  0.]                                      [ 1.  0.  0.]]                                     [[ 0.  0.  1.]                                      [ 1.  0.  0.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L468
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="indices">array of locations where to set on_value</param>
/// <param name="depth">Depth of the one hot dimension.</param>
/// <param name="on_value">The value assigned to the locations represented by indices.</param>
/// <param name="off_value">The value assigned to the locations not represented by indices.</param>
/// <param name="dtype">DType of the output</param>
 /// <returns>returns new symbol</returns>
public static NdArray OneHot(NdArray @out,
NdArray indices,
int depth,
double on_value=1,
double off_value=0,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("one_hot")
.SetParam("depth", depth)
.SetParam("on_value", on_value)
.SetParam("off_value", off_value)
.SetParam("dtype", dtype)
.SetInput("indices", indices)
.Invoke(@out);
}
/// <summary>
/// Returns a one-hot array.The locations represented by `indices` take value `on_value`, while allother locations take value `off_value`.`one_hot` operation with `indices` of shape ``(i0, i1)`` and `depth`  of ``d`` would resultin an output array of shape ``(i0, i1, d)`` with::  output[i,j,:] = off_value  output[i,j,indices[i,j]] = on_valueExamples::  one_hot([1,0,2,0], 3) = [[ 0.  1.  0.]                           [ 1.  0.  0.]                           [ 0.  0.  1.]                           [ 1.  0.  0.]]  one_hot([1,0,2,0], 3, on_value=8, off_value=1,          dtype='int32') = [[1 8 1]                            [8 1 1]                            [1 1 8]                            [8 1 1]]  one_hot([[1,0],[1,0],[2,0]], 3) = [[[ 0.  1.  0.]                                      [ 1.  0.  0.]]                                     [[ 0.  1.  0.]                                      [ 1.  0.  0.]]                                     [[ 0.  0.  1.]                                      [ 1.  0.  0.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L468
/// </summary>
/// <param name="indices">array of locations where to set on_value</param>
/// <param name="depth">Depth of the one hot dimension.</param>
/// <param name="on_value">The value assigned to the locations represented by indices.</param>
/// <param name="off_value">The value assigned to the locations not represented by indices.</param>
/// <param name="dtype">DType of the output</param>
 /// <returns>returns new symbol</returns>
public static NdArray OneHot(NdArray indices,
int depth,
double on_value=1,
double off_value=0,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("one_hot")
.SetParam("depth", depth)
.SetParam("on_value", on_value)
.SetParam("off_value", off_value)
.SetParam("dtype", dtype)
.SetInput("indices", indices)
.Invoke();
}
/// <summary>
/// Gather elements or slices from `data` and store to a tensor whoseshape is defined by `indices`.Given `data` with shape `(X_0, X_1, ..., X_{N-1})` and indices with shape`(M, Y_0, ..., Y_{K-1})`, the output will have shape `(Y_0, ..., Y_{K-1}, X_M, ..., X_{N-1})`,where `M <= N`. If `M == N`, output shape will simply be `(Y_0, ..., Y_{K-1})`.The elements in output is defined as follows::  output[y_0, ..., y_{K-1}, x_M, ..., x_{N-1}] = data[indices[0, y_0, ..., y_{K-1}],                                                      ...,                                                      indices[M-1, y_0, ..., y_{K-1}],                                                      x_M, ..., x_{N-1}]Examples::  data = [[0, 1], [2, 3]]  indices = [[1, 1, 0], [0, 1, 0]]  gather_nd(data, indices) = [2, 3, 0]
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">data</param>
/// <param name="indices">indices</param>
 /// <returns>returns new symbol</returns>
public static NdArray GatherNd(NdArray @out,
NdArray data,
NdArray indices)
{
return new Operator("gather_nd")
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke(@out);
}
/// <summary>
/// Gather elements or slices from `data` and store to a tensor whoseshape is defined by `indices`.Given `data` with shape `(X_0, X_1, ..., X_{N-1})` and indices with shape`(M, Y_0, ..., Y_{K-1})`, the output will have shape `(Y_0, ..., Y_{K-1}, X_M, ..., X_{N-1})`,where `M <= N`. If `M == N`, output shape will simply be `(Y_0, ..., Y_{K-1})`.The elements in output is defined as follows::  output[y_0, ..., y_{K-1}, x_M, ..., x_{N-1}] = data[indices[0, y_0, ..., y_{K-1}],                                                      ...,                                                      indices[M-1, y_0, ..., y_{K-1}],                                                      x_M, ..., x_{N-1}]Examples::  data = [[0, 1], [2, 3]]  indices = [[1, 1, 0], [0, 1, 0]]  gather_nd(data, indices) = [2, 3, 0]
/// </summary>
/// <param name="data">data</param>
/// <param name="indices">indices</param>
 /// <returns>returns new symbol</returns>
public static NdArray GatherNd(NdArray data,
NdArray indices)
{
return new Operator("gather_nd")
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke();
}
/// <summary>
/// Scatters data into a new tensor according to indices.Given `data` with shape `(Y_0, ..., Y_{K-1}, X_M, ..., X_{N-1})` and indices with shape`(M, Y_0, ..., Y_{K-1})`, the output will have shape `(X_0, X_1, ..., X_{N-1})`,where `M <= N`. If `M == N`, data shape should simply be `(Y_0, ..., Y_{K-1})`.The elements in output is defined as follows::  output[indices[0, y_0, ..., y_{K-1}],         ...,         indices[M-1, y_0, ..., y_{K-1}],         x_M, ..., x_{N-1}] = data[y_0, ..., y_{K-1}, x_M, ..., x_{N-1}]all other entries in output are 0... warning::    If the indices have duplicates, the result will be non-deterministic and    the gradient of `scatter_nd` will not be correct!!Examples::  data = [2, 3, 0]  indices = [[1, 1, 0], [0, 1, 0]]  shape = (2, 2)  scatter_nd(data, indices, shape) = [[0, 0], [2, 3]]
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">data</param>
/// <param name="indices">indices</param>
/// <param name="shape">Shape of output.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterNd(NdArray @out,
NdArray data,
NdArray indices,
Shape shape)
{
return new Operator("scatter_nd")
.SetParam("shape", shape)
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke(@out);
}
/// <summary>
/// Scatters data into a new tensor according to indices.Given `data` with shape `(Y_0, ..., Y_{K-1}, X_M, ..., X_{N-1})` and indices with shape`(M, Y_0, ..., Y_{K-1})`, the output will have shape `(X_0, X_1, ..., X_{N-1})`,where `M <= N`. If `M == N`, data shape should simply be `(Y_0, ..., Y_{K-1})`.The elements in output is defined as follows::  output[indices[0, y_0, ..., y_{K-1}],         ...,         indices[M-1, y_0, ..., y_{K-1}],         x_M, ..., x_{N-1}] = data[y_0, ..., y_{K-1}, x_M, ..., x_{N-1}]all other entries in output are 0... warning::    If the indices have duplicates, the result will be non-deterministic and    the gradient of `scatter_nd` will not be correct!!Examples::  data = [2, 3, 0]  indices = [[1, 1, 0], [0, 1, 0]]  shape = (2, 2)  scatter_nd(data, indices, shape) = [[0, 0], [2, 3]]
/// </summary>
/// <param name="data">data</param>
/// <param name="indices">indices</param>
/// <param name="shape">Shape of output.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterNd(NdArray data,
NdArray indices,
Shape shape)
{
return new Operator("scatter_nd")
.SetParam("shape", shape)
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke();
}
/// <summary>
/// Accumulates data according to indices and get the result. It's the backward of`gather_nd`.Given `data` with shape `(Y_0, ..., Y_{K-1}, X_M, ..., X_{N-1})` and indices with shape`(M, Y_0, ..., Y_{K-1})`, the output will have shape `(X_0, X_1, ..., X_{N-1})`,where `M <= N`. If `M == N`, data shape should simply be `(Y_0, ..., Y_{K-1})`.The elements in output is defined as follows::  output[indices[0, y_0, ..., y_{K-1}],         ...,         indices[M-1, y_0, ..., y_{K-1}],         x_M, ..., x_{N-1}] += data[y_0, ..., y_{K-1}, x_M, ..., x_{N-1}]all other entries in output are 0 or the original value if AddTo is triggered.Examples::  data = [2, 3, 0]  indices = [[1, 1, 0], [0, 1, 0]]  shape = (2, 2)  _backward_gather_nd(data, indices, shape) = [[0, 0], [2, 3]] # Same as scatter_nd  # The difference between scatter_nd and scatter_nd_acc is the latter will accumulate  #  the values that point to the same index.  data = [2, 3, 0]  indices = [[1, 1, 0], [1, 1, 0]]  shape = (2, 2)  _backward_gather_nd(data, indices, shape) = [[0, 0], [0, 5]]
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">data</param>
/// <param name="indices">indices</param>
/// <param name="shape">Shape of output.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardGatherNd(NdArray @out,
NdArray data,
NdArray indices,
Shape shape)
{
return new Operator("_backward_gather_nd")
.SetParam("shape", shape)
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke(@out);
}
/// <summary>
/// Accumulates data according to indices and get the result. It's the backward of`gather_nd`.Given `data` with shape `(Y_0, ..., Y_{K-1}, X_M, ..., X_{N-1})` and indices with shape`(M, Y_0, ..., Y_{K-1})`, the output will have shape `(X_0, X_1, ..., X_{N-1})`,where `M <= N`. If `M == N`, data shape should simply be `(Y_0, ..., Y_{K-1})`.The elements in output is defined as follows::  output[indices[0, y_0, ..., y_{K-1}],         ...,         indices[M-1, y_0, ..., y_{K-1}],         x_M, ..., x_{N-1}] += data[y_0, ..., y_{K-1}, x_M, ..., x_{N-1}]all other entries in output are 0 or the original value if AddTo is triggered.Examples::  data = [2, 3, 0]  indices = [[1, 1, 0], [0, 1, 0]]  shape = (2, 2)  _backward_gather_nd(data, indices, shape) = [[0, 0], [2, 3]] # Same as scatter_nd  # The difference between scatter_nd and scatter_nd_acc is the latter will accumulate  #  the values that point to the same index.  data = [2, 3, 0]  indices = [[1, 1, 0], [1, 1, 0]]  shape = (2, 2)  _backward_gather_nd(data, indices, shape) = [[0, 0], [0, 5]]
/// </summary>
/// <param name="data">data</param>
/// <param name="indices">indices</param>
/// <param name="shape">Shape of output.</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardGatherNd(NdArray data,
NdArray indices,
Shape shape)
{
return new Operator("_backward_gather_nd")
.SetParam("shape", shape)
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke();
}
/// <summary>
/// This operator has the same functionality as scatter_ndexcept that it does not reset the elements not indexed by the inputindex `NDArray` in the input data `NDArray`... note:: This operator is for internal use only.Examples::  data = [2, 3, 0]  indices = [[1, 1, 0], [0, 1, 0]]  out = [[1, 1], [1, 1]]  scatter_nd(data=data, indices=indices, out=out)  out = [[0, 1], [2, 3]]
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">data</param>
/// <param name="indices">indices</param>
/// <param name="shape">Shape of output.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterSetNd(NdArray @out,
NdArray data,
NdArray indices,
Shape shape)
{
return new Operator("_scatter_set_nd")
.SetParam("shape", shape)
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke(@out);
}
/// <summary>
/// This operator has the same functionality as scatter_ndexcept that it does not reset the elements not indexed by the inputindex `NDArray` in the input data `NDArray`... note:: This operator is for internal use only.Examples::  data = [2, 3, 0]  indices = [[1, 1, 0], [0, 1, 0]]  out = [[1, 1], [1, 1]]  scatter_nd(data=data, indices=indices, out=out)  out = [[0, 1], [2, 3]]
/// </summary>
/// <param name="data">data</param>
/// <param name="indices">indices</param>
/// <param name="shape">Shape of output.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ScatterSetNd(NdArray data,
NdArray indices,
Shape shape)
{
return new Operator("_scatter_set_nd")
.SetParam("shape", shape)
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke();
}
/// <summary>
/// Maps integer indices to vector representations (embeddings).This operator maps words to real-valued vectors in a high-dimensional space,called word embeddings. These embeddings can capture semantic and syntactic properties of the words.For example, it has been noted that in the learned embedding spaces, similar words tendto be close to each other and dissimilar words far apart.For an input array of shape (d1, ..., dK),the shape of an output array is (d1, ..., dK, output_dim).All the input values should be integers in the range [0, input_dim).If the input_dim is ip0 and output_dim is op0, then shape of the embedding weight matrix must be(ip0, op0).By default, if any index mentioned is too large, it is replaced by the index that addressesthe last vector in an embedding matrix.Examples::  input_dim = 4  output_dim = 5  // Each row in weight matrix y represents a word. So, y = (w0,w1,w2,w3)  y = [[  0.,   1.,   2.,   3.,   4.],       [  5.,   6.,   7.,   8.,   9.],       [ 10.,  11.,  12.,  13.,  14.],       [ 15.,  16.,  17.,  18.,  19.]]  // Input array x represents n-grams(2-gram). So, x = [(w1,w3), (w0,w2)]  x = [[ 1.,  3.],       [ 0.,  2.]]  // Mapped input x to its vector representation y.  Embedding(x, y, 4, 5) = [[[  5.,   6.,   7.,   8.,   9.],                            [ 15.,  16.,  17.,  18.,  19.]],                           [[  0.,   1.,   2.,   3.,   4.],                            [ 10.,  11.,  12.,  13.,  14.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L225
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array to the embedding operator.</param>
/// <param name="input_dim">Vocabulary size of the input indices.</param>
/// <param name="output_dim">Dimension of the embedding vectors.</param>
/// <param name="weight">The embedding weight matrix.</param>
/// <param name="dtype">Data type of weight.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Embedding(NdArray @out,
NdArray data,
int input_dim,
int output_dim,
NdArray weight=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("Embedding")
.SetParam("input_dim", input_dim)
.SetParam("output_dim", output_dim)
.SetParam("dtype", dtype)
.SetInput("data", data)
.SetInput("weight", weight)
.Invoke(@out);
}
/// <summary>
/// Maps integer indices to vector representations (embeddings).This operator maps words to real-valued vectors in a high-dimensional space,called word embeddings. These embeddings can capture semantic and syntactic properties of the words.For example, it has been noted that in the learned embedding spaces, similar words tendto be close to each other and dissimilar words far apart.For an input array of shape (d1, ..., dK),the shape of an output array is (d1, ..., dK, output_dim).All the input values should be integers in the range [0, input_dim).If the input_dim is ip0 and output_dim is op0, then shape of the embedding weight matrix must be(ip0, op0).By default, if any index mentioned is too large, it is replaced by the index that addressesthe last vector in an embedding matrix.Examples::  input_dim = 4  output_dim = 5  // Each row in weight matrix y represents a word. So, y = (w0,w1,w2,w3)  y = [[  0.,   1.,   2.,   3.,   4.],       [  5.,   6.,   7.,   8.,   9.],       [ 10.,  11.,  12.,  13.,  14.],       [ 15.,  16.,  17.,  18.,  19.]]  // Input array x represents n-grams(2-gram). So, x = [(w1,w3), (w0,w2)]  x = [[ 1.,  3.],       [ 0.,  2.]]  // Mapped input x to its vector representation y.  Embedding(x, y, 4, 5) = [[[  5.,   6.,   7.,   8.,   9.],                            [ 15.,  16.,  17.,  18.,  19.]],                           [[  0.,   1.,   2.,   3.,   4.],                            [ 10.,  11.,  12.,  13.,  14.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\indexing_op.cc:L225
/// </summary>
/// <param name="data">The input array to the embedding operator.</param>
/// <param name="input_dim">Vocabulary size of the input indices.</param>
/// <param name="output_dim">Dimension of the embedding vectors.</param>
/// <param name="weight">The embedding weight matrix.</param>
/// <param name="dtype">Data type of weight.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Embedding(NdArray data,
int input_dim,
int output_dim,
NdArray weight=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("Embedding")
.SetParam("input_dim", input_dim)
.SetParam("output_dim", output_dim)
.SetParam("dtype", dtype)
.SetInput("data", data)
.SetInput("weight", weight)
.Invoke();
}
/// <summary>
/// Return an array of zeros with the same shape and typeas the input array.The storage type of ``zeros_like`` output depends on the storage type of the input- zeros_like(row_sparse) = row_sparse- zeros_like(csr) = csr- zeros_like(default) = defaultExamples::  x = [[ 1.,  1.,  1.],       [ 1.,  1.,  1.]]  zeros_like(x) = [[ 0.,  0.,  0.],                   [ 0.,  0.,  0.]]
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ZerosLike(NdArray @out,
NdArray data)
{
return new Operator("zeros_like")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Return an array of zeros with the same shape and typeas the input array.The storage type of ``zeros_like`` output depends on the storage type of the input- zeros_like(row_sparse) = row_sparse- zeros_like(csr) = csr- zeros_like(default) = defaultExamples::  x = [[ 1.,  1.,  1.],       [ 1.,  1.,  1.]]  zeros_like(x) = [[ 0.,  0.,  0.],                   [ 0.,  0.,  0.]]
/// </summary>
/// <param name="data">The input</param>
 /// <returns>returns new symbol</returns>
public static NdArray ZerosLike(NdArray data)
{
return new Operator("zeros_like")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Return an array of ones with the same shape and typeas the input array.Examples::  x = [[ 0.,  0.,  0.],       [ 0.,  0.,  0.]]  ones_like(x) = [[ 1.,  1.,  1.],                  [ 1.,  1.,  1.]]
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
 /// <returns>returns new symbol</returns>
public static NdArray OnesLike(NdArray @out,
NdArray data)
{
return new Operator("ones_like")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Return an array of ones with the same shape and typeas the input array.Examples::  x = [[ 0.,  0.,  0.],       [ 0.,  0.,  0.]]  ones_like(x) = [[ 1.,  1.,  1.],                  [ 1.,  1.,  1.]]
/// </summary>
/// <param name="data">The input</param>
 /// <returns>returns new symbol</returns>
public static NdArray OnesLike(NdArray data)
{
return new Operator("ones_like")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// fill target with zeros
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="shape">The shape of the output</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
/// <param name="dtype">Target data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Zeros(NdArray @out,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_zeros")
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// fill target with zeros
/// </summary>
/// <param name="shape">The shape of the output</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
/// <param name="dtype">Target data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Zeros(Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_zeros")
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// fill target with ones
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="shape">The shape of the output</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
/// <param name="dtype">Target data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Ones(NdArray @out,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_ones")
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// fill target with ones
/// </summary>
/// <param name="shape">The shape of the output</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
/// <param name="dtype">Target data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Ones(Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_ones")
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// fill target with a scalar value
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="value">Value with which to fill newly created tensor</param>
/// <param name="shape">The shape of the output</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
/// <param name="dtype">Target data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Full(NdArray @out,
double value,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_full")
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.SetParam("value", value)
.Invoke(@out);
}
/// <summary>
/// fill target with a scalar value
/// </summary>
/// <param name="value">Value with which to fill newly created tensor</param>
/// <param name="shape">The shape of the output</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
/// <param name="dtype">Target data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Full(double value,
Shape shape=null,
Context ctx=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_full")
.SetParam("shape", shape)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.SetParam("value", value)
.Invoke();
}
/// <summary>
/// Return evenly spaced values within a given interval. Similar to Numpy
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="start">Start of interval. The interval includes this value. The default start value is 0.</param>
/// <param name="stop">End of interval. The interval does not include this value, except in some cases where step is not an integer and floating point round-off affects the length of out.</param>
/// <param name="step">Spacing between values.</param>
/// <param name="repeat">The repeating time of all elements. E.g repeat=3, the element a will be repeated three times --> a, a, a.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
/// <param name="dtype">Target data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arange(NdArray @out,
double start,
 double? stop=null,
double step=1,
int repeat=1,
Context ctx=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_arange")
.SetParam("start", start)
.SetParam("stop", stop)
.SetParam("step", step)
.SetParam("repeat", repeat)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke(@out);
}
/// <summary>
/// Return evenly spaced values within a given interval. Similar to Numpy
/// </summary>
/// <param name="start">Start of interval. The interval includes this value. The default start value is 0.</param>
/// <param name="stop">End of interval. The interval does not include this value, except in some cases where step is not an integer and floating point round-off affects the length of out.</param>
/// <param name="step">Spacing between values.</param>
/// <param name="repeat">The repeating time of all elements. E.g repeat=3, the element a will be repeated three times --> a, a, a.</param>
/// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
/// <param name="dtype">Target data type.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Arange(double start,
 double? stop=null,
double step=1,
int repeat=1,
Context ctx=null,
Dtype dtype=null)
{if(dtype==null){ dtype= Dtype.Float32;}

return new Operator("_arange")
.SetParam("start", start)
.SetParam("stop", stop)
.SetParam("step", step)
.SetParam("repeat", repeat)
.SetParam("ctx", ctx)
.SetParam("dtype", dtype)
.Invoke();
}
/// <summary>
/// Performs general matrix multiplication and accumulation.Input are tensors *A*, *B*, *C*, each of dimension *n >= 2* and having the same shapeon the leading *n-2* dimensions.If *n=2*, the BLAS3 function *gemm* is performed:   *out* = *alpha* \* *op*\ (*A*) \* *op*\ (*B*) + *beta* \* *C*Here, *alpha* and *beta* are scalar parameters, and *op()* is either the identity ormatrix transposition (depending on *transpose_a*, *transpose_b*).If *n>2*, *gemm* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix multiply-add   A = [[1.0, 1.0], [1.0, 1.0]]   B = [[1.0, 1.0], [1.0, 1.0], [1.0, 1.0]]   C = [[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]   gemm(A, B, C, transpose_b=True, alpha=2.0, beta=10.0)           = [[14.0, 14.0, 14.0], [14.0, 14.0, 14.0]]   // Batch matrix multiply-add   A = [[[1.0, 1.0]], [[0.1, 0.1]]]   B = [[[1.0, 1.0]], [[0.1, 0.1]]]   C = [[[10.0]], [[0.01]]]   gemm(A, B, C, transpose_b=True, alpha=2.0 , beta=10.0)           = [[[104.0]], [[0.14]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L69
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of input matrices</param>
/// <param name="B">Tensor of input matrices</param>
/// <param name="C">Tensor of input matrices</param>
/// <param name="transpose_a">Multiply with transposed of first input (A).</param>
/// <param name="transpose_b">Multiply with transposed of second input (B).</param>
/// <param name="alpha">Scalar factor multiplied with A*B.</param>
/// <param name="beta">Scalar factor multiplied with C.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgGemm(NdArray @out,
NdArray A,
NdArray B,
NdArray C,
bool transpose_a=false,
bool transpose_b=false,
double alpha=1,
double beta=1)
{
return new Operator("_linalg_gemm")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.SetParam("alpha", alpha)
.SetParam("beta", beta)
.SetInput("A", A)
.SetInput("B", B)
.SetInput("C", C)
.Invoke(@out);
}
/// <summary>
/// Performs general matrix multiplication and accumulation.Input are tensors *A*, *B*, *C*, each of dimension *n >= 2* and having the same shapeon the leading *n-2* dimensions.If *n=2*, the BLAS3 function *gemm* is performed:   *out* = *alpha* \* *op*\ (*A*) \* *op*\ (*B*) + *beta* \* *C*Here, *alpha* and *beta* are scalar parameters, and *op()* is either the identity ormatrix transposition (depending on *transpose_a*, *transpose_b*).If *n>2*, *gemm* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix multiply-add   A = [[1.0, 1.0], [1.0, 1.0]]   B = [[1.0, 1.0], [1.0, 1.0], [1.0, 1.0]]   C = [[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]   gemm(A, B, C, transpose_b=True, alpha=2.0, beta=10.0)           = [[14.0, 14.0, 14.0], [14.0, 14.0, 14.0]]   // Batch matrix multiply-add   A = [[[1.0, 1.0]], [[0.1, 0.1]]]   B = [[[1.0, 1.0]], [[0.1, 0.1]]]   C = [[[10.0]], [[0.01]]]   gemm(A, B, C, transpose_b=True, alpha=2.0 , beta=10.0)           = [[[104.0]], [[0.14]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L69
/// </summary>
/// <param name="A">Tensor of input matrices</param>
/// <param name="B">Tensor of input matrices</param>
/// <param name="C">Tensor of input matrices</param>
/// <param name="transpose_a">Multiply with transposed of first input (A).</param>
/// <param name="transpose_b">Multiply with transposed of second input (B).</param>
/// <param name="alpha">Scalar factor multiplied with A*B.</param>
/// <param name="beta">Scalar factor multiplied with C.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgGemm(NdArray A,
NdArray B,
NdArray C,
bool transpose_a=false,
bool transpose_b=false,
double alpha=1,
double beta=1)
{
return new Operator("_linalg_gemm")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.SetParam("alpha", alpha)
.SetParam("beta", beta)
.SetInput("A", A)
.SetInput("B", B)
.SetInput("C", C)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgGemm(NdArray @out)
{
return new Operator("_backward_linalg_gemm")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgGemm()
{
return new Operator("_backward_linalg_gemm")
.Invoke();
}
/// <summary>
/// Performs general matrix multiplication.Input are tensors *A*, *B*, each of dimension *n >= 2* and having the same shapeon the leading *n-2* dimensions.If *n=2*, the BLAS3 function *gemm* is performed:   *out* = *alpha* \* *op*\ (*A*) \* *op*\ (*B*)Here *alpha* is a scalar parameter and *op()* is either the identity or the matrixtransposition (depending on *transpose_a*, *transpose_b*).If *n>2*, *gemm* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix multiply   A = [[1.0, 1.0], [1.0, 1.0]]   B = [[1.0, 1.0], [1.0, 1.0], [1.0, 1.0]]   gemm2(A, B, transpose_b=True, alpha=2.0)            = [[4.0, 4.0, 4.0], [4.0, 4.0, 4.0]]   // Batch matrix multiply   A = [[[1.0, 1.0]], [[0.1, 0.1]]]   B = [[[1.0, 1.0]], [[0.1, 0.1]]]   gemm2(A, B, transpose_b=True, alpha=2.0)           = [[[4.0]], [[0.04 ]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L128
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of input matrices</param>
/// <param name="B">Tensor of input matrices</param>
/// <param name="transpose_a">Multiply with transposed of first input (A).</param>
/// <param name="transpose_b">Multiply with transposed of second input (B).</param>
/// <param name="alpha">Scalar factor multiplied with A*B.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgGemm2(NdArray @out,
NdArray A,
NdArray B,
bool transpose_a=false,
bool transpose_b=false,
double alpha=1)
{
return new Operator("_linalg_gemm2")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.SetParam("alpha", alpha)
.SetInput("A", A)
.SetInput("B", B)
.Invoke(@out);
}
/// <summary>
/// Performs general matrix multiplication.Input are tensors *A*, *B*, each of dimension *n >= 2* and having the same shapeon the leading *n-2* dimensions.If *n=2*, the BLAS3 function *gemm* is performed:   *out* = *alpha* \* *op*\ (*A*) \* *op*\ (*B*)Here *alpha* is a scalar parameter and *op()* is either the identity or the matrixtransposition (depending on *transpose_a*, *transpose_b*).If *n>2*, *gemm* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix multiply   A = [[1.0, 1.0], [1.0, 1.0]]   B = [[1.0, 1.0], [1.0, 1.0], [1.0, 1.0]]   gemm2(A, B, transpose_b=True, alpha=2.0)            = [[4.0, 4.0, 4.0], [4.0, 4.0, 4.0]]   // Batch matrix multiply   A = [[[1.0, 1.0]], [[0.1, 0.1]]]   B = [[[1.0, 1.0]], [[0.1, 0.1]]]   gemm2(A, B, transpose_b=True, alpha=2.0)           = [[[4.0]], [[0.04 ]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L128
/// </summary>
/// <param name="A">Tensor of input matrices</param>
/// <param name="B">Tensor of input matrices</param>
/// <param name="transpose_a">Multiply with transposed of first input (A).</param>
/// <param name="transpose_b">Multiply with transposed of second input (B).</param>
/// <param name="alpha">Scalar factor multiplied with A*B.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgGemm2(NdArray A,
NdArray B,
bool transpose_a=false,
bool transpose_b=false,
double alpha=1)
{
return new Operator("_linalg_gemm2")
.SetParam("transpose_a", transpose_a)
.SetParam("transpose_b", transpose_b)
.SetParam("alpha", alpha)
.SetInput("A", A)
.SetInput("B", B)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgGemm2(NdArray @out)
{
return new Operator("_backward_linalg_gemm2")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgGemm2()
{
return new Operator("_backward_linalg_gemm2")
.Invoke();
}
/// <summary>
/// Performs multiplication with a lower triangular matrix.Input are tensors *A*, *B*, each of dimension *n >= 2* and having the same shapeon the leading *n-2* dimensions.If *n=2*, *A* must be lower triangular. The operator performs the BLAS3 function*trmm*:   *out* = *alpha* \* *op*\ (*A*) \* *B*if *rightside=False*, or   *out* = *alpha* \* *B* \* *op*\ (*A*)if *rightside=True*. Here, *alpha* is a scalar parameter, and *op()* is either theidentity or the matrix transposition (depending on *transpose*).If *n>2*, *trmm* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single triangular matrix multiply   A = [[1.0, 0], [1.0, 1.0]]   B = [[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]   trmm(A, B, alpha=2.0) = [[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]]   // Batch triangular matrix multiply   A = [[[1.0, 0], [1.0, 1.0]], [[1.0, 0], [1.0, 1.0]]]   B = [[[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]], [[0.5, 0.5, 0.5], [0.5, 0.5, 0.5]]]   trmm(A, B, alpha=2.0) = [[[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]],                            [[1.0, 1.0, 1.0], [2.0, 2.0, 2.0]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L293
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of lower triangular matrices</param>
/// <param name="B">Tensor of matrices</param>
/// <param name="transpose">Use transposed of the triangular matrix</param>
/// <param name="rightside">Multiply triangular matrix from the right to non-triangular one.</param>
/// <param name="alpha">Scalar factor to be applied to the result.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgTrmm(NdArray @out,
NdArray A,
NdArray B,
bool transpose=false,
bool rightside=false,
double alpha=1)
{
return new Operator("_linalg_trmm")
.SetParam("transpose", transpose)
.SetParam("rightside", rightside)
.SetParam("alpha", alpha)
.SetInput("A", A)
.SetInput("B", B)
.Invoke(@out);
}
/// <summary>
/// Performs multiplication with a lower triangular matrix.Input are tensors *A*, *B*, each of dimension *n >= 2* and having the same shapeon the leading *n-2* dimensions.If *n=2*, *A* must be lower triangular. The operator performs the BLAS3 function*trmm*:   *out* = *alpha* \* *op*\ (*A*) \* *B*if *rightside=False*, or   *out* = *alpha* \* *B* \* *op*\ (*A*)if *rightside=True*. Here, *alpha* is a scalar parameter, and *op()* is either theidentity or the matrix transposition (depending on *transpose*).If *n>2*, *trmm* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single triangular matrix multiply   A = [[1.0, 0], [1.0, 1.0]]   B = [[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]   trmm(A, B, alpha=2.0) = [[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]]   // Batch triangular matrix multiply   A = [[[1.0, 0], [1.0, 1.0]], [[1.0, 0], [1.0, 1.0]]]   B = [[[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]], [[0.5, 0.5, 0.5], [0.5, 0.5, 0.5]]]   trmm(A, B, alpha=2.0) = [[[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]],                            [[1.0, 1.0, 1.0], [2.0, 2.0, 2.0]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L293
/// </summary>
/// <param name="A">Tensor of lower triangular matrices</param>
/// <param name="B">Tensor of matrices</param>
/// <param name="transpose">Use transposed of the triangular matrix</param>
/// <param name="rightside">Multiply triangular matrix from the right to non-triangular one.</param>
/// <param name="alpha">Scalar factor to be applied to the result.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgTrmm(NdArray A,
NdArray B,
bool transpose=false,
bool rightside=false,
double alpha=1)
{
return new Operator("_linalg_trmm")
.SetParam("transpose", transpose)
.SetParam("rightside", rightside)
.SetParam("alpha", alpha)
.SetInput("A", A)
.SetInput("B", B)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgTrmm(NdArray @out)
{
return new Operator("_backward_linalg_trmm")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgTrmm()
{
return new Operator("_backward_linalg_trmm")
.Invoke();
}
/// <summary>
/// Solves matrix equation involving a lower triangular matrix.Input are tensors *A*, *B*, each of dimension *n >= 2* and having the same shapeon the leading *n-2* dimensions.If *n=2*, *A* must be lower triangular. The operator performs the BLAS3 function*trsm*, solving for *out* in:   *op*\ (*A*) \* *out* = *alpha* \* *B*if *rightside=False*, or   *out* \* *op*\ (*A*) = *alpha* \* *B*if *rightside=True*. Here, *alpha* is a scalar parameter, and *op()* is either theidentity or the matrix transposition (depending on *transpose*).If *n>2*, *trsm* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix solve   A = [[1.0, 0], [1.0, 1.0]]   B = [[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]]   trsm(A, B, alpha=0.5) = [[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]   // Batch matrix solve   A = [[[1.0, 0], [1.0, 1.0]], [[1.0, 0], [1.0, 1.0]]]   B = [[[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]],        [[4.0, 4.0, 4.0], [8.0, 8.0, 8.0]]]   trsm(A, B, alpha=0.5) = [[[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]],                            [[2.0, 2.0, 2.0], [2.0, 2.0, 2.0]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L356
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of lower triangular matrices</param>
/// <param name="B">Tensor of matrices</param>
/// <param name="transpose">Use transposed of the triangular matrix</param>
/// <param name="rightside">Multiply triangular matrix from the right to non-triangular one.</param>
/// <param name="alpha">Scalar factor to be applied to the result.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgTrsm(NdArray @out,
NdArray A,
NdArray B,
bool transpose=false,
bool rightside=false,
double alpha=1)
{
return new Operator("_linalg_trsm")
.SetParam("transpose", transpose)
.SetParam("rightside", rightside)
.SetParam("alpha", alpha)
.SetInput("A", A)
.SetInput("B", B)
.Invoke(@out);
}
/// <summary>
/// Solves matrix equation involving a lower triangular matrix.Input are tensors *A*, *B*, each of dimension *n >= 2* and having the same shapeon the leading *n-2* dimensions.If *n=2*, *A* must be lower triangular. The operator performs the BLAS3 function*trsm*, solving for *out* in:   *op*\ (*A*) \* *out* = *alpha* \* *B*if *rightside=False*, or   *out* \* *op*\ (*A*) = *alpha* \* *B*if *rightside=True*. Here, *alpha* is a scalar parameter, and *op()* is either theidentity or the matrix transposition (depending on *transpose*).If *n>2*, *trsm* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix solve   A = [[1.0, 0], [1.0, 1.0]]   B = [[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]]   trsm(A, B, alpha=0.5) = [[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]   // Batch matrix solve   A = [[[1.0, 0], [1.0, 1.0]], [[1.0, 0], [1.0, 1.0]]]   B = [[[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]],        [[4.0, 4.0, 4.0], [8.0, 8.0, 8.0]]]   trsm(A, B, alpha=0.5) = [[[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]],                            [[2.0, 2.0, 2.0], [2.0, 2.0, 2.0]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L356
/// </summary>
/// <param name="A">Tensor of lower triangular matrices</param>
/// <param name="B">Tensor of matrices</param>
/// <param name="transpose">Use transposed of the triangular matrix</param>
/// <param name="rightside">Multiply triangular matrix from the right to non-triangular one.</param>
/// <param name="alpha">Scalar factor to be applied to the result.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgTrsm(NdArray A,
NdArray B,
bool transpose=false,
bool rightside=false,
double alpha=1)
{
return new Operator("_linalg_trsm")
.SetParam("transpose", transpose)
.SetParam("rightside", rightside)
.SetParam("alpha", alpha)
.SetInput("A", A)
.SetInput("B", B)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgTrsm(NdArray @out)
{
return new Operator("_backward_linalg_trsm")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgTrsm()
{
return new Operator("_backward_linalg_trsm")
.Invoke();
}
/// <summary>
/// Multiplication of matrix with its transpose.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, the operator performs the BLAS3 function *syrk*:  *out* = *alpha* \* *A* \* *A*\ :sup:`T`if *transpose=False*, or  *out* = *alpha* \* *A*\ :sup:`T` \ \* *A*if *transpose=True*.If *n>2*, *syrk* is performed separately on the trailing two dimensions for allinputs (batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix multiply   A = [[1., 2., 3.], [4., 5., 6.]]   syrk(A, alpha=1., transpose=False)            = [[14., 32.],               [32., 77.]]   syrk(A, alpha=1., transpose=True)            = [[17., 22., 27.],               [22., 29., 36.],               [27., 36., 45.]]   // Batch matrix multiply   A = [[[1., 1.]], [[0.1, 0.1]]]   syrk(A, alpha=2., transpose=False) = [[[4.]], [[0.04]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L461
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of input matrices</param>
/// <param name="transpose">Use transpose of input matrix.</param>
/// <param name="alpha">Scalar factor to be applied to the result.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgSyrk(NdArray @out,
NdArray A,
bool transpose=false,
double alpha=1)
{
return new Operator("_linalg_syrk")
.SetParam("transpose", transpose)
.SetParam("alpha", alpha)
.SetInput("A", A)
.Invoke(@out);
}
/// <summary>
/// Multiplication of matrix with its transpose.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, the operator performs the BLAS3 function *syrk*:  *out* = *alpha* \* *A* \* *A*\ :sup:`T`if *transpose=False*, or  *out* = *alpha* \* *A*\ :sup:`T` \ \* *A*if *transpose=True*.If *n>2*, *syrk* is performed separately on the trailing two dimensions for allinputs (batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix multiply   A = [[1., 2., 3.], [4., 5., 6.]]   syrk(A, alpha=1., transpose=False)            = [[14., 32.],               [32., 77.]]   syrk(A, alpha=1., transpose=True)            = [[17., 22., 27.],               [22., 29., 36.],               [27., 36., 45.]]   // Batch matrix multiply   A = [[[1., 1.]], [[0.1, 0.1]]]   syrk(A, alpha=2., transpose=False) = [[[4.]], [[0.04]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L461
/// </summary>
/// <param name="A">Tensor of input matrices</param>
/// <param name="transpose">Use transpose of input matrix.</param>
/// <param name="alpha">Scalar factor to be applied to the result.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgSyrk(NdArray A,
bool transpose=false,
double alpha=1)
{
return new Operator("_linalg_syrk")
.SetParam("transpose", transpose)
.SetParam("alpha", alpha)
.SetInput("A", A)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgSyrk(NdArray @out)
{
return new Operator("_backward_linalg_syrk")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgSyrk()
{
return new Operator("_backward_linalg_syrk")
.Invoke();
}
/// <summary>
/// Computes the sum of the logarithms of the diagonal elements of a square matrix.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, *A* must be square with positive diagonal entries. We sum the naturallogarithms of the diagonal elements, the result has shape (1,).If *n>2*, *sumlogdiag* is performed separately on the trailing two dimensions for allinputs (batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix reduction   A = [[1.0, 1.0], [1.0, 7.0]]   sumlogdiag(A) = [1.9459]   // Batch matrix reduction   A = [[[1.0, 1.0], [1.0, 7.0]], [[3.0, 0], [0, 17.0]]]   sumlogdiag(A) = [1.9459, 3.9318]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L405
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of square matrices</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgSumlogdiag(NdArray @out,
NdArray A)
{
return new Operator("_linalg_sumlogdiag")
.SetInput("A", A)
.Invoke(@out);
}
/// <summary>
/// Computes the sum of the logarithms of the diagonal elements of a square matrix.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, *A* must be square with positive diagonal entries. We sum the naturallogarithms of the diagonal elements, the result has shape (1,).If *n>2*, *sumlogdiag* is performed separately on the trailing two dimensions for allinputs (batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix reduction   A = [[1.0, 1.0], [1.0, 7.0]]   sumlogdiag(A) = [1.9459]   // Batch matrix reduction   A = [[[1.0, 1.0], [1.0, 7.0]], [[3.0, 0], [0, 17.0]]]   sumlogdiag(A) = [1.9459, 3.9318]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L405
/// </summary>
/// <param name="A">Tensor of square matrices</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgSumlogdiag(NdArray A)
{
return new Operator("_linalg_sumlogdiag")
.SetInput("A", A)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgSumlogdiag(NdArray @out)
{
return new Operator("_backward_linalg_sumlogdiag")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgSumlogdiag()
{
return new Operator("_backward_linalg_sumlogdiag")
.Invoke();
}
/// <summary>
/// Performs matrix inversion from a Cholesky factorization.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, *A* is a lower triangular matrix (entries of upper triangle are all zero)with positive diagonal. We compute:  *out* = *A*\ :sup:`-T` \* *A*\ :sup:`-1`In other words, if *A* is the Cholesky factor of a symmetric positive definite matrix*B* (obtained by *potrf*), then  *out* = *B*\ :sup:`-1`If *n>2*, *potri* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only... note:: Use this operator only if you are certain you need the inverse of *B*, and          cannot use the Cholesky factor *A* (*potrf*), together with backsubstitution          (*trsm*). The latter is numerically much safer, and also cheaper.Examples::   // Single matrix inverse   A = [[2.0, 0], [0.5, 2.0]]   potri(A) = [[0.26563, -0.0625], [-0.0625, 0.25]]   // Batch matrix inverse   A = [[[2.0, 0], [0.5, 2.0]], [[4.0, 0], [1.0, 4.0]]]   potri(A) = [[[0.26563, -0.0625], [-0.0625, 0.25]],               [[0.06641, -0.01562], [-0.01562, 0,0625]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L236
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of lower triangular matrices</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgPotri(NdArray @out,
NdArray A)
{
return new Operator("_linalg_potri")
.SetInput("A", A)
.Invoke(@out);
}
/// <summary>
/// Performs matrix inversion from a Cholesky factorization.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, *A* is a lower triangular matrix (entries of upper triangle are all zero)with positive diagonal. We compute:  *out* = *A*\ :sup:`-T` \* *A*\ :sup:`-1`In other words, if *A* is the Cholesky factor of a symmetric positive definite matrix*B* (obtained by *potrf*), then  *out* = *B*\ :sup:`-1`If *n>2*, *potri* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only... note:: Use this operator only if you are certain you need the inverse of *B*, and          cannot use the Cholesky factor *A* (*potrf*), together with backsubstitution          (*trsm*). The latter is numerically much safer, and also cheaper.Examples::   // Single matrix inverse   A = [[2.0, 0], [0.5, 2.0]]   potri(A) = [[0.26563, -0.0625], [-0.0625, 0.25]]   // Batch matrix inverse   A = [[[2.0, 0], [0.5, 2.0]], [[4.0, 0], [1.0, 4.0]]]   potri(A) = [[[0.26563, -0.0625], [-0.0625, 0.25]],               [[0.06641, -0.01562], [-0.01562, 0,0625]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L236
/// </summary>
/// <param name="A">Tensor of lower triangular matrices</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgPotri(NdArray A)
{
return new Operator("_linalg_potri")
.SetInput("A", A)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgPotri(NdArray @out)
{
return new Operator("_backward_linalg_potri")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgPotri()
{
return new Operator("_backward_linalg_potri")
.Invoke();
}
/// <summary>
/// Performs Cholesky factorization of a symmetric positive-definite matrix.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, the Cholesky factor *L* of the symmetric, positive definite matrix *A* iscomputed. *L* is lower triangular (entries of upper triangle are all zero), haspositive diagonal entries, and:  *A* = *L* \* *L*\ :sup:`T`If *n>2*, *potrf* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix factorization   A = [[4.0, 1.0], [1.0, 4.25]]   potrf(A) = [[2.0, 0], [0.5, 2.0]]   // Batch matrix factorization   A = [[[4.0, 1.0], [1.0, 4.25]], [[16.0, 4.0], [4.0, 17.0]]]   potrf(A) = [[[2.0, 0], [0.5, 2.0]], [[4.0, 0], [1.0, 4.0]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L178
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of input matrices to be decomposed</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgPotrf(NdArray @out,
NdArray A)
{
return new Operator("_linalg_potrf")
.SetInput("A", A)
.Invoke(@out);
}
/// <summary>
/// Performs Cholesky factorization of a symmetric positive-definite matrix.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, the Cholesky factor *L* of the symmetric, positive definite matrix *A* iscomputed. *L* is lower triangular (entries of upper triangle are all zero), haspositive diagonal entries, and:  *A* = *L* \* *L*\ :sup:`T`If *n>2*, *potrf* is performed separately on the trailing two dimensions for all inputs(batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single matrix factorization   A = [[4.0, 1.0], [1.0, 4.25]]   potrf(A) = [[2.0, 0], [0.5, 2.0]]   // Batch matrix factorization   A = [[[4.0, 1.0], [1.0, 4.25]], [[16.0, 4.0], [4.0, 17.0]]]   potrf(A) = [[[2.0, 0], [0.5, 2.0]], [[4.0, 0], [1.0, 4.0]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L178
/// </summary>
/// <param name="A">Tensor of input matrices to be decomposed</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgPotrf(NdArray A)
{
return new Operator("_linalg_potrf")
.SetInput("A", A)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgPotrf(NdArray @out)
{
return new Operator("_backward_linalg_potrf")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgPotrf()
{
return new Operator("_backward_linalg_potrf")
.Invoke();
}
/// <summary>
/// LQ factorization for general matrix.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, we compute the LQ factorization (LAPACK *gelqf*, followed by *orglq*). *A*must have shape *(x, y)* with *x <= y*, and must have full rank *=x*. The LQfactorization consists of *L* with shape *(x, x)* and *Q* with shape *(x, y)*, sothat:   *A* = *L* \* *Q*Here, *L* is lower triangular (upper triangle equal to zero) with nonzero diagonal,and *Q* is row-orthonormal, meaning that   *Q* \* *Q*\ :sup:`T`is equal to the identity matrix of shape *(x, x)*.If *n>2*, *gelqf* is performed separately on the trailing two dimensions for allinputs (batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single LQ factorization   A = [[1., 2., 3.], [4., 5., 6.]]   Q, L = gelqf(A)   Q = [[-0.26726124, -0.53452248, -0.80178373],        [0.87287156, 0.21821789, -0.43643578]]   L = [[-3.74165739, 0.],        [-8.55235974, 1.96396101]]   // Batch LQ factorization   A = [[[1., 2., 3.], [4., 5., 6.]],        [[7., 8., 9.], [10., 11., 12.]]]   Q, L = gelqf(A)   Q = [[[-0.26726124, -0.53452248, -0.80178373],         [0.87287156, 0.21821789, -0.43643578]],        [[-0.50257071, -0.57436653, -0.64616234],         [0.7620735, 0.05862104, -0.64483142]]]   L = [[[-3.74165739, 0.],         [-8.55235974, 1.96396101]],        [[-13.92838828, 0.],         [-19.09768702, 0.52758934]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L529
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of input matrices to be factorized</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgGelqf(NdArray @out,
NdArray A)
{
return new Operator("_linalg_gelqf")
.SetInput("A", A)
.Invoke(@out);
}
/// <summary>
/// LQ factorization for general matrix.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, we compute the LQ factorization (LAPACK *gelqf*, followed by *orglq*). *A*must have shape *(x, y)* with *x <= y*, and must have full rank *=x*. The LQfactorization consists of *L* with shape *(x, x)* and *Q* with shape *(x, y)*, sothat:   *A* = *L* \* *Q*Here, *L* is lower triangular (upper triangle equal to zero) with nonzero diagonal,and *Q* is row-orthonormal, meaning that   *Q* \* *Q*\ :sup:`T`is equal to the identity matrix of shape *(x, x)*.If *n>2*, *gelqf* is performed separately on the trailing two dimensions for allinputs (batch mode)... note:: The operator supports float32 and float64 data types only.Examples::   // Single LQ factorization   A = [[1., 2., 3.], [4., 5., 6.]]   Q, L = gelqf(A)   Q = [[-0.26726124, -0.53452248, -0.80178373],        [0.87287156, 0.21821789, -0.43643578]]   L = [[-3.74165739, 0.],        [-8.55235974, 1.96396101]]   // Batch LQ factorization   A = [[[1., 2., 3.], [4., 5., 6.]],        [[7., 8., 9.], [10., 11., 12.]]]   Q, L = gelqf(A)   Q = [[[-0.26726124, -0.53452248, -0.80178373],         [0.87287156, 0.21821789, -0.43643578]],        [[-0.50257071, -0.57436653, -0.64616234],         [0.7620735, 0.05862104, -0.64483142]]]   L = [[[-3.74165739, 0.],         [-8.55235974, 1.96396101]],        [[-13.92838828, 0.],         [-19.09768702, 0.52758934]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L529
/// </summary>
/// <param name="A">Tensor of input matrices to be factorized</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgGelqf(NdArray A)
{
return new Operator("_linalg_gelqf")
.SetInput("A", A)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgGelqf(NdArray @out)
{
return new Operator("_backward_linalg_gelqf")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgGelqf()
{
return new Operator("_backward_linalg_gelqf")
.Invoke();
}
/// <summary>
/// Eigendecomposition for symmetric matrix.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, *A* must be symmetric, of shape *(x, x)*. We compute the eigendecomposition,resulting in the orthonormal matrix *U* of eigenvectors, shape *(x, x)*, and thevector *L* of eigenvalues, shape *(x,)*, so that:   *U* \* *A* = *diag(L)* \* *U*Here:   *U* \* *U*\ :sup:`T` = *U*\ :sup:`T` \* *U* = *I*where *I* is the identity matrix. Also, *L(0) <= L(1) <= L(2) <= ...* (ascending order).If *n>2*, *syevd* is performed separately on the trailing two dimensions of *A* (batchmode). In this case, *U* has *n* dimensions like *A*, and *L* has *n-1* dimensions... note:: The operator supports float32 and float64 data types only... note:: Derivatives for this operator are defined only if *A* is such that all its          eigenvalues are distinct, and the eigengaps are not too small. If you need          gradients, do not apply this operator to matrices with multiple eigenvalues.Examples::   // Single symmetric eigendecomposition   A = [[1., 2.], [2., 4.]]   U, L = syevd(A)   U = [[0.89442719, -0.4472136],        [0.4472136, 0.89442719]]   L = [0., 5.]   // Batch symmetric eigendecomposition   A = [[[1., 2.], [2., 4.]],        [[1., 2.], [2., 5.]]]   U, L = syevd(A)   U = [[[0.89442719, -0.4472136],         [0.4472136, 0.89442719]],        [[0.92387953, -0.38268343],         [0.38268343, 0.92387953]]]   L = [[0., 5.],        [0.17157288, 5.82842712]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L598
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="A">Tensor of input matrices to be factorized</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgSyevd(NdArray @out,
NdArray A)
{
return new Operator("_linalg_syevd")
.SetInput("A", A)
.Invoke(@out);
}
/// <summary>
/// Eigendecomposition for symmetric matrix.Input is a tensor *A* of dimension *n >= 2*.If *n=2*, *A* must be symmetric, of shape *(x, x)*. We compute the eigendecomposition,resulting in the orthonormal matrix *U* of eigenvectors, shape *(x, x)*, and thevector *L* of eigenvalues, shape *(x,)*, so that:   *U* \* *A* = *diag(L)* \* *U*Here:   *U* \* *U*\ :sup:`T` = *U*\ :sup:`T` \* *U* = *I*where *I* is the identity matrix. Also, *L(0) <= L(1) <= L(2) <= ...* (ascending order).If *n>2*, *syevd* is performed separately on the trailing two dimensions of *A* (batchmode). In this case, *U* has *n* dimensions like *A*, and *L* has *n-1* dimensions... note:: The operator supports float32 and float64 data types only... note:: Derivatives for this operator are defined only if *A* is such that all its          eigenvalues are distinct, and the eigengaps are not too small. If you need          gradients, do not apply this operator to matrices with multiple eigenvalues.Examples::   // Single symmetric eigendecomposition   A = [[1., 2.], [2., 4.]]   U, L = syevd(A)   U = [[0.89442719, -0.4472136],        [0.4472136, 0.89442719]]   L = [0., 5.]   // Batch symmetric eigendecomposition   A = [[[1., 2.], [2., 4.]],        [[1., 2.], [2., 5.]]]   U, L = syevd(A)   U = [[[0.89442719, -0.4472136],         [0.4472136, 0.89442719]],        [[0.92387953, -0.38268343],         [0.38268343, 0.92387953]]]   L = [[0., 5.],        [0.17157288, 5.82842712]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\la_op.cc:L598
/// </summary>
/// <param name="A">Tensor of input matrices to be factorized</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinalgSyevd(NdArray A)
{
return new Operator("_linalg_syevd")
.SetInput("A", A)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgSyevd(NdArray @out)
{
return new Operator("_backward_linalg_syevd")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinalgSyevd()
{
return new Operator("_backward_linalg_syevd")
.Invoke();
}
/// <summary>
/// Slices a region of the array... note:: ``crop`` is deprecated. Use ``slice`` instead.This function returns a sliced array between the indices givenby `begin` and `end` with the corresponding `step`.For an input array of ``shape=(d_0, d_1, ..., d_n-1)``,slice operation with ``begin=(b_0, b_1...b_m-1)``,``end=(e_0, e_1, ..., e_m-1)``, and ``step=(s_0, s_1, ..., s_m-1)``,where m <= n, results in an array with the shape``(|e_0-b_0|/|s_0|, ..., |e_m-1-b_m-1|/|s_m-1|, d_m, ..., d_n-1)``.The resulting array's *k*-th dimension contains elementsfrom the *k*-th dimension of the input array startingfrom index ``b_k`` (inclusive) with step ``s_k``until reaching ``e_k`` (exclusive).If the *k*-th elements are `None` in the sequence of `begin`, `end`,and `step`, the following rule will be used to set default values.If `s_k` is `None`, set `s_k=1`. If `s_k > 0`, set `b_k=0`, `e_k=d_k`;else, set `b_k=d_k-1`, `e_k=-1`.The storage type of ``slice`` output depends on storage types of inputs- slice(csr) = csr- otherwise, ``slice`` generates output with default storage.. note:: When input data storage type is csr, it only supportsstep=(), or step=(None,), or step=(1,) to generate a csr output.For other step parameter values, it falls back to slicinga dense tensor.Example::  x = [[  1.,   2.,   3.,   4.],       [  5.,   6.,   7.,   8.],       [  9.,  10.,  11.,  12.]]  slice(x, begin=(0,1), end=(2,4)) = [[ 2.,  3.,  4.],                                     [ 6.,  7.,  8.]]  slice(x, begin=(None, 0), end=(None, 3), step=(-1, 2)) = [[9., 11.],                                                            [5.,  7.],                                                            [1.,  3.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L355
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Source input</param>
/// <param name="begin">starting indices for the slice operation, supports negative indices.</param>
/// <param name="end">ending indices for the slice operation, supports negative indices.</param>
/// <param name="step">step for the slice operation, supports negative values.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Slice(NdArray @out,
NdArray data,
Shape begin,
Shape end,
Shape step=null)
{
return new Operator("slice")
.SetParam("begin", begin)
.SetParam("end", end)
.SetParam("step", step)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Slices a region of the array... note:: ``crop`` is deprecated. Use ``slice`` instead.This function returns a sliced array between the indices givenby `begin` and `end` with the corresponding `step`.For an input array of ``shape=(d_0, d_1, ..., d_n-1)``,slice operation with ``begin=(b_0, b_1...b_m-1)``,``end=(e_0, e_1, ..., e_m-1)``, and ``step=(s_0, s_1, ..., s_m-1)``,where m <= n, results in an array with the shape``(|e_0-b_0|/|s_0|, ..., |e_m-1-b_m-1|/|s_m-1|, d_m, ..., d_n-1)``.The resulting array's *k*-th dimension contains elementsfrom the *k*-th dimension of the input array startingfrom index ``b_k`` (inclusive) with step ``s_k``until reaching ``e_k`` (exclusive).If the *k*-th elements are `None` in the sequence of `begin`, `end`,and `step`, the following rule will be used to set default values.If `s_k` is `None`, set `s_k=1`. If `s_k > 0`, set `b_k=0`, `e_k=d_k`;else, set `b_k=d_k-1`, `e_k=-1`.The storage type of ``slice`` output depends on storage types of inputs- slice(csr) = csr- otherwise, ``slice`` generates output with default storage.. note:: When input data storage type is csr, it only supportsstep=(), or step=(None,), or step=(1,) to generate a csr output.For other step parameter values, it falls back to slicinga dense tensor.Example::  x = [[  1.,   2.,   3.,   4.],       [  5.,   6.,   7.,   8.],       [  9.,  10.,  11.,  12.]]  slice(x, begin=(0,1), end=(2,4)) = [[ 2.,  3.,  4.],                                     [ 6.,  7.,  8.]]  slice(x, begin=(None, 0), end=(None, 3), step=(-1, 2)) = [[9., 11.],                                                            [5.,  7.],                                                            [1.,  3.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L355
/// </summary>
/// <param name="data">Source input</param>
/// <param name="begin">starting indices for the slice operation, supports negative indices.</param>
/// <param name="end">ending indices for the slice operation, supports negative indices.</param>
/// <param name="step">step for the slice operation, supports negative values.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Slice(NdArray data,
Shape begin,
Shape end,
Shape step=null)
{
return new Operator("slice")
.SetParam("begin", begin)
.SetParam("end", end)
.SetParam("step", step)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSlice(NdArray @out)
{
return new Operator("_backward_slice")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSlice()
{
return new Operator("_backward_slice")
.Invoke();
}
/// <summary>
/// Assign the rhs to a cropped subset of lhs.Requirements------------- output should be explicitly given and be the same as lhs.- lhs and rhs are of the same data type, and on the same device.From:I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:381
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">Source input</param>
/// <param name="rhs">value to assign</param>
/// <param name="begin">starting indices for the slice operation, supports negative indices.</param>
/// <param name="end">ending indices for the slice operation, supports negative indices.</param>
/// <param name="step">step for the slice operation, supports negative values.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SliceAssign(NdArray @out,
NdArray lhs,
NdArray rhs,
Shape begin,
Shape end,
Shape step=null)
{
return new Operator("_slice_assign")
.SetParam("begin", begin)
.SetParam("end", end)
.SetParam("step", step)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Assign the rhs to a cropped subset of lhs.Requirements------------- output should be explicitly given and be the same as lhs.- lhs and rhs are of the same data type, and on the same device.From:I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:381
/// </summary>
/// <param name="lhs">Source input</param>
/// <param name="rhs">value to assign</param>
/// <param name="begin">starting indices for the slice operation, supports negative indices.</param>
/// <param name="end">ending indices for the slice operation, supports negative indices.</param>
/// <param name="step">step for the slice operation, supports negative values.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SliceAssign(NdArray lhs,
NdArray rhs,
Shape begin,
Shape end,
Shape step=null)
{
return new Operator("_slice_assign")
.SetParam("begin", begin)
.SetParam("end", end)
.SetParam("step", step)
.SetInput("lhs", lhs)
.SetInput("rhs", rhs)
.Invoke();
}
/// <summary>
/// (Assign the scalar to a cropped subset of the input.Requirements------------- output should be explicitly given and be the same as input)From:I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:406
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Source input</param>
/// <param name="begin">starting indices for the slice operation, supports negative indices.</param>
/// <param name="end">ending indices for the slice operation, supports negative indices.</param>
/// <param name="scalar">The scalar value for assignment.</param>
/// <param name="step">step for the slice operation, supports negative values.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SliceAssignScalar(NdArray @out,
NdArray data,
Shape begin,
Shape end,
float scalar=0f,
Shape step=null)
{
return new Operator("_slice_assign_scalar")
.SetParam("scalar", scalar)
.SetParam("begin", begin)
.SetParam("end", end)
.SetParam("step", step)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// (Assign the scalar to a cropped subset of the input.Requirements------------- output should be explicitly given and be the same as input)From:I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:406
/// </summary>
/// <param name="data">Source input</param>
/// <param name="begin">starting indices for the slice operation, supports negative indices.</param>
/// <param name="end">ending indices for the slice operation, supports negative indices.</param>
/// <param name="scalar">The scalar value for assignment.</param>
/// <param name="step">step for the slice operation, supports negative values.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SliceAssignScalar(NdArray data,
Shape begin,
Shape end,
float scalar=0f,
Shape step=null)
{
return new Operator("_slice_assign_scalar")
.SetParam("scalar", scalar)
.SetParam("begin", begin)
.SetParam("end", end)
.SetParam("step", step)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Slices along a given axis.Returns an array slice along a given `axis` starting from the `begin` indexto the `end` index.Examples::  x = [[  1.,   2.,   3.,   4.],       [  5.,   6.,   7.,   8.],       [  9.,  10.,  11.,  12.]]  slice_axis(x, axis=0, begin=1, end=3) = [[  5.,   6.,   7.,   8.],                                           [  9.,  10.,  11.,  12.]]  slice_axis(x, axis=1, begin=0, end=2) = [[  1.,   2.],                                           [  5.,   6.],                                           [  9.,  10.]]  slice_axis(x, axis=1, begin=-3, end=-1) = [[  2.,   3.],                                             [  6.,   7.],                                             [ 10.,  11.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L442
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Source input</param>
/// <param name="axis">Axis along which to be sliced, supports negative indexes.</param>
/// <param name="begin">The beginning index along the axis to be sliced,  supports negative indexes.</param>
/// <param name="end">The ending index along the axis to be sliced,  supports negative indexes.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SliceAxis(NdArray @out,
NdArray data,
int axis,
int begin,
int? end)
{
return new Operator("slice_axis")
.SetParam("axis", axis)
.SetParam("begin", begin)
.SetParam("end", end)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Slices along a given axis.Returns an array slice along a given `axis` starting from the `begin` indexto the `end` index.Examples::  x = [[  1.,   2.,   3.,   4.],       [  5.,   6.,   7.,   8.],       [  9.,  10.,  11.,  12.]]  slice_axis(x, axis=0, begin=1, end=3) = [[  5.,   6.,   7.,   8.],                                           [  9.,  10.,  11.,  12.]]  slice_axis(x, axis=1, begin=0, end=2) = [[  1.,   2.],                                           [  5.,   6.],                                           [  9.,  10.]]  slice_axis(x, axis=1, begin=-3, end=-1) = [[  2.,   3.],                                             [  6.,   7.],                                             [ 10.,  11.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L442
/// </summary>
/// <param name="data">Source input</param>
/// <param name="axis">Axis along which to be sliced, supports negative indexes.</param>
/// <param name="begin">The beginning index along the axis to be sliced,  supports negative indexes.</param>
/// <param name="end">The ending index along the axis to be sliced,  supports negative indexes.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SliceAxis(NdArray data,
int axis,
int begin,
int? end)
{
return new Operator("slice_axis")
.SetParam("axis", axis)
.SetParam("begin", begin)
.SetParam("end", end)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSliceAxis(NdArray @out)
{
return new Operator("_backward_slice_axis")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSliceAxis()
{
return new Operator("_backward_slice_axis")
.Invoke();
}
/// <summary>
/// Clips (limits) the values in an array.Given an interval, values outside the interval are clipped to the interval edges.Clipping ``x`` between `a_min` and `a_x` would be::   clip(x, a_min, a_max) = max(min(x, a_max), a_min))Example::    x = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]    clip(x,1,8) = [ 1.,  1.,  2.,  3.,  4.,  5.,  6.,  7.,  8.,  8.]The storage type of ``clip`` output depends on storage types of inputs and the a_min, a_max \parameter values:   - clip(default) = default   - clip(row_sparse, a_min <= 0, a_max >= 0) = row_sparse   - clip(csr, a_min <= 0, a_max >= 0) = csr   - clip(row_sparse, a_min < 0, a_max < 0) = default   - clip(row_sparse, a_min > 0, a_max > 0) = default   - clip(csr, a_min < 0, a_max < 0) = csr   - clip(csr, a_min > 0, a_max > 0) = csrDefined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L486
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array.</param>
/// <param name="a_min">Minimum value</param>
/// <param name="a_max">Maximum value</param>
 /// <returns>returns new symbol</returns>
public static NdArray Clip(NdArray @out,
NdArray data,
float a_min,
float a_max)
{
return new Operator("clip")
.SetParam("a_min", a_min)
.SetParam("a_max", a_max)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Clips (limits) the values in an array.Given an interval, values outside the interval are clipped to the interval edges.Clipping ``x`` between `a_min` and `a_x` would be::   clip(x, a_min, a_max) = max(min(x, a_max), a_min))Example::    x = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]    clip(x,1,8) = [ 1.,  1.,  2.,  3.,  4.,  5.,  6.,  7.,  8.,  8.]The storage type of ``clip`` output depends on storage types of inputs and the a_min, a_max \parameter values:   - clip(default) = default   - clip(row_sparse, a_min <= 0, a_max >= 0) = row_sparse   - clip(csr, a_min <= 0, a_max >= 0) = csr   - clip(row_sparse, a_min < 0, a_max < 0) = default   - clip(row_sparse, a_min > 0, a_max > 0) = default   - clip(csr, a_min < 0, a_max < 0) = csr   - clip(csr, a_min > 0, a_max > 0) = csrDefined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L486
/// </summary>
/// <param name="data">Input array.</param>
/// <param name="a_min">Minimum value</param>
/// <param name="a_max">Maximum value</param>
 /// <returns>returns new symbol</returns>
public static NdArray Clip(NdArray data,
float a_min,
float a_max)
{
return new Operator("clip")
.SetParam("a_min", a_min)
.SetParam("a_max", a_max)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardClip(NdArray @out)
{
return new Operator("_backward_clip")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardClip()
{
return new Operator("_backward_clip")
.Invoke();
}
/// <summary>
/// Repeats elements of an array.By default, ``repeat`` flattens the input array into 1-D and then repeats theelements::  x = [[ 1, 2],       [ 3, 4]]  repeat(x, repeats=2) = [ 1.,  1.,  2.,  2.,  3.,  3.,  4.,  4.]The parameter ``axis`` specifies the axis along which to perform repeat::  repeat(x, repeats=2, axis=1) = [[ 1.,  1.,  2.,  2.],                                  [ 3.,  3.,  4.,  4.]]  repeat(x, repeats=2, axis=0) = [[ 1.,  2.],                                  [ 1.,  2.],                                  [ 3.,  4.],                                  [ 3.,  4.]]  repeat(x, repeats=2, axis=-1) = [[ 1.,  1.,  2.,  2.],                                   [ 3.,  3.,  4.,  4.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L560
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data array</param>
/// <param name="repeats">The number of repetitions for each element.</param>
/// <param name="axis">The axis along which to repeat values. The negative numbers are interpreted counting from the backward. By default, use the flattened input array, and return a flat output array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Repeat(NdArray @out,
NdArray data,
int repeats,
int? axis=null)
{
return new Operator("repeat")
.SetParam("repeats", repeats)
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Repeats elements of an array.By default, ``repeat`` flattens the input array into 1-D and then repeats theelements::  x = [[ 1, 2],       [ 3, 4]]  repeat(x, repeats=2) = [ 1.,  1.,  2.,  2.,  3.,  3.,  4.,  4.]The parameter ``axis`` specifies the axis along which to perform repeat::  repeat(x, repeats=2, axis=1) = [[ 1.,  1.,  2.,  2.],                                  [ 3.,  3.,  4.,  4.]]  repeat(x, repeats=2, axis=0) = [[ 1.,  2.],                                  [ 1.,  2.],                                  [ 3.,  4.],                                  [ 3.,  4.]]  repeat(x, repeats=2, axis=-1) = [[ 1.,  1.,  2.,  2.],                                   [ 3.,  3.,  4.,  4.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L560
/// </summary>
/// <param name="data">Input data array</param>
/// <param name="repeats">The number of repetitions for each element.</param>
/// <param name="axis">The axis along which to repeat values. The negative numbers are interpreted counting from the backward. By default, use the flattened input array, and return a flat output array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Repeat(NdArray data,
int repeats,
int? axis=null)
{
return new Operator("repeat")
.SetParam("repeats", repeats)
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRepeat(NdArray @out)
{
return new Operator("_backward_repeat")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRepeat()
{
return new Operator("_backward_repeat")
.Invoke();
}
/// <summary>
/// Repeats the whole array multiple times.If ``reps`` has length *d*, and input array has dimension of *n*. There arethree cases:- **n=d**. Repeat *i*-th dimension of the input by ``reps[i]`` times::    x = [[1, 2],         [3, 4]]    tile(x, reps=(2,3)) = [[ 1.,  2.,  1.,  2.,  1.,  2.],                           [ 3.,  4.,  3.,  4.,  3.,  4.],                           [ 1.,  2.,  1.,  2.,  1.,  2.],                           [ 3.,  4.,  3.,  4.,  3.,  4.]]- **n>d**. ``reps`` is promoted to length *n* by pre-pending 1's to it. Thus for  an input shape ``(2,3)``, ``repos=(2,)`` is treated as ``(1,2)``::    tile(x, reps=(2,)) = [[ 1.,  2.,  1.,  2.],                          [ 3.,  4.,  3.,  4.]]- **n<d**. The input is promoted to be d-dimensional by prepending new axes. So a  shape ``(2,2)`` array is promoted to ``(1,2,2)`` for 3-D replication::    tile(x, reps=(2,2,3)) = [[[ 1.,  2.,  1.,  2.,  1.,  2.],                              [ 3.,  4.,  3.,  4.,  3.,  4.],                              [ 1.,  2.,  1.,  2.,  1.,  2.],                              [ 3.,  4.,  3.,  4.,  3.,  4.]],                             [[ 1.,  2.,  1.,  2.,  1.,  2.],                              [ 3.,  4.,  3.,  4.,  3.,  4.],                              [ 1.,  2.,  1.,  2.,  1.,  2.],                              [ 3.,  4.,  3.,  4.,  3.,  4.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L621
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data array</param>
/// <param name="reps">The number of times for repeating the tensor a. If reps has length d, the result will have dimension of max(d, a.ndim); If a.ndim < d, a is promoted to be d-dimensional by prepending new axes. If a.ndim > d, reps is promoted to a.ndim by pre-pending 1's to it.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Tile(NdArray @out,
NdArray data,
Shape reps)
{
return new Operator("tile")
.SetParam("reps", reps)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Repeats the whole array multiple times.If ``reps`` has length *d*, and input array has dimension of *n*. There arethree cases:- **n=d**. Repeat *i*-th dimension of the input by ``reps[i]`` times::    x = [[1, 2],         [3, 4]]    tile(x, reps=(2,3)) = [[ 1.,  2.,  1.,  2.,  1.,  2.],                           [ 3.,  4.,  3.,  4.,  3.,  4.],                           [ 1.,  2.,  1.,  2.,  1.,  2.],                           [ 3.,  4.,  3.,  4.,  3.,  4.]]- **n>d**. ``reps`` is promoted to length *n* by pre-pending 1's to it. Thus for  an input shape ``(2,3)``, ``repos=(2,)`` is treated as ``(1,2)``::    tile(x, reps=(2,)) = [[ 1.,  2.,  1.,  2.],                          [ 3.,  4.,  3.,  4.]]- **n<d**. The input is promoted to be d-dimensional by prepending new axes. So a  shape ``(2,2)`` array is promoted to ``(1,2,2)`` for 3-D replication::    tile(x, reps=(2,2,3)) = [[[ 1.,  2.,  1.,  2.,  1.,  2.],                              [ 3.,  4.,  3.,  4.,  3.,  4.],                              [ 1.,  2.,  1.,  2.,  1.,  2.],                              [ 3.,  4.,  3.,  4.,  3.,  4.]],                             [[ 1.,  2.,  1.,  2.,  1.,  2.],                              [ 3.,  4.,  3.,  4.,  3.,  4.],                              [ 1.,  2.,  1.,  2.,  1.,  2.],                              [ 3.,  4.,  3.,  4.,  3.,  4.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L621
/// </summary>
/// <param name="data">Input data array</param>
/// <param name="reps">The number of times for repeating the tensor a. If reps has length d, the result will have dimension of max(d, a.ndim); If a.ndim < d, a is promoted to be d-dimensional by prepending new axes. If a.ndim > d, reps is promoted to a.ndim by pre-pending 1's to it.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Tile(NdArray data,
Shape reps)
{
return new Operator("tile")
.SetParam("reps", reps)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTile(NdArray @out)
{
return new Operator("_backward_tile")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTile()
{
return new Operator("_backward_tile")
.Invoke();
}
/// <summary>
/// Reverses the order of elements along given axis while preserving array shape.Note: reverse and flip are equivalent. We use reverse in the following examples.Examples::  x = [[ 0.,  1.,  2.,  3.,  4.],       [ 5.,  6.,  7.,  8.,  9.]]  reverse(x, axis=0) = [[ 5.,  6.,  7.,  8.,  9.],                        [ 0.,  1.,  2.,  3.,  4.]]  reverse(x, axis=1) = [[ 4.,  3.,  2.,  1.,  0.],                        [ 9.,  8.,  7.,  6.,  5.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L662
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data array</param>
/// <param name="axis">The axis which to reverse elements.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Reverse(NdArray @out,
NdArray data,
Shape axis)
{
return new Operator("reverse")
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Reverses the order of elements along given axis while preserving array shape.Note: reverse and flip are equivalent. We use reverse in the following examples.Examples::  x = [[ 0.,  1.,  2.,  3.,  4.],       [ 5.,  6.,  7.,  8.,  9.]]  reverse(x, axis=0) = [[ 5.,  6.,  7.,  8.,  9.],                        [ 0.,  1.,  2.,  3.,  4.]]  reverse(x, axis=1) = [[ 4.,  3.,  2.,  1.,  0.],                        [ 9.,  8.,  7.,  6.,  5.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L662
/// </summary>
/// <param name="data">Input data array</param>
/// <param name="axis">The axis which to reverse elements.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Reverse(NdArray data,
Shape axis)
{
return new Operator("reverse")
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardReverse(NdArray @out)
{
return new Operator("_backward_reverse")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardReverse()
{
return new Operator("_backward_reverse")
.Invoke();
}
/// <summary>
/// Join a sequence of arrays along a new axis.The axis parameter specifies the index of the new axis in the dimensions of theresult. For example, if axis=0 it will be the first dimension and if axis=-1 itwill be the last dimension.Examples::  x = [1, 2]  y = [3, 4]  stack(x, y) = [[1, 2],                 [3, 4]]  stack(x, y, axis=1) = [[1, 3],                         [2, 4]]
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">List of arrays to stack</param>
/// <param name="num_args">Number of inputs to be stacked.</param>
/// <param name="axis">The axis in the result array along which the input arrays are stacked.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Stack(NdArray @out,
NdArray[] data,
int num_args,
int axis=0)
{
return new Operator("stack")
.SetParam("axis", axis)
.SetParam("num_args", num_args)
.AddInput(data)
.Invoke(@out);
}
/// <summary>
/// Join a sequence of arrays along a new axis.The axis parameter specifies the index of the new axis in the dimensions of theresult. For example, if axis=0 it will be the first dimension and if axis=-1 itwill be the last dimension.Examples::  x = [1, 2]  y = [3, 4]  stack(x, y) = [[1, 2],                 [3, 4]]  stack(x, y, axis=1) = [[1, 3],                         [2, 4]]
/// </summary>
/// <param name="data">List of arrays to stack</param>
/// <param name="num_args">Number of inputs to be stacked.</param>
/// <param name="axis">The axis in the result array along which the input arrays are stacked.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Stack(NdArray[] data,
int num_args,
int axis=0)
{
return new Operator("stack")
.SetParam("axis", axis)
.SetParam("num_args", num_args)
.AddInput(data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardStack(NdArray @out)
{
return new Operator("_backward_stack")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardStack()
{
return new Operator("_backward_stack")
.Invoke();
}
/// <summary>
/// Reshapes the input array... note:: ``Reshape`` is deprecated, use ``reshape``Given an array and a shape, this function returns a copy of the array in the new shape.The shape is a tuple of integers such as (2,3,4).The size of the new shape should be same as the size of the input array.Example::  reshape([1,2,3,4], shape=(2,2)) = [[1,2], [3,4]]Some dimensions of the shape can take special values from the set {0, -1, -2, -3, -4}. The significance of each is explained below:- ``0``  copy this dimension from the input to the output shape.  Example::  - input shape = (2,3,4), shape = (4,0,2), output shape = (4,3,2)  - input shape = (2,3,4), shape = (2,0,0), output shape = (2,3,4)- ``-1`` infers the dimension of the output shape by using the remainder of the input dimensions  keeping the size of the new array same as that of the input array.  At most one dimension of shape can be -1.  Example::  - input shape = (2,3,4), shape = (6,1,-1), output shape = (6,1,4)  - input shape = (2,3,4), shape = (3,-1,8), output shape = (3,1,8)  - input shape = (2,3,4), shape=(-1,), output shape = (24,)- ``-2`` copy all/remainder of the input dimensions to the output shape.  Example::  - input shape = (2,3,4), shape = (-2,), output shape = (2,3,4)  - input shape = (2,3,4), shape = (2,-2), output shape = (2,3,4)  - input shape = (2,3,4), shape = (-2,1,1), output shape = (2,3,4,1,1)- ``-3`` use the product of two consecutive dimensions of the input shape as the output dimension.  Example::  - input shape = (2,3,4), shape = (-3,4), output shape = (6,4)  - input shape = (2,3,4,5), shape = (-3,-3), output shape = (6,20)  - input shape = (2,3,4), shape = (0,-3), output shape = (2,12)  - input shape = (2,3,4), shape = (-3,-2), output shape = (6,4)- ``-4`` split one dimension of the input into two dimensions passed subsequent to -4 in shape (can contain -1).  Example::  - input shape = (2,3,4), shape = (-4,1,2,-2), output shape =(1,2,3,4)  - input shape = (2,3,4), shape = (2,-4,-1,3,-2), output shape = (2,1,3,4)If the argument `reverse` is set to 1, then the special values are inferred from right to left.  Example::  - without reverse=1, for input shape = (10,5,4), shape = (-1,0), output shape would be (40,5)  - with reverse=1, output shape will be (50,4).Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L164
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to reshape.</param>
/// <param name="shape">The target shape</param>
/// <param name="reverse">If true then the special values are inferred from right to left</param>
 /// <returns>returns new symbol</returns>
public static NdArray Reshape(NdArray @out,
NdArray data,
Shape shape=null,
bool reverse=false)
{
return new Operator("Reshape")
.SetParam("shape", shape)
.SetParam("reverse", reverse)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Reshapes the input array... note:: ``Reshape`` is deprecated, use ``reshape``Given an array and a shape, this function returns a copy of the array in the new shape.The shape is a tuple of integers such as (2,3,4).The size of the new shape should be same as the size of the input array.Example::  reshape([1,2,3,4], shape=(2,2)) = [[1,2], [3,4]]Some dimensions of the shape can take special values from the set {0, -1, -2, -3, -4}. The significance of each is explained below:- ``0``  copy this dimension from the input to the output shape.  Example::  - input shape = (2,3,4), shape = (4,0,2), output shape = (4,3,2)  - input shape = (2,3,4), shape = (2,0,0), output shape = (2,3,4)- ``-1`` infers the dimension of the output shape by using the remainder of the input dimensions  keeping the size of the new array same as that of the input array.  At most one dimension of shape can be -1.  Example::  - input shape = (2,3,4), shape = (6,1,-1), output shape = (6,1,4)  - input shape = (2,3,4), shape = (3,-1,8), output shape = (3,1,8)  - input shape = (2,3,4), shape=(-1,), output shape = (24,)- ``-2`` copy all/remainder of the input dimensions to the output shape.  Example::  - input shape = (2,3,4), shape = (-2,), output shape = (2,3,4)  - input shape = (2,3,4), shape = (2,-2), output shape = (2,3,4)  - input shape = (2,3,4), shape = (-2,1,1), output shape = (2,3,4,1,1)- ``-3`` use the product of two consecutive dimensions of the input shape as the output dimension.  Example::  - input shape = (2,3,4), shape = (-3,4), output shape = (6,4)  - input shape = (2,3,4,5), shape = (-3,-3), output shape = (6,20)  - input shape = (2,3,4), shape = (0,-3), output shape = (2,12)  - input shape = (2,3,4), shape = (-3,-2), output shape = (6,4)- ``-4`` split one dimension of the input into two dimensions passed subsequent to -4 in shape (can contain -1).  Example::  - input shape = (2,3,4), shape = (-4,1,2,-2), output shape =(1,2,3,4)  - input shape = (2,3,4), shape = (2,-4,-1,3,-2), output shape = (2,1,3,4)If the argument `reverse` is set to 1, then the special values are inferred from right to left.  Example::  - without reverse=1, for input shape = (10,5,4), shape = (-1,0), output shape would be (40,5)  - with reverse=1, output shape will be (50,4).Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L164
/// </summary>
/// <param name="data">Input data to reshape.</param>
/// <param name="shape">The target shape</param>
/// <param name="reverse">If true then the special values are inferred from right to left</param>
 /// <returns>returns new symbol</returns>
public static NdArray Reshape(NdArray data,
Shape shape=null,
bool reverse=false)
{
return new Operator("Reshape")
.SetParam("shape", shape)
.SetParam("reverse", reverse)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Flattens the input array into a 2-D array by collapsing the higher dimensions... note:: `Flatten` is deprecated. Use `flatten` instead.For an input array with shape ``(d1, d2, ..., dk)``, `flatten` operation reshapesthe input array into an output array of shape ``(d1, d2*...*dk)``.Example::    x = [[        [1,2,3],        [4,5,6],        [7,8,9]    ],    [    [1,2,3],        [4,5,6],        [7,8,9]    ]],    flatten(x) = [[ 1.,  2.,  3.,  4.,  5.,  6.,  7.,  8.,  9.],       [ 1.,  2.,  3.,  4.,  5.,  6.,  7.,  8.,  9.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L208
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Flatten(NdArray @out,
NdArray data)
{
return new Operator("Flatten")
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Flattens the input array into a 2-D array by collapsing the higher dimensions... note:: `Flatten` is deprecated. Use `flatten` instead.For an input array with shape ``(d1, d2, ..., dk)``, `flatten` operation reshapesthe input array into an output array of shape ``(d1, d2*...*dk)``.Example::    x = [[        [1,2,3],        [4,5,6],        [7,8,9]    ],    [    [1,2,3],        [4,5,6],        [7,8,9]    ]],    flatten(x) = [[ 1.,  2.,  3.,  4.,  5.,  6.,  7.,  8.,  9.],       [ 1.,  2.,  3.,  4.,  5.,  6.,  7.,  8.,  9.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L208
/// </summary>
/// <param name="data">Input array.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Flatten(NdArray data)
{
return new Operator("Flatten")
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Permutes the dimensions of an array.Examples::  x = [[ 1, 2],       [ 3, 4]]  transpose(x) = [[ 1.,  3.],                  [ 2.,  4.]]  x = [[[ 1.,  2.],        [ 3.,  4.]],       [[ 5.,  6.],        [ 7.,  8.]]]  transpose(x) = [[[ 1.,  5.],                   [ 3.,  7.]],                  [[ 2.,  6.],                   [ 4.,  8.]]]  transpose(x, axes=(1,0,2)) = [[[ 1.,  2.],                                 [ 5.,  6.]],                                [[ 3.,  4.],                                 [ 7.,  8.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L253
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Source input</param>
/// <param name="axes">Target axis order. By default the axes will be inverted.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Transpose(NdArray @out,
NdArray data,
Shape axes=null)
{
return new Operator("transpose")
.SetParam("axes", axes)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Permutes the dimensions of an array.Examples::  x = [[ 1, 2],       [ 3, 4]]  transpose(x) = [[ 1.,  3.],                  [ 2.,  4.]]  x = [[[ 1.,  2.],        [ 3.,  4.]],       [[ 5.,  6.],        [ 7.,  8.]]]  transpose(x) = [[[ 1.,  5.],                   [ 3.,  7.]],                  [[ 2.,  6.],                   [ 4.,  8.]]]  transpose(x, axes=(1,0,2)) = [[[ 1.,  2.],                                 [ 5.,  6.]],                                [[ 3.,  4.],                                 [ 7.,  8.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L253
/// </summary>
/// <param name="data">Source input</param>
/// <param name="axes">Target axis order. By default the axes will be inverted.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Transpose(NdArray data,
Shape axes=null)
{
return new Operator("transpose")
.SetParam("axes", axes)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Inserts a new axis of size 1 into the array shapeFor example, given ``x`` with shape ``(2,3,4)``, then ``expand_dims(x, axis=1)``will return a new array with shape ``(2,1,3,4)``.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L289
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Source input</param>
/// <param name="axis">Position where new axis is to be inserted. Suppose that the input `NDArray`'s dimension is `ndim`, the range of the inserted axis is `[-ndim, ndim]`</param>
 /// <returns>returns new symbol</returns>
public static NdArray ExpandDims(NdArray @out,
NdArray data,
int axis)
{
return new Operator("expand_dims")
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Inserts a new axis of size 1 into the array shapeFor example, given ``x`` with shape ``(2,3,4)``, then ``expand_dims(x, axis=1)``will return a new array with shape ``(2,1,3,4)``.Defined in I:\workspace\incubator-mxnet\src\operator\tensor\matrix_op.cc:L289
/// </summary>
/// <param name="data">Source input</param>
/// <param name="axis">Position where new axis is to be inserted. Suppose that the input `NDArray`'s dimension is `ndim`, the range of the inserted axis is `[-ndim, ndim]`</param>
 /// <returns>returns new symbol</returns>
public static NdArray ExpandDims(NdArray data,
int axis)
{
return new Operator("expand_dims")
.SetParam("axis", axis)
.SetInput("data", data)
.Invoke();
}
private static readonly List<string> TopkRetTypConvert = new List<string>(){"both","indices","mask","value"};
/// <summary>
/// Returns the top *k* elements in an input array along the given axis.Examples::  x = [[ 0.3,  0.2,  0.4],       [ 0.1,  0.3,  0.2]]  // returns an index of the largest element on last axis  topk(x) = [[ 2.],             [ 1.]]  // returns the value of top-2 largest elements on last axis  topk(x, ret_typ='value', k=2) = [[ 0.4,  0.3],                                   [ 0.3,  0.2]]  // returns the value of top-2 smallest elements on last axis  topk(x, ret_typ='value', k=2, is_ascend=1) = [[ 0.2 ,  0.3],                                               [ 0.1 ,  0.2]]  // returns the value of top-2 largest elements on axis 0  topk(x, axis=0, ret_typ='value', k=2) = [[ 0.3,  0.3,  0.4],                                           [ 0.1,  0.2,  0.2]]  // flattens and then returns list of both values and indices  topk(x, ret_typ='both', k=2) = [[[ 0.4,  0.3], [ 0.3,  0.2]] ,  [[ 2.,  0.], [ 1.,  2.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\ordering_op.cc:L63
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array</param>
/// <param name="axis">Axis along which to choose the top k indices. If not given, the flattened array is used. Default is -1.</param>
/// <param name="k">Number of top elements to select, should be always smaller than or equal to the element number in the given axis. A global sort is performed if set k < 1.</param>
/// <param name="ret_typ">The return type. "value" means to return the top k values, "indices" means to return the indices of the top k values, "mask" means to return a mask array containing 0 and 1. 1 means the top k values. "both" means to return a list of both values and indices of top k elements.</param>
/// <param name="is_ascend">Whether to choose k largest or k smallest elements. Top K largest elements will be chosen if set to false.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Topk(NdArray @out,
NdArray data,
int? axis=-1,
int k=1,
TopkRetTyp ret_typ=TopkRetTyp.Indices,
bool is_ascend=false)
{
return new Operator("topk")
.SetParam("axis", axis)
.SetParam("k", k)
.SetParam("ret_typ", Util.EnumToString<TopkRetTyp>(ret_typ,TopkRetTypConvert))
.SetParam("is_ascend", is_ascend)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the top *k* elements in an input array along the given axis.Examples::  x = [[ 0.3,  0.2,  0.4],       [ 0.1,  0.3,  0.2]]  // returns an index of the largest element on last axis  topk(x) = [[ 2.],             [ 1.]]  // returns the value of top-2 largest elements on last axis  topk(x, ret_typ='value', k=2) = [[ 0.4,  0.3],                                   [ 0.3,  0.2]]  // returns the value of top-2 smallest elements on last axis  topk(x, ret_typ='value', k=2, is_ascend=1) = [[ 0.2 ,  0.3],                                               [ 0.1 ,  0.2]]  // returns the value of top-2 largest elements on axis 0  topk(x, axis=0, ret_typ='value', k=2) = [[ 0.3,  0.3,  0.4],                                           [ 0.1,  0.2,  0.2]]  // flattens and then returns list of both values and indices  topk(x, ret_typ='both', k=2) = [[[ 0.4,  0.3], [ 0.3,  0.2]] ,  [[ 2.,  0.], [ 1.,  2.]]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\ordering_op.cc:L63
/// </summary>
/// <param name="data">The input array</param>
/// <param name="axis">Axis along which to choose the top k indices. If not given, the flattened array is used. Default is -1.</param>
/// <param name="k">Number of top elements to select, should be always smaller than or equal to the element number in the given axis. A global sort is performed if set k < 1.</param>
/// <param name="ret_typ">The return type. "value" means to return the top k values, "indices" means to return the indices of the top k values, "mask" means to return a mask array containing 0 and 1. 1 means the top k values. "both" means to return a list of both values and indices of top k elements.</param>
/// <param name="is_ascend">Whether to choose k largest or k smallest elements. Top K largest elements will be chosen if set to false.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Topk(NdArray data,
int? axis=-1,
int k=1,
TopkRetTyp ret_typ=TopkRetTyp.Indices,
bool is_ascend=false)
{
return new Operator("topk")
.SetParam("axis", axis)
.SetParam("k", k)
.SetParam("ret_typ", Util.EnumToString<TopkRetTyp>(ret_typ,TopkRetTypConvert))
.SetParam("is_ascend", is_ascend)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTopk(NdArray @out)
{
return new Operator("_backward_topk")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardTopk()
{
return new Operator("_backward_topk")
.Invoke();
}
/// <summary>
/// Returns a sorted copy of an input array along the given axis.Examples::  x = [[ 1, 4],       [ 3, 1]]  // sorts along the last axis  sort(x) = [[ 1.,  4.],             [ 1.,  3.]]  // flattens and then sorts  sort(x) = [ 1.,  1.,  3.,  4.]  // sorts along the first axis  sort(x, axis=0) = [[ 1.,  1.],                     [ 3.,  4.]]  // in a descend order  sort(x, is_ascend=0) = [[ 4.,  1.],                          [ 3.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\ordering_op.cc:L126
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array</param>
/// <param name="axis">Axis along which to choose sort the input tensor. If not given, the flattened array is used. Default is -1.</param>
/// <param name="is_ascend">Whether to sort in ascending or descending order.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sort(NdArray @out,
NdArray data,
int? axis=-1,
bool is_ascend=true)
{
return new Operator("sort")
.SetParam("axis", axis)
.SetParam("is_ascend", is_ascend)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns a sorted copy of an input array along the given axis.Examples::  x = [[ 1, 4],       [ 3, 1]]  // sorts along the last axis  sort(x) = [[ 1.,  4.],             [ 1.,  3.]]  // flattens and then sorts  sort(x) = [ 1.,  1.,  3.,  4.]  // sorts along the first axis  sort(x, axis=0) = [[ 1.,  1.],                     [ 3.,  4.]]  // in a descend order  sort(x, is_ascend=0) = [[ 4.,  1.],                          [ 3.,  1.]]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\ordering_op.cc:L126
/// </summary>
/// <param name="data">The input array</param>
/// <param name="axis">Axis along which to choose sort the input tensor. If not given, the flattened array is used. Default is -1.</param>
/// <param name="is_ascend">Whether to sort in ascending or descending order.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Sort(NdArray data,
int? axis=-1,
bool is_ascend=true)
{
return new Operator("sort")
.SetParam("axis", axis)
.SetParam("is_ascend", is_ascend)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Returns the indices that would sort an input array along the given axis.This function performs sorting along the given axis and returns an array of indices having same shapeas an input array that index data in sorted order.Examples::  x = [[ 0.3,  0.2,  0.4],       [ 0.1,  0.3,  0.2]]  // sort along axis -1  argsort(x) = [[ 1.,  0.,  2.],                [ 0.,  2.,  1.]]  // sort along axis 0  argsort(x, axis=0) = [[ 1.,  0.,  1.]                        [ 0.,  1.,  0.]]  // flatten and then sort  argsort(x) = [ 3.,  1.,  5.,  0.,  4.,  2.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\ordering_op.cc:L176
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array</param>
/// <param name="axis">Axis along which to sort the input tensor. If not given, the flattened array is used. Default is -1.</param>
/// <param name="is_ascend">Whether to sort in ascending or descending order.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Argsort(NdArray @out,
NdArray data,
int? axis=-1,
bool is_ascend=true)
{
return new Operator("argsort")
.SetParam("axis", axis)
.SetParam("is_ascend", is_ascend)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Returns the indices that would sort an input array along the given axis.This function performs sorting along the given axis and returns an array of indices having same shapeas an input array that index data in sorted order.Examples::  x = [[ 0.3,  0.2,  0.4],       [ 0.1,  0.3,  0.2]]  // sort along axis -1  argsort(x) = [[ 1.,  0.,  2.],                [ 0.,  2.,  1.]]  // sort along axis 0  argsort(x, axis=0) = [[ 1.,  0.,  1.]                        [ 0.,  1.,  0.]]  // flatten and then sort  argsort(x) = [ 3.,  1.,  5.,  0.,  4.,  2.]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\ordering_op.cc:L176
/// </summary>
/// <param name="data">The input array</param>
/// <param name="axis">Axis along which to sort the input tensor. If not given, the flattened array is used. Default is -1.</param>
/// <param name="is_ascend">Whether to sort in ascending or descending order.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Argsort(NdArray data,
int? axis=-1,
bool is_ascend=true)
{
return new Operator("argsort")
.SetParam("axis", axis)
.SetParam("is_ascend", is_ascend)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// pick rows specified by user input index array from a row sparse matrixand save them in the output sparse matrix.Example::  data = [[1, 2], [3, 4], [5, 6]]  indices = [0, 1, 3]  shape = (4, 2)  rsp_in = row_sparse(data, indices)  to_retain = [0, 3]  rsp_out = retain(rsp_in, to_retain)  rsp_out.values = [[1, 2], [5, 6]]  rsp_out.indices = [0, 3]The storage type of ``retain`` output depends on storage types of inputs- retain(row_sparse, default) = row_sparse- otherwise, ``retain`` is not supportedDefined in I:\workspace\incubator-mxnet\src\operator\tensor\sparse_retain.cc:L53
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array for sparse_retain operator.</param>
/// <param name="indices">The index array of rows ids that will be retained.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SparseRetain(NdArray @out,
NdArray data,
NdArray indices)
{
return new Operator("_sparse_retain")
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke(@out);
}
/// <summary>
/// pick rows specified by user input index array from a row sparse matrixand save them in the output sparse matrix.Example::  data = [[1, 2], [3, 4], [5, 6]]  indices = [0, 1, 3]  shape = (4, 2)  rsp_in = row_sparse(data, indices)  to_retain = [0, 3]  rsp_out = retain(rsp_in, to_retain)  rsp_out.values = [[1, 2], [5, 6]]  rsp_out.indices = [0, 3]The storage type of ``retain`` output depends on storage types of inputs- retain(row_sparse, default) = row_sparse- otherwise, ``retain`` is not supportedDefined in I:\workspace\incubator-mxnet\src\operator\tensor\sparse_retain.cc:L53
/// </summary>
/// <param name="data">The input array for sparse_retain operator.</param>
/// <param name="indices">The index array of rows ids that will be retained.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SparseRetain(NdArray data,
NdArray indices)
{
return new Operator("_sparse_retain")
.SetInput("data", data)
.SetInput("indices", indices)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSparseRetain(NdArray @out)
{
return new Operator("_backward_sparse_retain")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSparseRetain()
{
return new Operator("_backward_sparse_retain")
.Invoke();
}
/// <summary>
/// Computes the square sum of array elements over a given axisfor row-sparse matrix. This is a temporary solution for fusing ops square andsum together for row-sparse matrix to save memory for storing gradients.It will become deprecated once the functionality of fusing operators is finishedin the future.Example::  dns = mx.nd.array([[0, 0], [1, 2], [0, 0], [3, 4], [0, 0]])  rsp = dns.tostype('row_sparse')  sum = mx.nd._internal._square_sum(rsp, axis=1)  sum = [0, 5, 0, 25, 0]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\square_sum.cc:L63
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SquareSum(NdArray @out,
NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("_square_sum")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Computes the square sum of array elements over a given axisfor row-sparse matrix. This is a temporary solution for fusing ops square andsum together for row-sparse matrix to save memory for storing gradients.It will become deprecated once the functionality of fusing operators is finishedin the future.Example::  dns = mx.nd.array([[0, 0], [1, 2], [0, 0], [3, 4], [0, 0]])  rsp = dns.tostype('row_sparse')  sum = mx.nd._internal._square_sum(rsp, axis=1)  sum = [0, 5, 0, 25, 0]Defined in I:\workspace\incubator-mxnet\src\operator\tensor\square_sum.cc:L63
/// </summary>
/// <param name="data">The input</param>
/// <param name="axis">The axis or axes along which to perform the reduction.      The default, `axis=()`, will compute over all elements into a      scalar array with shape `(1,)`.      If `axis` is int, a reduction is performed on a particular axis.      If `axis` is a tuple of ints, a reduction is performed on all the axes      specified in the tuple.      If `exclude` is true, reduction will be performed on the axes that are      NOT in axis instead.      Negative values means indexing from right to left.</param>
/// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
/// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SquareSum(NdArray data,
Shape axis=null,
bool keepdims=false,
bool exclude=false)
{
return new Operator("_square_sum")
.SetParam("axis", axis)
.SetParam("keepdims", keepdims)
.SetParam("exclude", exclude)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSquareSum(NdArray @out)
{
return new Operator("_backward_square_sum")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSquareSum()
{
return new Operator("_backward_square_sum")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray CustomFunction(NdArray @out)
{
return new Operator("_CustomFunction")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray CustomFunction()
{
return new Operator("_CustomFunction")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCustomFunction(NdArray @out)
{
return new Operator("_backward_CustomFunction")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCustomFunction()
{
return new Operator("_backward_CustomFunction")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray CachedOp(NdArray @out)
{
return new Operator("_CachedOp")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray CachedOp()
{
return new Operator("_CachedOp")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCachedOp(NdArray @out)
{
return new Operator("_backward_CachedOp")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCachedOp()
{
return new Operator("_backward_CachedOp")
.Invoke();
}
/// <summary>
/// Decode image with OpenCV. Note: return image in RGB by default, instead of OpenCV's default BGR.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="buf">Buffer containing binary encoded image</param>
/// <param name="flag">Convert decoded image to grayscale (0) or color (1).</param>
/// <param name="to_rgb">Whether to convert decoded image to mxnet's default RGB format (instead of opencv's default BGR).</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cvimdecode(NdArray @out,
Symbol buf,
int flag=1,
bool to_rgb=true)
{
return new Operator("_cvimdecode")
.SetParam("buf", buf)
.SetParam("flag", flag)
.SetParam("to_rgb", to_rgb)
.Invoke(@out);
}
/// <summary>
/// Decode image with OpenCV. Note: return image in RGB by default, instead of OpenCV's default BGR.
/// </summary>
/// <param name="buf">Buffer containing binary encoded image</param>
/// <param name="flag">Convert decoded image to grayscale (0) or color (1).</param>
/// <param name="to_rgb">Whether to convert decoded image to mxnet's default RGB format (instead of opencv's default BGR).</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cvimdecode(Symbol buf,
int flag=1,
bool to_rgb=true)
{
return new Operator("_cvimdecode")
.SetParam("buf", buf)
.SetParam("flag", flag)
.SetParam("to_rgb", to_rgb)
.Invoke();
}
/// <summary>
/// Read and decode image with OpenCV. Note: return image in RGB by default, instead of OpenCV's default BGR.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="filename">Name of the image file to be loaded.</param>
/// <param name="flag">Convert decoded image to grayscale (0) or color (1).</param>
/// <param name="to_rgb">Whether to convert decoded image to mxnet's default RGB format (instead of opencv's default BGR).</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cvimread(NdArray @out,
string filename,
int flag=1,
bool to_rgb=true)
{
return new Operator("_cvimread")
.SetParam("filename", filename)
.SetParam("flag", flag)
.SetParam("to_rgb", to_rgb)
.Invoke(@out);
}
/// <summary>
/// Read and decode image with OpenCV. Note: return image in RGB by default, instead of OpenCV's default BGR.
/// </summary>
/// <param name="filename">Name of the image file to be loaded.</param>
/// <param name="flag">Convert decoded image to grayscale (0) or color (1).</param>
/// <param name="to_rgb">Whether to convert decoded image to mxnet's default RGB format (instead of opencv's default BGR).</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cvimread(string filename,
int flag=1,
bool to_rgb=true)
{
return new Operator("_cvimread")
.SetParam("filename", filename)
.SetParam("flag", flag)
.SetParam("to_rgb", to_rgb)
.Invoke();
}
/// <summary>
/// Resize image with OpenCV. 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source image</param>
/// <param name="w">Width of resized image.</param>
/// <param name="h">Height of resized image.</param>
/// <param name="interp">Interpolation method (default=cv2.INTER_LINEAR).</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cvimresize(NdArray @out,
Symbol data,
int w,
int h,
int interp=1)
{
return new Operator("_cvimresize")
.SetParam("data", data)
.SetParam("w", w)
.SetParam("h", h)
.SetParam("interp", interp)
.Invoke(@out);
}
/// <summary>
/// Resize image with OpenCV. 
/// </summary>
/// <param name="data">source image</param>
/// <param name="w">Width of resized image.</param>
/// <param name="h">Height of resized image.</param>
/// <param name="interp">Interpolation method (default=cv2.INTER_LINEAR).</param>
 /// <returns>returns new symbol</returns>
public static NdArray Cvimresize(Symbol data,
int w,
int h,
int interp=1)
{
return new Operator("_cvimresize")
.SetParam("data", data)
.SetParam("w", w)
.SetParam("h", h)
.SetParam("interp", interp)
.Invoke();
}
/// <summary>
/// Pad image border with OpenCV. 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">source image</param>
/// <param name="top">Top margin.</param>
/// <param name="bot">Bottom margin.</param>
/// <param name="left">Left margin.</param>
/// <param name="right">Right margin.</param>
/// <param name="type">Filling type (default=cv2.BORDER_CONSTANT).</param>
/// <param name="values">Fill with value(RGB[A] or gray), up to 4 channels.</param>
 /// <returns>returns new symbol</returns>
public static NdArray CvcopyMakeBorder(NdArray @out,
Symbol data,
int top,
int bot,
int left,
int right,
int type=0,
object[] values=null)
{
return new Operator("_cvcopyMakeBorder")
.SetParam("data", data)
.SetParam("top", top)
.SetParam("bot", bot)
.SetParam("left", left)
.SetParam("right", right)
.SetParam("type", type)
.SetParam("values", values)
.Invoke(@out);
}
/// <summary>
/// Pad image border with OpenCV. 
/// </summary>
/// <param name="data">source image</param>
/// <param name="top">Top margin.</param>
/// <param name="bot">Bottom margin.</param>
/// <param name="left">Left margin.</param>
/// <param name="right">Right margin.</param>
/// <param name="type">Filling type (default=cv2.BORDER_CONSTANT).</param>
/// <param name="values">Fill with value(RGB[A] or gray), up to 4 channels.</param>
 /// <returns>returns new symbol</returns>
public static NdArray CvcopyMakeBorder(Symbol data,
int top,
int bot,
int left,
int right,
int type=0,
object[] values=null)
{
return new Operator("_cvcopyMakeBorder")
.SetParam("data", data)
.SetParam("top", top)
.SetParam("bot", bot)
.SetParam("left", left)
.SetParam("right", right)
.SetParam("type", type)
.SetParam("values", values)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">input data</param>
 /// <returns>returns new symbol</returns>
public static NdArray Copyto(NdArray @out,
Symbol data)
{
return new Operator("_copyto")
.SetParam("data", data)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="data">input data</param>
 /// <returns>returns new symbol</returns>
public static NdArray Copyto(Symbol data)
{
return new Operator("_copyto")
.SetParam("data", data)
.Invoke();
}
/// <summary>
/// Place holder for variable who cannot perform gradient
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray NoGradient(NdArray @out)
{
return new Operator("_NoGradient")
.Invoke(@out);
}
/// <summary>
/// Place holder for variable who cannot perform gradient
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray NoGradient()
{
return new Operator("_NoGradient")
.Invoke();
}
/// <summary>
/// Computes the Khatri-Rao product of the input matrices.Given a collection of :math:`n` input matrices,.. math::   A_1 \in \mathbb{R}^{M_1 \times M}, \ldots, A_n \in \mathbb{R}^{M_n \times N},the (column-wise) Khatri-Rao product is defined as the matrix,.. math::   X = A_1 \otimes \cdots \otimes A_n \in \mathbb{R}^{(M_1 \cdots M_n) \times N},where the :math:`k`th column is equal to the column-wise outer product:math:`{A_1}_k \otimes \cdots \otimes {A_n}_k` where :math:`{A_i}_k` is the kthcolumn of the ith matrix.Example::  >>> A = mx.nd.array([[1, -1],  >>>                  [2, -3]])  >>> B = mx.nd.array([[1, 4],  >>>                  [2, 5],  >>>                  [3, 6]])  >>> C = mx.nd.khatri_rao(A, B)  >>> print(C.asnumpy())  [[  1.  -4.]   [  2.  -5.]   [  3.  -6.]   [  2. -12.]   [  4. -15.]   [  6. -18.]]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\krprod.cc:L108
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="args">Positional input matrices</param>
 /// <returns>returns new symbol</returns>
public static NdArray KhatriRao(NdArray @out,
NdArray[] args)
{
return new Operator("khatri_rao")
.AddInput(args)
.Invoke(@out);
}
/// <summary>
/// Computes the Khatri-Rao product of the input matrices.Given a collection of :math:`n` input matrices,.. math::   A_1 \in \mathbb{R}^{M_1 \times M}, \ldots, A_n \in \mathbb{R}^{M_n \times N},the (column-wise) Khatri-Rao product is defined as the matrix,.. math::   X = A_1 \otimes \cdots \otimes A_n \in \mathbb{R}^{(M_1 \cdots M_n) \times N},where the :math:`k`th column is equal to the column-wise outer product:math:`{A_1}_k \otimes \cdots \otimes {A_n}_k` where :math:`{A_i}_k` is the kthcolumn of the ith matrix.Example::  >>> A = mx.nd.array([[1, -1],  >>>                  [2, -3]])  >>> B = mx.nd.array([[1, 4],  >>>                  [2, 5],  >>>                  [3, 6]])  >>> C = mx.nd.khatri_rao(A, B)  >>> print(C.asnumpy())  [[  1.  -4.]   [  2.  -5.]   [  3.  -6.]   [  2. -12.]   [  4. -15.]   [  6. -18.]]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\krprod.cc:L108
/// </summary>
/// <param name="args">Positional input matrices</param>
 /// <returns>returns new symbol</returns>
public static NdArray KhatriRao(NdArray[] args)
{
return new Operator("khatri_rao")
.AddInput(args)
.Invoke();
}
/// <summary>
/// Apply a custom operator implemented in a frontend language (like Python).Custom operators should override required methods like `forward` and `backward`.The custom operator must be registered before it can be used.Please check the tutorial here: http://mxnet.io/how_to/new_op.html.Defined in I:\workspace\incubator-mxnet\src\operator\custom\custom.cc:L369
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data for the custom operator.</param>
/// <param name="op_type">Name of the custom operator. This is the name that is passed to `mx.operator.register` to register the operator.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Custom(NdArray @out,
NdArray[] data,
string op_type)
{
return new Operator("Custom")
.SetParam("op_type", op_type)
.AddInput(data)
.Invoke(@out);
}
/// <summary>
/// Apply a custom operator implemented in a frontend language (like Python).Custom operators should override required methods like `forward` and `backward`.The custom operator must be registered before it can be used.Please check the tutorial here: http://mxnet.io/how_to/new_op.html.Defined in I:\workspace\incubator-mxnet\src\operator\custom\custom.cc:L369
/// </summary>
/// <param name="data">Input data for the custom operator.</param>
/// <param name="op_type">Name of the custom operator. This is the name that is passed to `mx.operator.register` to register the operator.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Custom(NdArray[] data,
string op_type)
{
return new Operator("Custom")
.SetParam("op_type", op_type)
.AddInput(data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCustom(NdArray @out)
{
return new Operator("_backward_Custom")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCustom()
{
return new Operator("_backward_Custom")
.Invoke();
}
/// <summary>
/// Batch normalization.Normalizes a data batch by mean and variance, and applies a scale ``gamma`` aswell as offset ``beta``.Assume the input has more than one dimension and we normalize along axis 1.We first compute the mean and variance along this axis:.. math::  data\_mean[i] = mean(data[:,i,:,...]) \\  data\_var[i] = var(data[:,i,:,...])Then compute the normalized output, which has the same shape as input, as following:.. math::  out[:,i,:,...] = \frac{data[:,i,:,...] - data\_mean[i]}{\sqrt{data\_var[i]+\epsilon}} * gamma[i] + beta[i]Both *mean* and *var* returns a scalar by treating the input as a vector.Assume the input has size *k* on axis 1, then both ``gamma`` and ``beta``have shape *(k,)*. If ``output_mean_var`` is set to be true, then outputs both ``data_mean`` and``data_var`` as well, which are needed for the backward pass.Besides the inputs and the outputs, this operator accepts two auxiliarystates, ``moving_mean`` and ``moving_var``, which are *k*-lengthvectors. They are global statistics for the whole dataset, which are updatedby::  moving_mean = moving_mean * momentum + data_mean * (1 - momentum)  moving_var = moving_var * momentum + data_var * (1 - momentum)If ``use_global_stats`` is set to be true, then ``moving_mean`` and``moving_var`` are used instead of ``data_mean`` and ``data_var`` to computethe output. It is often used during inference.Both ``gamma`` and ``beta`` are learnable parameters. But if ``fix_gamma`` is true,then set ``gamma`` to 1 and its gradient to 0.Defined in I:\workspace\incubator-mxnet\src\operator\batch_norm_v1.cc:L90
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to batch normalization</param>
/// <param name="gamma">gamma array</param>
/// <param name="beta">beta array</param>
/// <param name="eps">Epsilon to prevent div 0</param>
/// <param name="momentum">Momentum for moving average</param>
/// <param name="fix_gamma">Fix gamma while training</param>
/// <param name="use_global_stats">Whether use global moving statistics instead of local batch-norm. This will force change batch-norm into a scale shift operator.</param>
/// <param name="output_mean_var">Output All,normal mean and var</param>
 /// <returns>returns new symbol</returns>
public static NdArray BatchNormV1(NdArray @out,
NdArray data,
NdArray gamma,
NdArray beta,
float eps=0.001f,
float momentum=0.9f,
bool fix_gamma=true,
bool use_global_stats=false,
bool output_mean_var=false)
{
return new Operator("BatchNorm_v1")
.SetParam("eps", eps)
.SetParam("momentum", momentum)
.SetParam("fix_gamma", fix_gamma)
.SetParam("use_global_stats", use_global_stats)
.SetParam("output_mean_var", output_mean_var)
.SetInput("data", data)
.SetInput("gamma", gamma)
.SetInput("beta", beta)
.Invoke(@out);
}
/// <summary>
/// Batch normalization.Normalizes a data batch by mean and variance, and applies a scale ``gamma`` aswell as offset ``beta``.Assume the input has more than one dimension and we normalize along axis 1.We first compute the mean and variance along this axis:.. math::  data\_mean[i] = mean(data[:,i,:,...]) \\  data\_var[i] = var(data[:,i,:,...])Then compute the normalized output, which has the same shape as input, as following:.. math::  out[:,i,:,...] = \frac{data[:,i,:,...] - data\_mean[i]}{\sqrt{data\_var[i]+\epsilon}} * gamma[i] + beta[i]Both *mean* and *var* returns a scalar by treating the input as a vector.Assume the input has size *k* on axis 1, then both ``gamma`` and ``beta``have shape *(k,)*. If ``output_mean_var`` is set to be true, then outputs both ``data_mean`` and``data_var`` as well, which are needed for the backward pass.Besides the inputs and the outputs, this operator accepts two auxiliarystates, ``moving_mean`` and ``moving_var``, which are *k*-lengthvectors. They are global statistics for the whole dataset, which are updatedby::  moving_mean = moving_mean * momentum + data_mean * (1 - momentum)  moving_var = moving_var * momentum + data_var * (1 - momentum)If ``use_global_stats`` is set to be true, then ``moving_mean`` and``moving_var`` are used instead of ``data_mean`` and ``data_var`` to computethe output. It is often used during inference.Both ``gamma`` and ``beta`` are learnable parameters. But if ``fix_gamma`` is true,then set ``gamma`` to 1 and its gradient to 0.Defined in I:\workspace\incubator-mxnet\src\operator\batch_norm_v1.cc:L90
/// </summary>
/// <param name="data">Input data to batch normalization</param>
/// <param name="gamma">gamma array</param>
/// <param name="beta">beta array</param>
/// <param name="eps">Epsilon to prevent div 0</param>
/// <param name="momentum">Momentum for moving average</param>
/// <param name="fix_gamma">Fix gamma while training</param>
/// <param name="use_global_stats">Whether use global moving statistics instead of local batch-norm. This will force change batch-norm into a scale shift operator.</param>
/// <param name="output_mean_var">Output All,normal mean and var</param>
 /// <returns>returns new symbol</returns>
public static NdArray BatchNormV1(NdArray data,
NdArray gamma,
NdArray beta,
float eps=0.001f,
float momentum=0.9f,
bool fix_gamma=true,
bool use_global_stats=false,
bool output_mean_var=false)
{
return new Operator("BatchNorm_v1")
.SetParam("eps", eps)
.SetParam("momentum", momentum)
.SetParam("fix_gamma", fix_gamma)
.SetParam("use_global_stats", use_global_stats)
.SetParam("output_mean_var", output_mean_var)
.SetInput("data", data)
.SetInput("gamma", gamma)
.SetInput("beta", beta)
.Invoke();
}
/// <summary>
/// Joins input arrays along a given axis... note:: `Concat` is deprecated. Use `concat` instead.The dimensions of the input arrays should be the same except the axis alongwhich they will be concatenated.The dimension of the output array along the concatenated axis will be equalto the sum of the corresponding dimensions of the input arrays.Example::   x = [[1,1],[2,2]]   y = [[3,3],[4,4],[5,5]]   z = [[6,6], [7,7],[8,8]]   concat(x,y,z,dim=0) = [[ 1.,  1.],                          [ 2.,  2.],                          [ 3.,  3.],                          [ 4.,  4.],                          [ 5.,  5.],                          [ 6.,  6.],                          [ 7.,  7.],                          [ 8.,  8.]]   Note that you cannot concat x,y,z along dimension 1 since dimension   0 is not the same for all the input arrays.   concat(y,z,dim=1) = [[ 3.,  3.,  6.,  6.],                         [ 4.,  4.,  7.,  7.],                         [ 5.,  5.,  8.,  8.]]Defined in I:\workspace\incubator-mxnet\src\operator\concat.cc:L104
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">List of arrays to concatenate</param>
/// <param name="num_args">Number of inputs to be concated.</param>
/// <param name="dim">the dimension to be concated.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Concat(NdArray @out,
NdArray[] data,
int num_args,
int dim=1)
{
return new Operator("Concat")
.SetParam("num_args", num_args)
.SetParam("dim", dim)
.AddInput(data)
.Invoke(@out);
}
/// <summary>
/// Joins input arrays along a given axis... note:: `Concat` is deprecated. Use `concat` instead.The dimensions of the input arrays should be the same except the axis alongwhich they will be concatenated.The dimension of the output array along the concatenated axis will be equalto the sum of the corresponding dimensions of the input arrays.Example::   x = [[1,1],[2,2]]   y = [[3,3],[4,4],[5,5]]   z = [[6,6], [7,7],[8,8]]   concat(x,y,z,dim=0) = [[ 1.,  1.],                          [ 2.,  2.],                          [ 3.,  3.],                          [ 4.,  4.],                          [ 5.,  5.],                          [ 6.,  6.],                          [ 7.,  7.],                          [ 8.,  8.]]   Note that you cannot concat x,y,z along dimension 1 since dimension   0 is not the same for all the input arrays.   concat(y,z,dim=1) = [[ 3.,  3.,  6.,  6.],                         [ 4.,  4.,  7.,  7.],                         [ 5.,  5.,  8.,  8.]]Defined in I:\workspace\incubator-mxnet\src\operator\concat.cc:L104
/// </summary>
/// <param name="data">List of arrays to concatenate</param>
/// <param name="num_args">Number of inputs to be concated.</param>
/// <param name="dim">the dimension to be concated.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Concat(NdArray[] data,
int num_args,
int dim=1)
{
return new Operator("Concat")
.SetParam("num_args", num_args)
.SetParam("dim", dim)
.AddInput(data)
.Invoke();
}
private static readonly List<string> ContribCtclossBlankLabelConvert = new List<string>(){"first","last"};
/// <summary>
/// Connectionist Temporal Classification Loss.The shapes of the inputs and outputs:- **data**: `(sequence_length, batch_size, alphabet_size)`- **label**: `(batch_size, label_sequence_length)`- **out**: `(batch_size)`The `data` tensor consists of sequences of activation vectors (without applying softmax),with i-th channel in the last dimension corresponding to i-th labelfor i between 0 and alphabet_size-1 (i.e always 0-indexed).Alphabet size should include one additional value reserved for blank label.When `blank_label` is ``"first"``, the ``0``-th channel is be reserved foractivation of blank label, or otherwise if it is "last", ``(alphabet_size-1)``-th channel should bereserved for blank label.``label`` is an index matrix of integers. When `blank_label` is ``"first"``,the value 0 is then reserved for blank label, and should not be passed in this matrix. Otherwise,when `blank_label` is ``"last"``, the value `(alphabet_size-1)` is reserved for blank label.If a sequence of labels is shorter than *label_sequence_length*, use the specialpadding value at the end of the sequence to conform it to the correctlength. The padding value is `0` when `blank_label` is ``"first"``, and `-1` otherwise.For example, suppose the vocabulary is `[a, b, c]`, and in one batch we have three sequences'ba', 'cbb', and 'abac'. When `blank_label` is ``"first"``, we can index the labels as`{'a': 1, 'b': 2, 'c': 3}`, and we reserve the 0-th channel for blank label in data tensor.The resulting `label` tensor should be padded to be::  [[2, 1, 0, 0], [3, 2, 2, 0], [1, 2, 1, 3]]When `blank_label` is ``"last"``, we can index the labels as`{'a': 0, 'b': 1, 'c': 2}`, and we reserve the channel index 3 for blank label in data tensor.The resulting `label` tensor should be padded to be::  [[1, 0, -1, -1], [2, 1, 1, -1], [0, 1, 0, 2]]``out`` is a list of CTC loss values, one per example in the batch.See *Connectionist Temporal Classification: Labelling UnsegmentedSequence Data with Recurrent Neural Networks*, A. Graves *et al*. for moreinformation on the definition and the algorithm.Defined in I:\workspace\incubator-mxnet\src\operator\contrib\ctc_loss.cc:L115
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the ctc_loss op.</param>
/// <param name="label">Ground-truth labels for the loss.</param>
/// <param name="data_lengths">Lengths of data for each of the samples. Only required when use_data_lengths is true.</param>
/// <param name="label_lengths">Lengths of labels for each of the samples. Only required when use_label_lengths is true.</param>
/// <param name="use_data_lengths">Whether the data lenghts are decided by `data_lengths`. If false, the lengths are equal to the max sequence length.</param>
/// <param name="use_label_lengths">Whether the label lenghts are decided by `label_lengths`, or derived from `padding_mask`. If false, the lengths are derived from the first occurrence of the value of `padding_mask`. The value of `padding_mask` is ``0`` when first CTC label is reserved for blank, and ``-1`` when last label is reserved for blank. See `blank_label`.</param>
/// <param name="blank_label">Set the label that is reserved for blank label.If "first", 0-th label is reserved, and label values for tokens in the vocabulary are between ``1`` and ``alphabet_size-1``, and the padding mask is ``-1``. If "last", last label value ``alphabet_size-1`` is reserved for blank label instead, and label values for tokens in the vocabulary are between ``0`` and ``alphabet_size-2``, and the padding mask is ``0``.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribCTCLoss(NdArray @out,
NdArray data,
NdArray label,
NdArray data_lengths,
NdArray label_lengths,
bool use_data_lengths=false,
bool use_label_lengths=false,
ContribCtclossBlankLabel blank_label=ContribCtclossBlankLabel.First)
{
return new Operator("_contrib_CTCLoss")
.SetParam("use_data_lengths", use_data_lengths)
.SetParam("use_label_lengths", use_label_lengths)
.SetParam("blank_label", Util.EnumToString<ContribCtclossBlankLabel>(blank_label,ContribCtclossBlankLabelConvert))
.SetInput("data", data)
.SetInput("label", label)
.SetInput("data_lengths", data_lengths)
.SetInput("label_lengths", label_lengths)
.Invoke(@out);
}
/// <summary>
/// Connectionist Temporal Classification Loss.The shapes of the inputs and outputs:- **data**: `(sequence_length, batch_size, alphabet_size)`- **label**: `(batch_size, label_sequence_length)`- **out**: `(batch_size)`The `data` tensor consists of sequences of activation vectors (without applying softmax),with i-th channel in the last dimension corresponding to i-th labelfor i between 0 and alphabet_size-1 (i.e always 0-indexed).Alphabet size should include one additional value reserved for blank label.When `blank_label` is ``"first"``, the ``0``-th channel is be reserved foractivation of blank label, or otherwise if it is "last", ``(alphabet_size-1)``-th channel should bereserved for blank label.``label`` is an index matrix of integers. When `blank_label` is ``"first"``,the value 0 is then reserved for blank label, and should not be passed in this matrix. Otherwise,when `blank_label` is ``"last"``, the value `(alphabet_size-1)` is reserved for blank label.If a sequence of labels is shorter than *label_sequence_length*, use the specialpadding value at the end of the sequence to conform it to the correctlength. The padding value is `0` when `blank_label` is ``"first"``, and `-1` otherwise.For example, suppose the vocabulary is `[a, b, c]`, and in one batch we have three sequences'ba', 'cbb', and 'abac'. When `blank_label` is ``"first"``, we can index the labels as`{'a': 1, 'b': 2, 'c': 3}`, and we reserve the 0-th channel for blank label in data tensor.The resulting `label` tensor should be padded to be::  [[2, 1, 0, 0], [3, 2, 2, 0], [1, 2, 1, 3]]When `blank_label` is ``"last"``, we can index the labels as`{'a': 0, 'b': 1, 'c': 2}`, and we reserve the channel index 3 for blank label in data tensor.The resulting `label` tensor should be padded to be::  [[1, 0, -1, -1], [2, 1, 1, -1], [0, 1, 0, 2]]``out`` is a list of CTC loss values, one per example in the batch.See *Connectionist Temporal Classification: Labelling UnsegmentedSequence Data with Recurrent Neural Networks*, A. Graves *et al*. for moreinformation on the definition and the algorithm.Defined in I:\workspace\incubator-mxnet\src\operator\contrib\ctc_loss.cc:L115
/// </summary>
/// <param name="data">Input data to the ctc_loss op.</param>
/// <param name="label">Ground-truth labels for the loss.</param>
/// <param name="data_lengths">Lengths of data for each of the samples. Only required when use_data_lengths is true.</param>
/// <param name="label_lengths">Lengths of labels for each of the samples. Only required when use_label_lengths is true.</param>
/// <param name="use_data_lengths">Whether the data lenghts are decided by `data_lengths`. If false, the lengths are equal to the max sequence length.</param>
/// <param name="use_label_lengths">Whether the label lenghts are decided by `label_lengths`, or derived from `padding_mask`. If false, the lengths are derived from the first occurrence of the value of `padding_mask`. The value of `padding_mask` is ``0`` when first CTC label is reserved for blank, and ``-1`` when last label is reserved for blank. See `blank_label`.</param>
/// <param name="blank_label">Set the label that is reserved for blank label.If "first", 0-th label is reserved, and label values for tokens in the vocabulary are between ``1`` and ``alphabet_size-1``, and the padding mask is ``-1``. If "last", last label value ``alphabet_size-1`` is reserved for blank label instead, and label values for tokens in the vocabulary are between ``0`` and ``alphabet_size-2``, and the padding mask is ``0``.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribCTCLoss(NdArray data,
NdArray label,
NdArray data_lengths,
NdArray label_lengths,
bool use_data_lengths=false,
bool use_label_lengths=false,
ContribCtclossBlankLabel blank_label=ContribCtclossBlankLabel.First)
{
return new Operator("_contrib_CTCLoss")
.SetParam("use_data_lengths", use_data_lengths)
.SetParam("use_label_lengths", use_label_lengths)
.SetParam("blank_label", Util.EnumToString<ContribCtclossBlankLabel>(blank_label,ContribCtclossBlankLabelConvert))
.SetInput("data", data)
.SetInput("label", label)
.SetInput("data_lengths", data_lengths)
.SetInput("label_lengths", label_lengths)
.Invoke();
}
/// <summary>
/// Apply a sparse regularization to the output a sigmoid activation function.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data.</param>
/// <param name="sparseness_target">The sparseness target</param>
/// <param name="penalty">The tradeoff parameter for the sparseness penalty</param>
/// <param name="momentum">The momentum for running average</param>
 /// <returns>returns new symbol</returns>
public static NdArray IdentityAttachKLSparseReg(NdArray @out,
NdArray data,
float sparseness_target=0.1f,
float penalty=0.001f,
float momentum=0.9f)
{
return new Operator("IdentityAttachKLSparseReg")
.SetParam("sparseness_target", sparseness_target)
.SetParam("penalty", penalty)
.SetParam("momentum", momentum)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Apply a sparse regularization to the output a sigmoid activation function.
/// </summary>
/// <param name="data">Input data.</param>
/// <param name="sparseness_target">The sparseness target</param>
/// <param name="penalty">The tradeoff parameter for the sparseness penalty</param>
/// <param name="momentum">The momentum for running average</param>
 /// <returns>returns new symbol</returns>
public static NdArray IdentityAttachKLSparseReg(NdArray data,
float sparseness_target=0.1f,
float penalty=0.001f,
float momentum=0.9f)
{
return new Operator("IdentityAttachKLSparseReg")
.SetParam("sparseness_target", sparseness_target)
.SetParam("penalty", penalty)
.SetParam("momentum", momentum)
.SetInput("data", data)
.Invoke();
}
private static readonly List<string> LeakyreluActTypeConvert = new List<string>(){"elu","leaky","prelu","rrelu"};
/// <summary>
/// Applies Leaky rectified linear unit activation element-wise to the input.Leaky ReLUs attempt to fix the "dying ReLU" problem by allowing a small `slope`when the input is negative and has a slope of one when input is positive.The following modified ReLU Activation functions are supported:- *elu*: Exponential Linear Unit. `y = x > 0 ? x : slope * (exp(x)-1)`- *leaky*: Leaky ReLU. `y = x > 0 ? x : slope * x`- *prelu*: Parametric ReLU. This is same as *leaky* except that `slope` is learnt during training.- *rrelu*: Randomized ReLU. same as *leaky* but the `slope` is uniformly and randomly chosen from  *[lower_bound, upper_bound)* for training, while fixed to be  *(lower_bound+upper_bound)/2* for inference.Defined in I:\workspace\incubator-mxnet\src\operator\leaky_relu.cc:L58
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to activation function.</param>
/// <param name="act_type">Activation function to be applied.</param>
/// <param name="slope">Init slope for the activation. (For leaky and elu only)</param>
/// <param name="lower_bound">Lower bound of random slope. (For rrelu only)</param>
/// <param name="upper_bound">Upper bound of random slope. (For rrelu only)</param>
 /// <returns>returns new symbol</returns>
public static NdArray LeakyReLU(NdArray @out,
NdArray data,
LeakyreluActType act_type=LeakyreluActType.Leaky,
float slope=0.25f,
float lower_bound=0.125f,
float upper_bound=0.334f)
{
return new Operator("LeakyReLU")
.SetParam("act_type", Util.EnumToString<LeakyreluActType>(act_type,LeakyreluActTypeConvert))
.SetParam("slope", slope)
.SetParam("lower_bound", lower_bound)
.SetParam("upper_bound", upper_bound)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Applies Leaky rectified linear unit activation element-wise to the input.Leaky ReLUs attempt to fix the "dying ReLU" problem by allowing a small `slope`when the input is negative and has a slope of one when input is positive.The following modified ReLU Activation functions are supported:- *elu*: Exponential Linear Unit. `y = x > 0 ? x : slope * (exp(x)-1)`- *leaky*: Leaky ReLU. `y = x > 0 ? x : slope * x`- *prelu*: Parametric ReLU. This is same as *leaky* except that `slope` is learnt during training.- *rrelu*: Randomized ReLU. same as *leaky* but the `slope` is uniformly and randomly chosen from  *[lower_bound, upper_bound)* for training, while fixed to be  *(lower_bound+upper_bound)/2* for inference.Defined in I:\workspace\incubator-mxnet\src\operator\leaky_relu.cc:L58
/// </summary>
/// <param name="data">Input data to activation function.</param>
/// <param name="act_type">Activation function to be applied.</param>
/// <param name="slope">Init slope for the activation. (For leaky and elu only)</param>
/// <param name="lower_bound">Lower bound of random slope. (For rrelu only)</param>
/// <param name="upper_bound">Upper bound of random slope. (For rrelu only)</param>
 /// <returns>returns new symbol</returns>
public static NdArray LeakyReLU(NdArray data,
LeakyreluActType act_type=LeakyreluActType.Leaky,
float slope=0.25f,
float lower_bound=0.125f,
float upper_bound=0.334f)
{
return new Operator("LeakyReLU")
.SetParam("act_type", Util.EnumToString<LeakyreluActType>(act_type,LeakyreluActTypeConvert))
.SetParam("slope", slope)
.SetParam("lower_bound", lower_bound)
.SetParam("upper_bound", upper_bound)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Batch normalization.Normalizes a data batch by mean and variance, and applies a scale ``gamma`` aswell as offset ``beta``.Assume the input has more than one dimension and we normalize along axis 1.We first compute the mean and variance along this axis:.. math::  data\_mean[i] = mean(data[:,i,:,...]) \\  data\_var[i] = var(data[:,i,:,...])Then compute the normalized output, which has the same shape as input, as following:.. math::  out[:,i,:,...] = \frac{data[:,i,:,...] - data\_mean[i]}{\sqrt{data\_var[i]+\epsilon}} * gamma[i] + beta[i]Both *mean* and *var* returns a scalar by treating the input as a vector.Assume the input has size *k* on axis 1, then both ``gamma`` and ``beta``have shape *(k,)*. If ``output_mean_var`` is set to be true, then outputs both ``data_mean`` and``data_var`` as well, which are needed for the backward pass.Besides the inputs and the outputs, this operator accepts two auxiliarystates, ``moving_mean`` and ``moving_var``, which are *k*-lengthvectors. They are global statistics for the whole dataset, which are updatedby::  moving_mean = moving_mean * momentum + data_mean * (1 - momentum)  moving_var = moving_var * momentum + data_var * (1 - momentum)If ``use_global_stats`` is set to be true, then ``moving_mean`` and``moving_var`` are used instead of ``data_mean`` and ``data_var`` to computethe output. It is often used during inference.The parameter ``axis`` specifies which axis of the input shape denotesthe 'channel' (separately normalized groups).  The default is 1.  Specifying -1 sets the channelaxis to be the last item in the input shape.Both ``gamma`` and ``beta`` are learnable parameters. But if ``fix_gamma`` is true,then set ``gamma`` to 1 and its gradient to 0.Defined in I:\workspace\incubator-mxnet\src\operator\nn\batch_norm.cc:L400
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to batch normalization</param>
/// <param name="gamma">gamma array</param>
/// <param name="beta">beta array</param>
/// <param name="moving_mean">running mean of input</param>
/// <param name="moving_var">running variance of input</param>
/// <param name="eps">Epsilon to prevent div 0. Must be no less than CUDNN_BN_MIN_EPSILON defined in cudnn.h when using cudnn (usually 1e-5)</param>
/// <param name="momentum">Momentum for moving average</param>
/// <param name="fix_gamma">Fix gamma while training</param>
/// <param name="use_global_stats">Whether use global moving statistics instead of local batch-norm. This will force change batch-norm into a scale shift operator.</param>
/// <param name="output_mean_var">Output All,normal mean and var</param>
/// <param name="axis">Specify which shape axis the channel is specified</param>
/// <param name="cudnn_off">Do not select CUDNN operator, if available</param>
 /// <returns>returns new symbol</returns>
public static NdArray BatchNorm(NdArray @out,
NdArray data,
NdArray gamma,
NdArray beta,
NdArray moving_mean,
NdArray moving_var,
double eps=0.001,
float momentum=0.9f,
bool fix_gamma=true,
bool use_global_stats=false,
bool output_mean_var=false,
int axis=1,
bool cudnn_off=false)
{
return new Operator("BatchNorm")
.SetParam("eps", eps)
.SetParam("momentum", momentum)
.SetParam("fix_gamma", fix_gamma)
.SetParam("use_global_stats", use_global_stats)
.SetParam("output_mean_var", output_mean_var)
.SetParam("axis", axis)
.SetParam("cudnn_off", cudnn_off)
.SetInput("data", data)
.SetInput("gamma", gamma)
.SetInput("beta", beta)
.SetInput("moving_mean", moving_mean)
.SetInput("moving_var", moving_var)
.Invoke(@out);
}
/// <summary>
/// Batch normalization.Normalizes a data batch by mean and variance, and applies a scale ``gamma`` aswell as offset ``beta``.Assume the input has more than one dimension and we normalize along axis 1.We first compute the mean and variance along this axis:.. math::  data\_mean[i] = mean(data[:,i,:,...]) \\  data\_var[i] = var(data[:,i,:,...])Then compute the normalized output, which has the same shape as input, as following:.. math::  out[:,i,:,...] = \frac{data[:,i,:,...] - data\_mean[i]}{\sqrt{data\_var[i]+\epsilon}} * gamma[i] + beta[i]Both *mean* and *var* returns a scalar by treating the input as a vector.Assume the input has size *k* on axis 1, then both ``gamma`` and ``beta``have shape *(k,)*. If ``output_mean_var`` is set to be true, then outputs both ``data_mean`` and``data_var`` as well, which are needed for the backward pass.Besides the inputs and the outputs, this operator accepts two auxiliarystates, ``moving_mean`` and ``moving_var``, which are *k*-lengthvectors. They are global statistics for the whole dataset, which are updatedby::  moving_mean = moving_mean * momentum + data_mean * (1 - momentum)  moving_var = moving_var * momentum + data_var * (1 - momentum)If ``use_global_stats`` is set to be true, then ``moving_mean`` and``moving_var`` are used instead of ``data_mean`` and ``data_var`` to computethe output. It is often used during inference.The parameter ``axis`` specifies which axis of the input shape denotesthe 'channel' (separately normalized groups).  The default is 1.  Specifying -1 sets the channelaxis to be the last item in the input shape.Both ``gamma`` and ``beta`` are learnable parameters. But if ``fix_gamma`` is true,then set ``gamma`` to 1 and its gradient to 0.Defined in I:\workspace\incubator-mxnet\src\operator\nn\batch_norm.cc:L400
/// </summary>
/// <param name="data">Input data to batch normalization</param>
/// <param name="gamma">gamma array</param>
/// <param name="beta">beta array</param>
/// <param name="moving_mean">running mean of input</param>
/// <param name="moving_var">running variance of input</param>
/// <param name="eps">Epsilon to prevent div 0. Must be no less than CUDNN_BN_MIN_EPSILON defined in cudnn.h when using cudnn (usually 1e-5)</param>
/// <param name="momentum">Momentum for moving average</param>
/// <param name="fix_gamma">Fix gamma while training</param>
/// <param name="use_global_stats">Whether use global moving statistics instead of local batch-norm. This will force change batch-norm into a scale shift operator.</param>
/// <param name="output_mean_var">Output All,normal mean and var</param>
/// <param name="axis">Specify which shape axis the channel is specified</param>
/// <param name="cudnn_off">Do not select CUDNN operator, if available</param>
 /// <returns>returns new symbol</returns>
public static NdArray BatchNorm(NdArray data,
NdArray gamma,
NdArray beta,
NdArray moving_mean,
NdArray moving_var,
double eps=0.001,
float momentum=0.9f,
bool fix_gamma=true,
bool use_global_stats=false,
bool output_mean_var=false,
int axis=1,
bool cudnn_off=false)
{
return new Operator("BatchNorm")
.SetParam("eps", eps)
.SetParam("momentum", momentum)
.SetParam("fix_gamma", fix_gamma)
.SetParam("use_global_stats", use_global_stats)
.SetParam("output_mean_var", output_mean_var)
.SetParam("axis", axis)
.SetParam("cudnn_off", cudnn_off)
.SetInput("data", data)
.SetInput("gamma", gamma)
.SetInput("beta", beta)
.SetInput("moving_mean", moving_mean)
.SetInput("moving_var", moving_var)
.Invoke();
}
private static readonly List<string> UpsamplingSampleTypeConvert = new List<string>(){"bilinear","nearest"};
private static readonly List<string> UpsamplingMultiInputModeConvert = new List<string>(){"concat","sum"};
/// <summary>
/// Performs nearest neighbor/bilinear up sampling to inputs.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Array of tensors to upsample</param>
/// <param name="scale">Up sampling scale</param>
/// <param name="sample_type">upsampling method</param>
/// <param name="num_args">Number of inputs to be upsampled. For nearest neighbor upsampling, this can be 1-N; the size of output will be(scale*h_0,scale*w_0) and all other inputs will be upsampled to thesame size. For bilinear upsampling this must be 2; 1 input and 1 weight.</param>
/// <param name="num_filter">Input filter. Only used by bilinear sample_type.</param>
/// <param name="multi_input_mode">How to handle multiple input. concat means concatenate upsampled images along the channel dimension. sum means add all images together, only available for nearest neighbor upsampling.</param>
/// <param name="workspace">Tmp workspace for deconvolution (MB)</param>
 /// <returns>returns new symbol</returns>
public static NdArray UpSampling(NdArray @out,
NdArray[] data,
uint scale,
UpsamplingSampleType sample_type,
int num_args,
uint num_filter=0,
UpsamplingMultiInputMode multi_input_mode=UpsamplingMultiInputMode.Concat,
ulong workspace=512)
{
return new Operator("UpSampling")
.SetParam("scale", scale)
.SetParam("num_filter", num_filter)
.SetParam("sample_type", Util.EnumToString<UpsamplingSampleType>(sample_type,UpsamplingSampleTypeConvert))
.SetParam("multi_input_mode", Util.EnumToString<UpsamplingMultiInputMode>(multi_input_mode,UpsamplingMultiInputModeConvert))
.SetParam("num_args", num_args)
.SetParam("workspace", workspace)
.AddInput(data)
.Invoke(@out);
}
/// <summary>
/// Performs nearest neighbor/bilinear up sampling to inputs.
/// </summary>
/// <param name="data">Array of tensors to upsample</param>
/// <param name="scale">Up sampling scale</param>
/// <param name="sample_type">upsampling method</param>
/// <param name="num_args">Number of inputs to be upsampled. For nearest neighbor upsampling, this can be 1-N; the size of output will be(scale*h_0,scale*w_0) and all other inputs will be upsampled to thesame size. For bilinear upsampling this must be 2; 1 input and 1 weight.</param>
/// <param name="num_filter">Input filter. Only used by bilinear sample_type.</param>
/// <param name="multi_input_mode">How to handle multiple input. concat means concatenate upsampled images along the channel dimension. sum means add all images together, only available for nearest neighbor upsampling.</param>
/// <param name="workspace">Tmp workspace for deconvolution (MB)</param>
 /// <returns>returns new symbol</returns>
public static NdArray UpSampling(NdArray[] data,
uint scale,
UpsamplingSampleType sample_type,
int num_args,
uint num_filter=0,
UpsamplingMultiInputMode multi_input_mode=UpsamplingMultiInputMode.Concat,
ulong workspace=512)
{
return new Operator("UpSampling")
.SetParam("scale", scale)
.SetParam("num_filter", num_filter)
.SetParam("sample_type", Util.EnumToString<UpsamplingSampleType>(sample_type,UpsamplingSampleTypeConvert))
.SetParam("multi_input_mode", Util.EnumToString<UpsamplingMultiInputMode>(multi_input_mode,UpsamplingMultiInputModeConvert))
.SetParam("num_args", num_args)
.SetParam("workspace", workspace)
.AddInput(data)
.Invoke();
}
private static readonly List<string> PadModeConvert = new List<string>(){"constant","edge","reflect"};
/// <summary>
/// Pads an input array with a constant or edge values of the array... note:: `Pad` is deprecated. Use `pad` instead... note:: Current implementation only supports 4D and 5D input arrays with padding applied   only on axes 1, 2 and 3. Expects axes 4 and 5 in `pad_width` to be zero.This operation pads an input array with either a `constant_value` or edge valuesalong each axis of the input array. The amount of padding is specified by `pad_width`.`pad_width` is a tuple of integer padding widths for each axis of the format``(before_1, after_1, ... , before_N, after_N)``. The `pad_width` should be of length ``2*N``where ``N`` is the number of dimensions of the array.For dimension ``N`` of the input array, ``before_N`` and ``after_N`` indicates how many valuesto add before and after the elements of the array along dimension ``N``.The widths of the higher two dimensions ``before_1``, ``after_1``, ``before_2``,``after_2`` must be 0.Example::   x = [[[[  1.   2.   3.]          [  4.   5.   6.]]         [[  7.   8.   9.]          [ 10.  11.  12.]]]        [[[ 11.  12.  13.]          [ 14.  15.  16.]]         [[ 17.  18.  19.]          [ 20.  21.  22.]]]]   pad(x,mode="edge", pad_width=(0,0,0,0,1,1,1,1)) =         [[[[  1.   1.   2.   3.   3.]            [  1.   1.   2.   3.   3.]            [  4.   4.   5.   6.   6.]            [  4.   4.   5.   6.   6.]]           [[  7.   7.   8.   9.   9.]            [  7.   7.   8.   9.   9.]            [ 10.  10.  11.  12.  12.]            [ 10.  10.  11.  12.  12.]]]          [[[ 11.  11.  12.  13.  13.]            [ 11.  11.  12.  13.  13.]            [ 14.  14.  15.  16.  16.]            [ 14.  14.  15.  16.  16.]]           [[ 17.  17.  18.  19.  19.]            [ 17.  17.  18.  19.  19.]            [ 20.  20.  21.  22.  22.]            [ 20.  20.  21.  22.  22.]]]]   pad(x, mode="constant", constant_value=0, pad_width=(0,0,0,0,1,1,1,1)) =         [[[[  0.   0.   0.   0.   0.]            [  0.   1.   2.   3.   0.]            [  0.   4.   5.   6.   0.]            [  0.   0.   0.   0.   0.]]           [[  0.   0.   0.   0.   0.]            [  0.   7.   8.   9.   0.]            [  0.  10.  11.  12.   0.]            [  0.   0.   0.   0.   0.]]]          [[[  0.   0.   0.   0.   0.]            [  0.  11.  12.  13.   0.]            [  0.  14.  15.  16.   0.]            [  0.   0.   0.   0.   0.]]           [[  0.   0.   0.   0.   0.]            [  0.  17.  18.  19.   0.]            [  0.  20.  21.  22.   0.]            [  0.   0.   0.   0.   0.]]]]Defined in I:\workspace\incubator-mxnet\src\operator\pad.cc:L766
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">An n-dimensional input array.</param>
/// <param name="mode">Padding type to use. "constant" pads with `constant_value` "edge" pads using the edge values of the input array "reflect" pads by reflecting values with respect to the edges.</param>
/// <param name="pad_width">Widths of the padding regions applied to the edges of each axis. It is a tuple of integer padding widths for each axis of the format ``(before_1, after_1, ... , before_N, after_N)``. It should be of length ``2*N`` where ``N`` is the number of dimensions of the array.This is equivalent to pad_width in numpy.pad, but flattened.</param>
/// <param name="constant_value">The value used for padding when `mode` is "constant".</param>
 /// <returns>returns new symbol</returns>
public static NdArray Pad(NdArray @out,
NdArray data,
PadMode mode,
Shape pad_width,
double constant_value=0)
{
return new Operator("Pad")
.SetParam("mode", Util.EnumToString<PadMode>(mode,PadModeConvert))
.SetParam("pad_width", pad_width)
.SetParam("constant_value", constant_value)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Pads an input array with a constant or edge values of the array... note:: `Pad` is deprecated. Use `pad` instead... note:: Current implementation only supports 4D and 5D input arrays with padding applied   only on axes 1, 2 and 3. Expects axes 4 and 5 in `pad_width` to be zero.This operation pads an input array with either a `constant_value` or edge valuesalong each axis of the input array. The amount of padding is specified by `pad_width`.`pad_width` is a tuple of integer padding widths for each axis of the format``(before_1, after_1, ... , before_N, after_N)``. The `pad_width` should be of length ``2*N``where ``N`` is the number of dimensions of the array.For dimension ``N`` of the input array, ``before_N`` and ``after_N`` indicates how many valuesto add before and after the elements of the array along dimension ``N``.The widths of the higher two dimensions ``before_1``, ``after_1``, ``before_2``,``after_2`` must be 0.Example::   x = [[[[  1.   2.   3.]          [  4.   5.   6.]]         [[  7.   8.   9.]          [ 10.  11.  12.]]]        [[[ 11.  12.  13.]          [ 14.  15.  16.]]         [[ 17.  18.  19.]          [ 20.  21.  22.]]]]   pad(x,mode="edge", pad_width=(0,0,0,0,1,1,1,1)) =         [[[[  1.   1.   2.   3.   3.]            [  1.   1.   2.   3.   3.]            [  4.   4.   5.   6.   6.]            [  4.   4.   5.   6.   6.]]           [[  7.   7.   8.   9.   9.]            [  7.   7.   8.   9.   9.]            [ 10.  10.  11.  12.  12.]            [ 10.  10.  11.  12.  12.]]]          [[[ 11.  11.  12.  13.  13.]            [ 11.  11.  12.  13.  13.]            [ 14.  14.  15.  16.  16.]            [ 14.  14.  15.  16.  16.]]           [[ 17.  17.  18.  19.  19.]            [ 17.  17.  18.  19.  19.]            [ 20.  20.  21.  22.  22.]            [ 20.  20.  21.  22.  22.]]]]   pad(x, mode="constant", constant_value=0, pad_width=(0,0,0,0,1,1,1,1)) =         [[[[  0.   0.   0.   0.   0.]            [  0.   1.   2.   3.   0.]            [  0.   4.   5.   6.   0.]            [  0.   0.   0.   0.   0.]]           [[  0.   0.   0.   0.   0.]            [  0.   7.   8.   9.   0.]            [  0.  10.  11.  12.   0.]            [  0.   0.   0.   0.   0.]]]          [[[  0.   0.   0.   0.   0.]            [  0.  11.  12.  13.   0.]            [  0.  14.  15.  16.   0.]            [  0.   0.   0.   0.   0.]]           [[  0.   0.   0.   0.   0.]            [  0.  17.  18.  19.   0.]            [  0.  20.  21.  22.   0.]            [  0.   0.   0.   0.   0.]]]]Defined in I:\workspace\incubator-mxnet\src\operator\pad.cc:L766
/// </summary>
/// <param name="data">An n-dimensional input array.</param>
/// <param name="mode">Padding type to use. "constant" pads with `constant_value` "edge" pads using the edge values of the input array "reflect" pads by reflecting values with respect to the edges.</param>
/// <param name="pad_width">Widths of the padding regions applied to the edges of each axis. It is a tuple of integer padding widths for each axis of the format ``(before_1, after_1, ... , before_N, after_N)``. It should be of length ``2*N`` where ``N`` is the number of dimensions of the array.This is equivalent to pad_width in numpy.pad, but flattened.</param>
/// <param name="constant_value">The value used for padding when `mode` is "constant".</param>
 /// <returns>returns new symbol</returns>
public static NdArray Pad(NdArray data,
PadMode mode,
Shape pad_width,
double constant_value=0)
{
return new Operator("Pad")
.SetParam("mode", Util.EnumToString<PadMode>(mode,PadModeConvert))
.SetParam("pad_width", pad_width)
.SetParam("constant_value", constant_value)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Splits an array along a particular axis into multiple sub-arrays... note:: ``SliceChannel`` is deprecated. Use ``split`` instead.**Note** that `num_outputs` should evenly divide the length of the axisalong which to split the array.Example::   x  = [[[ 1.]          [ 2.]]         [[ 3.]          [ 4.]]         [[ 5.]          [ 6.]]]   x.shape = (3, 2, 1)   y = split(x, axis=1, num_outputs=2) // a list of 2 arrays with shape (3, 1, 1)   y = [[[ 1.]]        [[ 3.]]        [[ 5.]]]       [[[ 2.]]        [[ 4.]]        [[ 6.]]]   y[0].shape = (3, 1, 1)   z = split(x, axis=0, num_outputs=3) // a list of 3 arrays with shape (1, 2, 1)   z = [[[ 1.]         [ 2.]]]       [[[ 3.]         [ 4.]]]       [[[ 5.]         [ 6.]]]   z[0].shape = (1, 2, 1)`squeeze_axis=1` removes the axis with length 1 from the shapes of the output arrays.**Note** that setting `squeeze_axis` to ``1`` removes axis with length 1 onlyalong the `axis` which it is split.Also `squeeze_axis` can be set to true only if ``input.shape[axis] == num_outputs``.Example::   z = split(x, axis=0, num_outputs=3, squeeze_axis=1) // a list of 3 arrays with shape (2, 1)   z = [[ 1.]        [ 2.]]       [[ 3.]        [ 4.]]       [[ 5.]        [ 6.]]   z[0].shape = (2 ,1 )Defined in I:\workspace\incubator-mxnet\src\operator\slice_channel.cc:L107
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input</param>
/// <param name="num_outputs">Number of splits. Note that this should evenly divide the length of the `axis`.</param>
/// <param name="axis">Axis along which to split.</param>
/// <param name="squeeze_axis">If true, Removes the axis with length 1 from the shapes of the output arrays. **Note** that setting `squeeze_axis` to ``true`` removes axis with length 1 only along the `axis` which it is split. Also `squeeze_axis` can be set to ``true`` only if ``input.shape[axis] == num_outputs``.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SliceChannel(NdArray @out,
NdArray data,
int num_outputs,
int axis=1,
bool squeeze_axis=false)
{
return new Operator("SliceChannel")
.SetParam("num_outputs", num_outputs)
.SetParam("axis", axis)
.SetParam("squeeze_axis", squeeze_axis)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Splits an array along a particular axis into multiple sub-arrays... note:: ``SliceChannel`` is deprecated. Use ``split`` instead.**Note** that `num_outputs` should evenly divide the length of the axisalong which to split the array.Example::   x  = [[[ 1.]          [ 2.]]         [[ 3.]          [ 4.]]         [[ 5.]          [ 6.]]]   x.shape = (3, 2, 1)   y = split(x, axis=1, num_outputs=2) // a list of 2 arrays with shape (3, 1, 1)   y = [[[ 1.]]        [[ 3.]]        [[ 5.]]]       [[[ 2.]]        [[ 4.]]        [[ 6.]]]   y[0].shape = (3, 1, 1)   z = split(x, axis=0, num_outputs=3) // a list of 3 arrays with shape (1, 2, 1)   z = [[[ 1.]         [ 2.]]]       [[[ 3.]         [ 4.]]]       [[[ 5.]         [ 6.]]]   z[0].shape = (1, 2, 1)`squeeze_axis=1` removes the axis with length 1 from the shapes of the output arrays.**Note** that setting `squeeze_axis` to ``1`` removes axis with length 1 onlyalong the `axis` which it is split.Also `squeeze_axis` can be set to true only if ``input.shape[axis] == num_outputs``.Example::   z = split(x, axis=0, num_outputs=3, squeeze_axis=1) // a list of 3 arrays with shape (2, 1)   z = [[ 1.]        [ 2.]]       [[ 3.]        [ 4.]]       [[ 5.]        [ 6.]]   z[0].shape = (2 ,1 )Defined in I:\workspace\incubator-mxnet\src\operator\slice_channel.cc:L107
/// </summary>
/// <param name="data">The input</param>
/// <param name="num_outputs">Number of splits. Note that this should evenly divide the length of the `axis`.</param>
/// <param name="axis">Axis along which to split.</param>
/// <param name="squeeze_axis">If true, Removes the axis with length 1 from the shapes of the output arrays. **Note** that setting `squeeze_axis` to ``true`` removes axis with length 1 only along the `axis` which it is split. Also `squeeze_axis` can be set to ``true`` only if ``input.shape[axis] == num_outputs``.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SliceChannel(NdArray data,
int num_outputs,
int axis=1,
bool squeeze_axis=false)
{
return new Operator("SliceChannel")
.SetParam("num_outputs", num_outputs)
.SetParam("axis", axis)
.SetParam("squeeze_axis", squeeze_axis)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Interchanges two axes of an array.Examples::  x = [[1, 2, 3]])  swapaxes(x, 0, 1) = [[ 1],                       [ 2],                       [ 3]]  x = [[[ 0, 1],        [ 2, 3]],       [[ 4, 5],        [ 6, 7]]]  // (2,2,2) array swapaxes(x, 0, 2) = [[[ 0, 4],                       [ 2, 6]],                      [[ 1, 5],                       [ 3, 7]]]Defined in I:\workspace\incubator-mxnet\src\operator\swapaxis.cc:L70
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array.</param>
/// <param name="dim1">the first axis to be swapped.</param>
/// <param name="dim2">the second axis to be swapped.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SwapAxis(NdArray @out,
NdArray data,
uint dim1=0,
uint dim2=0)
{
return new Operator("SwapAxis")
.SetParam("dim1", dim1)
.SetParam("dim2", dim2)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Interchanges two axes of an array.Examples::  x = [[1, 2, 3]])  swapaxes(x, 0, 1) = [[ 1],                       [ 2],                       [ 3]]  x = [[[ 0, 1],        [ 2, 3]],       [[ 4, 5],        [ 6, 7]]]  // (2,2,2) array swapaxes(x, 0, 2) = [[[ 0, 4],                       [ 2, 6]],                      [[ 1, 5],                       [ 3, 7]]]Defined in I:\workspace\incubator-mxnet\src\operator\swapaxis.cc:L70
/// </summary>
/// <param name="data">Input array.</param>
/// <param name="dim1">the first axis to be swapped.</param>
/// <param name="dim2">the second axis to be swapped.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SwapAxis(NdArray data,
uint dim1=0,
uint dim2=0)
{
return new Operator("SwapAxis")
.SetParam("dim1", dim1)
.SetParam("dim2", dim2)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// Special op to copy data cross device
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray CrossDeviceCopy(NdArray @out)
{
return new Operator("_CrossDeviceCopy")
.Invoke(@out);
}
/// <summary>
/// Special op to copy data cross device
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray CrossDeviceCopy()
{
return new Operator("_CrossDeviceCopy")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCrossDeviceCopy(NdArray @out)
{
return new Operator("_backward__CrossDeviceCopy")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCrossDeviceCopy()
{
return new Operator("_backward__CrossDeviceCopy")
.Invoke();
}
/// <summary>
/// Stub for implementing an operator implemented in native frontend language with ndarray.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data for the custom operator.</param>
/// <param name="info"></param>
 /// <returns>returns new symbol</returns>
public static NdArray NDArray(NdArray @out,
NdArray[] data,
IntPtr info)
{
return new Operator("_NDArray")
.SetParam("info", info)
.AddInput(data)
.Invoke(@out);
}
/// <summary>
/// Stub for implementing an operator implemented in native frontend language with ndarray.
/// </summary>
/// <param name="data">Input data for the custom operator.</param>
/// <param name="info"></param>
 /// <returns>returns new symbol</returns>
public static NdArray NDArray(NdArray[] data,
IntPtr info)
{
return new Operator("_NDArray")
.SetParam("info", info)
.AddInput(data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardNDArray(NdArray @out)
{
return new Operator("_backward__NDArray")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardNDArray()
{
return new Operator("_backward__NDArray")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBatchNormV1(NdArray @out)
{
return new Operator("_backward_BatchNorm_v1")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBatchNormV1()
{
return new Operator("_backward_BatchNorm_v1")
.Invoke();
}
/// <summary>
/// Applies bilinear sampling to input feature map.Bilinear Sampling is the key of  [NIPS2015] \"Spatial Transformer Networks\". The usage of the operator is very similar to remap function in OpenCV,except that the operator has the backward pass.Given :math:`data` and :math:`grid`, then the output is computed by.. math::  x_{src} = grid[batch, 0, y_{dst}, x_{dst}] \\  y_{src} = grid[batch, 1, y_{dst}, x_{dst}] \\  output[batch, channel, y_{dst}, x_{dst}] = G(data[batch, channel, y_{src}, x_{src}):math:`x_{dst}`, :math:`y_{dst}` enumerate all spatial locations in :math:`output`, and :math:`G()` denotes the bilinear interpolation kernel.The out-boundary points will be padded with zeros.The shape of the output will be (data.shape[0], data.shape[1], grid.shape[2], grid.shape[3]).The operator assumes that :math:`data` has 'NCHW' layout and :math:`grid` has been normalized to [-1, 1].BilinearSampler often cooperates with GridGenerator which generates sampling grids for BilinearSampler.GridGenerator supports two kinds of transformation: ``affine`` and ``warp``.If users want to design a CustomOp to manipulate :math:`grid`, please firstly refer to the code of GridGenerator.Example 1::  ## Zoom out data two times  data = array([[[[1, 4, 3, 6],                  [1, 8, 8, 9],                  [0, 4, 1, 5],                  [1, 0, 1, 3]]]])  affine_matrix = array([[2, 0, 0],                         [0, 2, 0]])  affine_matrix = reshape(affine_matrix, shape=(1, 6))  grid = GridGenerator(data=affine_matrix, transform_type='affine', target_shape=(4, 4))  out = BilinearSampler(data, grid)  out  [[[[ 0,   0,     0,   0],     [ 0,   3.5,   6.5, 0],     [ 0,   1.25,  2.5, 0],     [ 0,   0,     0,   0]]]Example 2::  ## shift data horizontally by -1 pixel  data = array([[[[1, 4, 3, 6],                  [1, 8, 8, 9],                  [0, 4, 1, 5],                  [1, 0, 1, 3]]]])  warp_maxtrix = array([[[[1, 1, 1, 1],                          [1, 1, 1, 1],                          [1, 1, 1, 1],                          [1, 1, 1, 1]],                         [[0, 0, 0, 0],                          [0, 0, 0, 0],                          [0, 0, 0, 0],                          [0, 0, 0, 0]]]])  grid = GridGenerator(data=warp_matrix, transform_type='warp')  out = BilinearSampler(data, grid)  out  [[[[ 4,  3,  6,  0],     [ 8,  8,  9,  0],     [ 4,  1,  5,  0],     [ 0,  1,  3,  0]]]Defined in I:\workspace\incubator-mxnet\src\operator\bilinear_sampler.cc:L245
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the BilinearsamplerOp.</param>
/// <param name="grid">Input grid to the BilinearsamplerOp.grid has two channels: x_src, y_src</param>
 /// <returns>returns new symbol</returns>
public static NdArray BilinearSampler(NdArray @out,
NdArray data,
NdArray grid)
{
return new Operator("BilinearSampler")
.SetInput("data", data)
.SetInput("grid", grid)
.Invoke(@out);
}
/// <summary>
/// Applies bilinear sampling to input feature map.Bilinear Sampling is the key of  [NIPS2015] \"Spatial Transformer Networks\". The usage of the operator is very similar to remap function in OpenCV,except that the operator has the backward pass.Given :math:`data` and :math:`grid`, then the output is computed by.. math::  x_{src} = grid[batch, 0, y_{dst}, x_{dst}] \\  y_{src} = grid[batch, 1, y_{dst}, x_{dst}] \\  output[batch, channel, y_{dst}, x_{dst}] = G(data[batch, channel, y_{src}, x_{src}):math:`x_{dst}`, :math:`y_{dst}` enumerate all spatial locations in :math:`output`, and :math:`G()` denotes the bilinear interpolation kernel.The out-boundary points will be padded with zeros.The shape of the output will be (data.shape[0], data.shape[1], grid.shape[2], grid.shape[3]).The operator assumes that :math:`data` has 'NCHW' layout and :math:`grid` has been normalized to [-1, 1].BilinearSampler often cooperates with GridGenerator which generates sampling grids for BilinearSampler.GridGenerator supports two kinds of transformation: ``affine`` and ``warp``.If users want to design a CustomOp to manipulate :math:`grid`, please firstly refer to the code of GridGenerator.Example 1::  ## Zoom out data two times  data = array([[[[1, 4, 3, 6],                  [1, 8, 8, 9],                  [0, 4, 1, 5],                  [1, 0, 1, 3]]]])  affine_matrix = array([[2, 0, 0],                         [0, 2, 0]])  affine_matrix = reshape(affine_matrix, shape=(1, 6))  grid = GridGenerator(data=affine_matrix, transform_type='affine', target_shape=(4, 4))  out = BilinearSampler(data, grid)  out  [[[[ 0,   0,     0,   0],     [ 0,   3.5,   6.5, 0],     [ 0,   1.25,  2.5, 0],     [ 0,   0,     0,   0]]]Example 2::  ## shift data horizontally by -1 pixel  data = array([[[[1, 4, 3, 6],                  [1, 8, 8, 9],                  [0, 4, 1, 5],                  [1, 0, 1, 3]]]])  warp_maxtrix = array([[[[1, 1, 1, 1],                          [1, 1, 1, 1],                          [1, 1, 1, 1],                          [1, 1, 1, 1]],                         [[0, 0, 0, 0],                          [0, 0, 0, 0],                          [0, 0, 0, 0],                          [0, 0, 0, 0]]]])  grid = GridGenerator(data=warp_matrix, transform_type='warp')  out = BilinearSampler(data, grid)  out  [[[[ 4,  3,  6,  0],     [ 8,  8,  9,  0],     [ 4,  1,  5,  0],     [ 0,  1,  3,  0]]]Defined in I:\workspace\incubator-mxnet\src\operator\bilinear_sampler.cc:L245
/// </summary>
/// <param name="data">Input data to the BilinearsamplerOp.</param>
/// <param name="grid">Input grid to the BilinearsamplerOp.grid has two channels: x_src, y_src</param>
 /// <returns>returns new symbol</returns>
public static NdArray BilinearSampler(NdArray data,
NdArray grid)
{
return new Operator("BilinearSampler")
.SetInput("data", data)
.SetInput("grid", grid)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBilinearSampler(NdArray @out)
{
return new Operator("_backward_BilinearSampler")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBilinearSampler()
{
return new Operator("_backward_BilinearSampler")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardConcat(NdArray @out)
{
return new Operator("_backward_Concat")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardConcat()
{
return new Operator("_backward_Concat")
.Invoke();
}
/// <summary>
/// Apply CountSketch to input: map a d-dimension data to k-dimension data".. note:: `count_sketch` is only available on GPU.Assume input data has shape (N, d), sign hash table s has shape (N, d),index hash table h has shape (N, d) and mapping dimension out_dim = k,each element in s is either +1 or -1, each element in h is random integer from 0 to k-1.Then the operator computs:.. math::   out[h[i]] += data[i] * s[i]Example::   out_dim = 5   x = [[1.2, 2.5, 3.4],[3.2, 5.7, 6.6]]   h = [[0, 3, 4]]   s = [[1, -1, 1]]   mx.contrib.ndarray.count_sketch(data=x, h=h, s=s, out_dim = 5) = [[1.2, 0, 0, -2.5, 3.4],                                                                     [3.2, 0, 0, -5.7, 6.6]]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\count_sketch.cc:L67
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the CountSketchOp.</param>
/// <param name="h">The index vector</param>
/// <param name="s">The sign vector</param>
/// <param name="out_dim">The output dimension.</param>
/// <param name="processing_batch_size">How many sketch vectors to process at one time.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribCountSketch(NdArray @out,
NdArray data,
NdArray h,
NdArray s,
int out_dim,
int processing_batch_size=32)
{
return new Operator("_contrib_count_sketch")
.SetParam("out_dim", out_dim)
.SetParam("processing_batch_size", processing_batch_size)
.SetInput("data", data)
.SetInput("h", h)
.SetInput("s", s)
.Invoke(@out);
}
/// <summary>
/// Apply CountSketch to input: map a d-dimension data to k-dimension data".. note:: `count_sketch` is only available on GPU.Assume input data has shape (N, d), sign hash table s has shape (N, d),index hash table h has shape (N, d) and mapping dimension out_dim = k,each element in s is either +1 or -1, each element in h is random integer from 0 to k-1.Then the operator computs:.. math::   out[h[i]] += data[i] * s[i]Example::   out_dim = 5   x = [[1.2, 2.5, 3.4],[3.2, 5.7, 6.6]]   h = [[0, 3, 4]]   s = [[1, -1, 1]]   mx.contrib.ndarray.count_sketch(data=x, h=h, s=s, out_dim = 5) = [[1.2, 0, 0, -2.5, 3.4],                                                                     [3.2, 0, 0, -5.7, 6.6]]Defined in I:\workspace\incubator-mxnet\src\operator\contrib\count_sketch.cc:L67
/// </summary>
/// <param name="data">Input data to the CountSketchOp.</param>
/// <param name="h">The index vector</param>
/// <param name="s">The sign vector</param>
/// <param name="out_dim">The output dimension.</param>
/// <param name="processing_batch_size">How many sketch vectors to process at one time.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribCountSketch(NdArray data,
NdArray h,
NdArray s,
int out_dim,
int processing_batch_size=32)
{
return new Operator("_contrib_count_sketch")
.SetParam("out_dim", out_dim)
.SetParam("processing_batch_size", processing_batch_size)
.SetInput("data", data)
.SetInput("h", h)
.SetInput("s", s)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribCountSketch(NdArray @out)
{
return new Operator("_backward__contrib_count_sketch")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribCountSketch()
{
return new Operator("_backward__contrib_count_sketch")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribCTCLoss(NdArray @out)
{
return new Operator("_backward__contrib_CTCLoss")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribCTCLoss()
{
return new Operator("_backward__contrib_CTCLoss")
.Invoke();
}
private static readonly List<string> ContribDeformableconvolutionLayoutConvert = new List<string>(){"NCDHW","NCHW","NCW"};
/// <summary>
/// Compute 2-D deformable convolution on 4-D input.The deformable convolution operation is described in https://arxiv.org/abs/1703.06211For 2-D deformable convolution, the shapes are- **data**: *(batch_size, channel, height, width)*- **offset**: *(batch_size, num_deformable_group * kernel[0] * kernel[1], height, width)*- **weight**: *(num_filter, channel, kernel[0], kernel[1])*- **bias**: *(num_filter,)*- **out**: *(batch_size, num_filter, out_height, out_width)*.Define::  f(x,k,p,s,d) = floor((x+2*p-d*(k-1)-1)/s)+1then we have::  out_height=f(height, kernel[0], pad[0], stride[0], dilate[0])  out_width=f(width, kernel[1], pad[1], stride[1], dilate[1])If ``no_bias`` is set to be true, then the ``bias`` term is ignored.The default data ``layout`` is *NCHW*, namely *(batch_size, channle, height,width)*.If ``num_group`` is larger than 1, denoted by *g*, then split the input ``data``evenly into *g* parts along the channel axis, and also evenly split ``weight``along the first dimension. Next compute the convolution on the *i*-th part ofthe data with the *i*-th weight part. The output is obtained by concating allthe *g* results.If ``num_deformable_group`` is larger than 1, denoted by *dg*, then split theinput ``offset`` evenly into *dg* parts along the channel axis, and also evenlysplit ``out`` evenly into *dg* parts along the channel axis. Next compute thedeformable convolution, apply the *i*-th part of the offset part on the *i*-thout.Both ``weight`` and ``bias`` are learnable parameters.Defined in I:\workspace\incubator-mxnet\src\operator\contrib\deformable_convolution.cc:L100
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the DeformableConvolutionOp.</param>
/// <param name="offset">Input offset to the DeformableConvolutionOp.</param>
/// <param name="kernel">Convolution kernel size: (h, w) or (d, h, w)</param>
/// <param name="num_filter">Convolution filter(channel) number</param>
/// <param name="weight">Weight matrix.</param>
/// <param name="bias">Bias parameter.</param>
/// <param name="stride">Convolution stride: (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="dilate">Convolution dilate: (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="pad">Zero pad for convolution: (h, w) or (d, h, w). Defaults to no padding.</param>
/// <param name="num_group">Number of group partitions.</param>
/// <param name="num_deformable_group">Number of deformable group partitions.</param>
/// <param name="workspace">Maximum temperal workspace allowed for convolution (MB).</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="layout">Set layout for input, output and weight. Empty for    default layout: NCW for 1d, NCHW for 2d and NCDHW for 3d.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribDeformableConvolution(NdArray @out,
NdArray data,
NdArray offset,
Shape kernel,
uint num_filter,
NdArray weight=null,
NdArray bias=null,
Shape stride=null,
Shape dilate=null,
Shape pad=null,
uint num_group=1,
uint num_deformable_group=1,
ulong workspace=1024,
bool no_bias=false,
ContribDeformableconvolutionLayout? layout=null)
{
return new Operator("_contrib_DeformableConvolution")
.SetParam("kernel", kernel)
.SetParam("stride", stride)
.SetParam("dilate", dilate)
.SetParam("pad", pad)
.SetParam("num_filter", num_filter)
.SetParam("num_group", num_group)
.SetParam("num_deformable_group", num_deformable_group)
.SetParam("workspace", workspace)
.SetParam("no_bias", no_bias)
.SetParam("layout", Util.EnumToString<ContribDeformableconvolutionLayout>(layout,ContribDeformableconvolutionLayoutConvert))
.SetInput("data", data)
.SetInput("offset", offset)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke(@out);
}
/// <summary>
/// Compute 2-D deformable convolution on 4-D input.The deformable convolution operation is described in https://arxiv.org/abs/1703.06211For 2-D deformable convolution, the shapes are- **data**: *(batch_size, channel, height, width)*- **offset**: *(batch_size, num_deformable_group * kernel[0] * kernel[1], height, width)*- **weight**: *(num_filter, channel, kernel[0], kernel[1])*- **bias**: *(num_filter,)*- **out**: *(batch_size, num_filter, out_height, out_width)*.Define::  f(x,k,p,s,d) = floor((x+2*p-d*(k-1)-1)/s)+1then we have::  out_height=f(height, kernel[0], pad[0], stride[0], dilate[0])  out_width=f(width, kernel[1], pad[1], stride[1], dilate[1])If ``no_bias`` is set to be true, then the ``bias`` term is ignored.The default data ``layout`` is *NCHW*, namely *(batch_size, channle, height,width)*.If ``num_group`` is larger than 1, denoted by *g*, then split the input ``data``evenly into *g* parts along the channel axis, and also evenly split ``weight``along the first dimension. Next compute the convolution on the *i*-th part ofthe data with the *i*-th weight part. The output is obtained by concating allthe *g* results.If ``num_deformable_group`` is larger than 1, denoted by *dg*, then split theinput ``offset`` evenly into *dg* parts along the channel axis, and also evenlysplit ``out`` evenly into *dg* parts along the channel axis. Next compute thedeformable convolution, apply the *i*-th part of the offset part on the *i*-thout.Both ``weight`` and ``bias`` are learnable parameters.Defined in I:\workspace\incubator-mxnet\src\operator\contrib\deformable_convolution.cc:L100
/// </summary>
/// <param name="data">Input data to the DeformableConvolutionOp.</param>
/// <param name="offset">Input offset to the DeformableConvolutionOp.</param>
/// <param name="kernel">Convolution kernel size: (h, w) or (d, h, w)</param>
/// <param name="num_filter">Convolution filter(channel) number</param>
/// <param name="weight">Weight matrix.</param>
/// <param name="bias">Bias parameter.</param>
/// <param name="stride">Convolution stride: (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="dilate">Convolution dilate: (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="pad">Zero pad for convolution: (h, w) or (d, h, w). Defaults to no padding.</param>
/// <param name="num_group">Number of group partitions.</param>
/// <param name="num_deformable_group">Number of deformable group partitions.</param>
/// <param name="workspace">Maximum temperal workspace allowed for convolution (MB).</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="layout">Set layout for input, output and weight. Empty for    default layout: NCW for 1d, NCHW for 2d and NCDHW for 3d.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribDeformableConvolution(NdArray data,
NdArray offset,
Shape kernel,
uint num_filter,
NdArray weight=null,
NdArray bias=null,
Shape stride=null,
Shape dilate=null,
Shape pad=null,
uint num_group=1,
uint num_deformable_group=1,
ulong workspace=1024,
bool no_bias=false,
ContribDeformableconvolutionLayout? layout=null)
{
return new Operator("_contrib_DeformableConvolution")
.SetParam("kernel", kernel)
.SetParam("stride", stride)
.SetParam("dilate", dilate)
.SetParam("pad", pad)
.SetParam("num_filter", num_filter)
.SetParam("num_group", num_group)
.SetParam("num_deformable_group", num_deformable_group)
.SetParam("workspace", workspace)
.SetParam("no_bias", no_bias)
.SetParam("layout", Util.EnumToString<ContribDeformableconvolutionLayout>(layout,ContribDeformableconvolutionLayoutConvert))
.SetInput("data", data)
.SetInput("offset", offset)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribDeformableConvolution(NdArray @out)
{
return new Operator("_backward__contrib_DeformableConvolution")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribDeformableConvolution()
{
return new Operator("_backward__contrib_DeformableConvolution")
.Invoke();
}
/// <summary>
/// Performs deformable position-sensitive region-of-interest pooling on inputs.The DeformablePSROIPooling operation is described in https://arxiv.org/abs/1703.06211 .batch_size will change to the number of region bounding boxes after DeformablePSROIPooling
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the pooling operator, a 4D Feature maps</param>
/// <param name="rois">Bounding box coordinates, a 2D array of [[batch_index, x1, y1, x2, y2]]. (x1, y1) and (x2, y2) are top left and down right corners of designated region of interest. batch_index indicates the index of corresponding image in the input data</param>
/// <param name="trans">transition parameter</param>
/// <param name="spatial_scale">Ratio of input feature map height (or w) to raw image height (or w). Equals the reciprocal of total stride in convolutional layers</param>
/// <param name="output_dim">fix output dim</param>
/// <param name="group_size">fix group size</param>
/// <param name="pooled_size">fix pooled size</param>
/// <param name="part_size">fix part size</param>
/// <param name="sample_per_part">fix samples per part</param>
/// <param name="trans_std">fix transition std</param>
/// <param name="no_trans">Whether to disable trans parameter.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribDeformablePSROIPooling(NdArray @out,
Symbol data,
Symbol rois,
Symbol trans,
float spatial_scale,
int output_dim,
int group_size,
int pooled_size,
int part_size=0,
int sample_per_part=1,
float trans_std=0f,
bool no_trans=false)
{
return new Operator("_contrib_DeformablePSROIPooling")
.SetParam("data", data)
.SetParam("rois", rois)
.SetParam("trans", trans)
.SetParam("spatial_scale", spatial_scale)
.SetParam("output_dim", output_dim)
.SetParam("group_size", group_size)
.SetParam("pooled_size", pooled_size)
.SetParam("part_size", part_size)
.SetParam("sample_per_part", sample_per_part)
.SetParam("trans_std", trans_std)
.SetParam("no_trans", no_trans)
.Invoke(@out);
}
/// <summary>
/// Performs deformable position-sensitive region-of-interest pooling on inputs.The DeformablePSROIPooling operation is described in https://arxiv.org/abs/1703.06211 .batch_size will change to the number of region bounding boxes after DeformablePSROIPooling
/// </summary>
/// <param name="data">Input data to the pooling operator, a 4D Feature maps</param>
/// <param name="rois">Bounding box coordinates, a 2D array of [[batch_index, x1, y1, x2, y2]]. (x1, y1) and (x2, y2) are top left and down right corners of designated region of interest. batch_index indicates the index of corresponding image in the input data</param>
/// <param name="trans">transition parameter</param>
/// <param name="spatial_scale">Ratio of input feature map height (or w) to raw image height (or w). Equals the reciprocal of total stride in convolutional layers</param>
/// <param name="output_dim">fix output dim</param>
/// <param name="group_size">fix group size</param>
/// <param name="pooled_size">fix pooled size</param>
/// <param name="part_size">fix part size</param>
/// <param name="sample_per_part">fix samples per part</param>
/// <param name="trans_std">fix transition std</param>
/// <param name="no_trans">Whether to disable trans parameter.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribDeformablePSROIPooling(Symbol data,
Symbol rois,
Symbol trans,
float spatial_scale,
int output_dim,
int group_size,
int pooled_size,
int part_size=0,
int sample_per_part=1,
float trans_std=0f,
bool no_trans=false)
{
return new Operator("_contrib_DeformablePSROIPooling")
.SetParam("data", data)
.SetParam("rois", rois)
.SetParam("trans", trans)
.SetParam("spatial_scale", spatial_scale)
.SetParam("output_dim", output_dim)
.SetParam("group_size", group_size)
.SetParam("pooled_size", pooled_size)
.SetParam("part_size", part_size)
.SetParam("sample_per_part", sample_per_part)
.SetParam("trans_std", trans_std)
.SetParam("no_trans", no_trans)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribDeformablePSROIPooling(NdArray @out)
{
return new Operator("_backward__contrib_DeformablePSROIPooling")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribDeformablePSROIPooling()
{
return new Operator("_backward__contrib_DeformablePSROIPooling")
.Invoke();
}
/// <summary>
/// Apply 1D FFT to input".. note:: `fft` is only available on GPU.Currently accept 2 input data shapes: (N, d) or (N1, N2, N3, d), data can only be real numbers.The output data has shape: (N, 2*d) or (N1, N2, N3, 2*d). The format is: [real0, imag0, real1, imag1, ...].Example::   data = np.random.normal(0,1,(3,4))   out = mx.contrib.ndarray.fft(data = mx.nd.array(data,ctx = mx.gpu(0)))Defined in I:\workspace\incubator-mxnet\src\operator\contrib\fft.cc:L56
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the FFTOp.</param>
/// <param name="compute_size">Maximum size of sub-batch to be forwarded at one time</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribFft(NdArray @out,
NdArray data,
int compute_size=128)
{
return new Operator("_contrib_fft")
.SetParam("compute_size", compute_size)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Apply 1D FFT to input".. note:: `fft` is only available on GPU.Currently accept 2 input data shapes: (N, d) or (N1, N2, N3, d), data can only be real numbers.The output data has shape: (N, 2*d) or (N1, N2, N3, 2*d). The format is: [real0, imag0, real1, imag1, ...].Example::   data = np.random.normal(0,1,(3,4))   out = mx.contrib.ndarray.fft(data = mx.nd.array(data,ctx = mx.gpu(0)))Defined in I:\workspace\incubator-mxnet\src\operator\contrib\fft.cc:L56
/// </summary>
/// <param name="data">Input data to the FFTOp.</param>
/// <param name="compute_size">Maximum size of sub-batch to be forwarded at one time</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribFft(NdArray data,
int compute_size=128)
{
return new Operator("_contrib_fft")
.SetParam("compute_size", compute_size)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribFft(NdArray @out)
{
return new Operator("_backward__contrib_fft")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribFft()
{
return new Operator("_backward__contrib_fft")
.Invoke();
}
/// <summary>
/// Apply 1D ifft to input".. note:: `ifft` is only available on GPU.Currently accept 2 input data shapes: (N, d) or (N1, N2, N3, d). Data is in format: [real0, imag0, real1, imag1, ...].Last dimension must be an even number.The output data has shape: (N, d/2) or (N1, N2, N3, d/2). It is only the real part of the result.Example::   data = np.random.normal(0,1,(3,4))   out = mx.contrib.ndarray.ifft(data = mx.nd.array(data,ctx = mx.gpu(0)))Defined in I:\workspace\incubator-mxnet\src\operator\contrib\ifft.cc:L58
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the IFFTOp.</param>
/// <param name="compute_size">Maximum size of sub-batch to be forwarded at one time</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribIfft(NdArray @out,
NdArray data,
int compute_size=128)
{
return new Operator("_contrib_ifft")
.SetParam("compute_size", compute_size)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Apply 1D ifft to input".. note:: `ifft` is only available on GPU.Currently accept 2 input data shapes: (N, d) or (N1, N2, N3, d). Data is in format: [real0, imag0, real1, imag1, ...].Last dimension must be an even number.The output data has shape: (N, d/2) or (N1, N2, N3, d/2). It is only the real part of the result.Example::   data = np.random.normal(0,1,(3,4))   out = mx.contrib.ndarray.ifft(data = mx.nd.array(data,ctx = mx.gpu(0)))Defined in I:\workspace\incubator-mxnet\src\operator\contrib\ifft.cc:L58
/// </summary>
/// <param name="data">Input data to the IFFTOp.</param>
/// <param name="compute_size">Maximum size of sub-batch to be forwarded at one time</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribIfft(NdArray data,
int compute_size=128)
{
return new Operator("_contrib_ifft")
.SetParam("compute_size", compute_size)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribIfft(NdArray @out)
{
return new Operator("_backward__contrib_ifft")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribIfft()
{
return new Operator("_backward__contrib_ifft")
.Invoke();
}
/// <summary>
/// Generate region proposals via RPN
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="cls_score">Score of how likely proposal is object.</param>
/// <param name="bbox_pred">BBox Predicted deltas from anchors for proposals</param>
/// <param name="im_info">Image size and scale.</param>
/// <param name="rpn_pre_nms_top_n">Number of top scoring boxes to keep after applying NMS to RPN proposals</param>
/// <param name="rpn_post_nms_top_n">Overlap threshold used for non-maximumsuppresion(suppress boxes with IoU >= this threshold</param>
/// <param name="threshold">NMS value, below which to suppress.</param>
/// <param name="rpn_min_size">Minimum height or width in proposal</param>
/// <param name="scales">Used to generate anchor windows by enumerating scales</param>
/// <param name="ratios">Used to generate anchor windows by enumerating ratios</param>
/// <param name="feature_stride">The size of the receptive field each unit in the convolution layer of the rpn,for example the product of all stride's prior to this layer.</param>
/// <param name="output_score">Add score to outputs</param>
/// <param name="iou_loss">Usage of IoU Loss</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribMultiProposal(NdArray @out,
NdArray cls_score,
NdArray bbox_pred,
NdArray im_info,
int rpn_pre_nms_top_n=6000,
int rpn_post_nms_top_n=300,
float threshold=0.7f,
int rpn_min_size=16,
object[] scales=null,
object[] ratios=null,
int feature_stride=16,
bool output_score=false,
bool iou_loss=false)
{
return new Operator("_contrib_MultiProposal")
.SetParam("rpn_pre_nms_top_n", rpn_pre_nms_top_n)
.SetParam("rpn_post_nms_top_n", rpn_post_nms_top_n)
.SetParam("threshold", threshold)
.SetParam("rpn_min_size", rpn_min_size)
.SetParam("scales", scales)
.SetParam("ratios", ratios)
.SetParam("feature_stride", feature_stride)
.SetParam("output_score", output_score)
.SetParam("iou_loss", iou_loss)
.SetInput("cls_score", cls_score)
.SetInput("bbox_pred", bbox_pred)
.SetInput("im_info", im_info)
.Invoke(@out);
}
/// <summary>
/// Generate region proposals via RPN
/// </summary>
/// <param name="cls_score">Score of how likely proposal is object.</param>
/// <param name="bbox_pred">BBox Predicted deltas from anchors for proposals</param>
/// <param name="im_info">Image size and scale.</param>
/// <param name="rpn_pre_nms_top_n">Number of top scoring boxes to keep after applying NMS to RPN proposals</param>
/// <param name="rpn_post_nms_top_n">Overlap threshold used for non-maximumsuppresion(suppress boxes with IoU >= this threshold</param>
/// <param name="threshold">NMS value, below which to suppress.</param>
/// <param name="rpn_min_size">Minimum height or width in proposal</param>
/// <param name="scales">Used to generate anchor windows by enumerating scales</param>
/// <param name="ratios">Used to generate anchor windows by enumerating ratios</param>
/// <param name="feature_stride">The size of the receptive field each unit in the convolution layer of the rpn,for example the product of all stride's prior to this layer.</param>
/// <param name="output_score">Add score to outputs</param>
/// <param name="iou_loss">Usage of IoU Loss</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribMultiProposal(NdArray cls_score,
NdArray bbox_pred,
NdArray im_info,
int rpn_pre_nms_top_n=6000,
int rpn_post_nms_top_n=300,
float threshold=0.7f,
int rpn_min_size=16,
object[] scales=null,
object[] ratios=null,
int feature_stride=16,
bool output_score=false,
bool iou_loss=false)
{
return new Operator("_contrib_MultiProposal")
.SetParam("rpn_pre_nms_top_n", rpn_pre_nms_top_n)
.SetParam("rpn_post_nms_top_n", rpn_post_nms_top_n)
.SetParam("threshold", threshold)
.SetParam("rpn_min_size", rpn_min_size)
.SetParam("scales", scales)
.SetParam("ratios", ratios)
.SetParam("feature_stride", feature_stride)
.SetParam("output_score", output_score)
.SetParam("iou_loss", iou_loss)
.SetInput("cls_score", cls_score)
.SetInput("bbox_pred", bbox_pred)
.SetInput("im_info", im_info)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribMultiProposal(NdArray @out)
{
return new Operator("_backward__contrib_MultiProposal")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribMultiProposal()
{
return new Operator("_backward__contrib_MultiProposal")
.Invoke();
}
/// <summary>
/// Convert multibox detection predictions.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="cls_prob">Class probabilities.</param>
/// <param name="loc_pred">Location regression predictions.</param>
/// <param name="anchor">Multibox prior anchor boxes</param>
/// <param name="clip">Clip out-of-boundary boxes.</param>
/// <param name="threshold">Threshold to be a positive prediction.</param>
/// <param name="background_id">Background id.</param>
/// <param name="nms_threshold">Non-maximum suppression threshold.</param>
/// <param name="force_suppress">Suppress all detections regardless of class_id.</param>
/// <param name="variances">Variances to be decoded from box regression output.</param>
/// <param name="nms_topk">Keep maximum top k detections before nms, -1 for no limit.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribMultiBoxDetection(NdArray @out,
NdArray cls_prob,
NdArray loc_pred,
NdArray anchor,
bool clip=true,
float threshold=0.01f,
int background_id=0,
float nms_threshold=0.5f,
bool force_suppress=false,
object[] variances=null,
int nms_topk=-1)
{
return new Operator("_contrib_MultiBoxDetection")
.SetParam("clip", clip)
.SetParam("threshold", threshold)
.SetParam("background_id", background_id)
.SetParam("nms_threshold", nms_threshold)
.SetParam("force_suppress", force_suppress)
.SetParam("variances", variances)
.SetParam("nms_topk", nms_topk)
.SetInput("cls_prob", cls_prob)
.SetInput("loc_pred", loc_pred)
.SetInput("anchor", anchor)
.Invoke(@out);
}
/// <summary>
/// Convert multibox detection predictions.
/// </summary>
/// <param name="cls_prob">Class probabilities.</param>
/// <param name="loc_pred">Location regression predictions.</param>
/// <param name="anchor">Multibox prior anchor boxes</param>
/// <param name="clip">Clip out-of-boundary boxes.</param>
/// <param name="threshold">Threshold to be a positive prediction.</param>
/// <param name="background_id">Background id.</param>
/// <param name="nms_threshold">Non-maximum suppression threshold.</param>
/// <param name="force_suppress">Suppress all detections regardless of class_id.</param>
/// <param name="variances">Variances to be decoded from box regression output.</param>
/// <param name="nms_topk">Keep maximum top k detections before nms, -1 for no limit.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribMultiBoxDetection(NdArray cls_prob,
NdArray loc_pred,
NdArray anchor,
bool clip=true,
float threshold=0.01f,
int background_id=0,
float nms_threshold=0.5f,
bool force_suppress=false,
object[] variances=null,
int nms_topk=-1)
{
return new Operator("_contrib_MultiBoxDetection")
.SetParam("clip", clip)
.SetParam("threshold", threshold)
.SetParam("background_id", background_id)
.SetParam("nms_threshold", nms_threshold)
.SetParam("force_suppress", force_suppress)
.SetParam("variances", variances)
.SetParam("nms_topk", nms_topk)
.SetInput("cls_prob", cls_prob)
.SetInput("loc_pred", loc_pred)
.SetInput("anchor", anchor)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribMultiBoxDetection(NdArray @out)
{
return new Operator("_backward__contrib_MultiBoxDetection")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribMultiBoxDetection()
{
return new Operator("_backward__contrib_MultiBoxDetection")
.Invoke();
}
/// <summary>
/// Generate prior(anchor) boxes from data, sizes and ratios.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data.</param>
/// <param name="sizes">List of sizes of generated MultiBoxPriores.</param>
/// <param name="ratios">List of aspect ratios of generated MultiBoxPriores.</param>
/// <param name="clip">Whether to clip out-of-boundary boxes.</param>
/// <param name="steps">Priorbox step across y and x, -1 for auto calculation.</param>
/// <param name="offsets">Priorbox center offsets, y and x respectively</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribMultiBoxPrior(NdArray @out,
NdArray data,
object[] sizes=null,
object[] ratios=null,
bool clip=false,
object[] steps=null,
object[] offsets=null)
{
return new Operator("_contrib_MultiBoxPrior")
.SetParam("sizes", sizes)
.SetParam("ratios", ratios)
.SetParam("clip", clip)
.SetParam("steps", steps)
.SetParam("offsets", offsets)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Generate prior(anchor) boxes from data, sizes and ratios.
/// </summary>
/// <param name="data">Input data.</param>
/// <param name="sizes">List of sizes of generated MultiBoxPriores.</param>
/// <param name="ratios">List of aspect ratios of generated MultiBoxPriores.</param>
/// <param name="clip">Whether to clip out-of-boundary boxes.</param>
/// <param name="steps">Priorbox step across y and x, -1 for auto calculation.</param>
/// <param name="offsets">Priorbox center offsets, y and x respectively</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribMultiBoxPrior(NdArray data,
object[] sizes=null,
object[] ratios=null,
bool clip=false,
object[] steps=null,
object[] offsets=null)
{
return new Operator("_contrib_MultiBoxPrior")
.SetParam("sizes", sizes)
.SetParam("ratios", ratios)
.SetParam("clip", clip)
.SetParam("steps", steps)
.SetParam("offsets", offsets)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribMultiBoxPrior(NdArray @out)
{
return new Operator("_backward__contrib_MultiBoxPrior")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribMultiBoxPrior()
{
return new Operator("_backward__contrib_MultiBoxPrior")
.Invoke();
}
/// <summary>
/// Compute Multibox training targets
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="anchor">Generated anchor boxes.</param>
/// <param name="label">Object detection labels.</param>
/// <param name="cls_pred">Class predictions.</param>
/// <param name="overlap_threshold">Anchor-GT overlap threshold to be regarded as a positive match.</param>
/// <param name="ignore_label">Label for ignored anchors.</param>
/// <param name="negative_mining_ratio">Max negative to positive samples ratio, use -1 to disable mining</param>
/// <param name="negative_mining_thresh">Threshold used for negative mining.</param>
/// <param name="minimum_negative_samples">Minimum number of negative samples.</param>
/// <param name="variances">Variances to be encoded in box regression target.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribMultiBoxTarget(NdArray @out,
NdArray anchor,
NdArray label,
NdArray cls_pred,
float overlap_threshold=0.5f,
float ignore_label=-1f,
float negative_mining_ratio=-1f,
float negative_mining_thresh=0.5f,
int minimum_negative_samples=0,
object[] variances=null)
{
return new Operator("_contrib_MultiBoxTarget")
.SetParam("overlap_threshold", overlap_threshold)
.SetParam("ignore_label", ignore_label)
.SetParam("negative_mining_ratio", negative_mining_ratio)
.SetParam("negative_mining_thresh", negative_mining_thresh)
.SetParam("minimum_negative_samples", minimum_negative_samples)
.SetParam("variances", variances)
.SetInput("anchor", anchor)
.SetInput("label", label)
.SetInput("cls_pred", cls_pred)
.Invoke(@out);
}
/// <summary>
/// Compute Multibox training targets
/// </summary>
/// <param name="anchor">Generated anchor boxes.</param>
/// <param name="label">Object detection labels.</param>
/// <param name="cls_pred">Class predictions.</param>
/// <param name="overlap_threshold">Anchor-GT overlap threshold to be regarded as a positive match.</param>
/// <param name="ignore_label">Label for ignored anchors.</param>
/// <param name="negative_mining_ratio">Max negative to positive samples ratio, use -1 to disable mining</param>
/// <param name="negative_mining_thresh">Threshold used for negative mining.</param>
/// <param name="minimum_negative_samples">Minimum number of negative samples.</param>
/// <param name="variances">Variances to be encoded in box regression target.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribMultiBoxTarget(NdArray anchor,
NdArray label,
NdArray cls_pred,
float overlap_threshold=0.5f,
float ignore_label=-1f,
float negative_mining_ratio=-1f,
float negative_mining_thresh=0.5f,
int minimum_negative_samples=0,
object[] variances=null)
{
return new Operator("_contrib_MultiBoxTarget")
.SetParam("overlap_threshold", overlap_threshold)
.SetParam("ignore_label", ignore_label)
.SetParam("negative_mining_ratio", negative_mining_ratio)
.SetParam("negative_mining_thresh", negative_mining_thresh)
.SetParam("minimum_negative_samples", minimum_negative_samples)
.SetParam("variances", variances)
.SetInput("anchor", anchor)
.SetInput("label", label)
.SetInput("cls_pred", cls_pred)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribMultiBoxTarget(NdArray @out)
{
return new Operator("_backward__contrib_MultiBoxTarget")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribMultiBoxTarget()
{
return new Operator("_backward__contrib_MultiBoxTarget")
.Invoke();
}
/// <summary>
/// Generate region proposals via RPN
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="cls_score">Score of how likely proposal is object.</param>
/// <param name="bbox_pred">BBox Predicted deltas from anchors for proposals</param>
/// <param name="im_info">Image size and scale.</param>
/// <param name="rpn_pre_nms_top_n">Number of top scoring boxes to keep after applying NMS to RPN proposals</param>
/// <param name="rpn_post_nms_top_n">Overlap threshold used for non-maximumsuppresion(suppress boxes with IoU >= this threshold</param>
/// <param name="threshold">NMS value, below which to suppress.</param>
/// <param name="rpn_min_size">Minimum height or width in proposal</param>
/// <param name="scales">Used to generate anchor windows by enumerating scales</param>
/// <param name="ratios">Used to generate anchor windows by enumerating ratios</param>
/// <param name="feature_stride">The size of the receptive field each unit in the convolution layer of the rpn,for example the product of all stride's prior to this layer.</param>
/// <param name="output_score">Add score to outputs</param>
/// <param name="iou_loss">Usage of IoU Loss</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribProposal(NdArray @out,
NdArray cls_score,
NdArray bbox_pred,
NdArray im_info,
int rpn_pre_nms_top_n=6000,
int rpn_post_nms_top_n=300,
float threshold=0.7f,
int rpn_min_size=16,
object[] scales=null,
object[] ratios=null,
int feature_stride=16,
bool output_score=false,
bool iou_loss=false)
{
return new Operator("_contrib_Proposal")
.SetParam("rpn_pre_nms_top_n", rpn_pre_nms_top_n)
.SetParam("rpn_post_nms_top_n", rpn_post_nms_top_n)
.SetParam("threshold", threshold)
.SetParam("rpn_min_size", rpn_min_size)
.SetParam("scales", scales)
.SetParam("ratios", ratios)
.SetParam("feature_stride", feature_stride)
.SetParam("output_score", output_score)
.SetParam("iou_loss", iou_loss)
.SetInput("cls_score", cls_score)
.SetInput("bbox_pred", bbox_pred)
.SetInput("im_info", im_info)
.Invoke(@out);
}
/// <summary>
/// Generate region proposals via RPN
/// </summary>
/// <param name="cls_score">Score of how likely proposal is object.</param>
/// <param name="bbox_pred">BBox Predicted deltas from anchors for proposals</param>
/// <param name="im_info">Image size and scale.</param>
/// <param name="rpn_pre_nms_top_n">Number of top scoring boxes to keep after applying NMS to RPN proposals</param>
/// <param name="rpn_post_nms_top_n">Overlap threshold used for non-maximumsuppresion(suppress boxes with IoU >= this threshold</param>
/// <param name="threshold">NMS value, below which to suppress.</param>
/// <param name="rpn_min_size">Minimum height or width in proposal</param>
/// <param name="scales">Used to generate anchor windows by enumerating scales</param>
/// <param name="ratios">Used to generate anchor windows by enumerating ratios</param>
/// <param name="feature_stride">The size of the receptive field each unit in the convolution layer of the rpn,for example the product of all stride's prior to this layer.</param>
/// <param name="output_score">Add score to outputs</param>
/// <param name="iou_loss">Usage of IoU Loss</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribProposal(NdArray cls_score,
NdArray bbox_pred,
NdArray im_info,
int rpn_pre_nms_top_n=6000,
int rpn_post_nms_top_n=300,
float threshold=0.7f,
int rpn_min_size=16,
object[] scales=null,
object[] ratios=null,
int feature_stride=16,
bool output_score=false,
bool iou_loss=false)
{
return new Operator("_contrib_Proposal")
.SetParam("rpn_pre_nms_top_n", rpn_pre_nms_top_n)
.SetParam("rpn_post_nms_top_n", rpn_post_nms_top_n)
.SetParam("threshold", threshold)
.SetParam("rpn_min_size", rpn_min_size)
.SetParam("scales", scales)
.SetParam("ratios", ratios)
.SetParam("feature_stride", feature_stride)
.SetParam("output_score", output_score)
.SetParam("iou_loss", iou_loss)
.SetInput("cls_score", cls_score)
.SetInput("bbox_pred", bbox_pred)
.SetInput("im_info", im_info)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribProposal(NdArray @out)
{
return new Operator("_backward__contrib_Proposal")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribProposal()
{
return new Operator("_backward__contrib_Proposal")
.Invoke();
}
/// <summary>
/// Performs region-of-interest pooling on inputs. Resize bounding box coordinates by spatial_scale and crop input feature maps accordingly. The cropped feature maps are pooled by max pooling to a fixed size output indicated by pooled_size. batch_size will change to the number of region bounding boxes after PSROIPooling
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the pooling operator, a 4D Feature maps</param>
/// <param name="rois">Bounding box coordinates, a 2D array of [[batch_index, x1, y1, x2, y2]]. (x1, y1) and (x2, y2) are top left and down right corners of designated region of interest. batch_index indicates the index of corresponding image in the input data</param>
/// <param name="spatial_scale">Ratio of input feature map height (or w) to raw image height (or w). Equals the reciprocal of total stride in convolutional layers</param>
/// <param name="output_dim">fix output dim</param>
/// <param name="pooled_size">fix pooled size</param>
/// <param name="group_size">fix group size</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribPSROIPooling(NdArray @out,
Symbol data,
Symbol rois,
float spatial_scale,
int output_dim,
int pooled_size,
int group_size=0)
{
return new Operator("_contrib_PSROIPooling")
.SetParam("data", data)
.SetParam("rois", rois)
.SetParam("spatial_scale", spatial_scale)
.SetParam("output_dim", output_dim)
.SetParam("pooled_size", pooled_size)
.SetParam("group_size", group_size)
.Invoke(@out);
}
/// <summary>
/// Performs region-of-interest pooling on inputs. Resize bounding box coordinates by spatial_scale and crop input feature maps accordingly. The cropped feature maps are pooled by max pooling to a fixed size output indicated by pooled_size. batch_size will change to the number of region bounding boxes after PSROIPooling
/// </summary>
/// <param name="data">Input data to the pooling operator, a 4D Feature maps</param>
/// <param name="rois">Bounding box coordinates, a 2D array of [[batch_index, x1, y1, x2, y2]]. (x1, y1) and (x2, y2) are top left and down right corners of designated region of interest. batch_index indicates the index of corresponding image in the input data</param>
/// <param name="spatial_scale">Ratio of input feature map height (or w) to raw image height (or w). Equals the reciprocal of total stride in convolutional layers</param>
/// <param name="output_dim">fix output dim</param>
/// <param name="pooled_size">fix pooled size</param>
/// <param name="group_size">fix group size</param>
 /// <returns>returns new symbol</returns>
public static NdArray ContribPSROIPooling(Symbol data,
Symbol rois,
float spatial_scale,
int output_dim,
int pooled_size,
int group_size=0)
{
return new Operator("_contrib_PSROIPooling")
.SetParam("data", data)
.SetParam("rois", rois)
.SetParam("spatial_scale", spatial_scale)
.SetParam("output_dim", output_dim)
.SetParam("pooled_size", pooled_size)
.SetParam("group_size", group_size)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribPSROIPooling(NdArray @out)
{
return new Operator("_backward__contrib_PSROIPooling")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardContribPSROIPooling()
{
return new Operator("_backward__contrib_PSROIPooling")
.Invoke();
}
private static readonly List<string> ConvolutionV1CudnnTuneConvert = new List<string>(){"fastest","limited_workspace","off"};
private static readonly List<string> ConvolutionV1LayoutConvert = new List<string>(){"NCDHW","NCHW","NDHWC","NHWC"};
/// <summary>
/// This operator is DEPRECATED. Apply convolution to input then add a bias.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the ConvolutionV1Op.</param>
/// <param name="kernel">convolution kernel size: (h, w) or (d, h, w)</param>
/// <param name="num_filter">convolution filter(channel) number</param>
/// <param name="weight">Weight matrix.</param>
/// <param name="bias">Bias parameter.</param>
/// <param name="stride">convolution stride: (h, w) or (d, h, w)</param>
/// <param name="dilate">convolution dilate: (h, w) or (d, h, w)</param>
/// <param name="pad">pad for convolution: (h, w) or (d, h, w)</param>
/// <param name="num_group">Number of group partitions. Equivalent to slicing input into num_group    partitions, apply convolution on each, then concatenate the results</param>
/// <param name="workspace">Maximum tmp workspace allowed for convolution (MB).</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="cudnn_tune">Whether to pick convolution algo by running performance test.    Leads to higher startup time but may give faster speed. Options are:    'off': no tuning    'limited_workspace': run test and pick the fastest algorithm that doesn't exceed workspace limit.    'fastest': pick the fastest algorithm and ignore workspace limit.    If set to None (default), behavior is determined by environment    variable MXNET_CUDNN_AUTOTUNE_DEFAULT: 0 for off,    1 for limited workspace (default), 2 for fastest.</param>
/// <param name="cudnn_off">Turn off cudnn for this layer.</param>
/// <param name="layout">Set layout for input, output and weight. Empty for    default layout: NCHW for 2d and NCDHW for 3d.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ConvolutionV1(NdArray @out,
NdArray data,
Shape kernel,
uint num_filter,
NdArray weight=null,
NdArray bias=null,
Shape stride=null,
Shape dilate=null,
Shape pad=null,
uint num_group=1,
ulong workspace=1024,
bool no_bias=false,
ConvolutionV1CudnnTune? cudnn_tune=null,
bool cudnn_off=false,
ConvolutionV1Layout? layout=null)
{
return new Operator("Convolution_v1")
.SetParam("kernel", kernel)
.SetParam("stride", stride)
.SetParam("dilate", dilate)
.SetParam("pad", pad)
.SetParam("num_filter", num_filter)
.SetParam("num_group", num_group)
.SetParam("workspace", workspace)
.SetParam("no_bias", no_bias)
.SetParam("cudnn_tune", Util.EnumToString<ConvolutionV1CudnnTune>(cudnn_tune,ConvolutionV1CudnnTuneConvert))
.SetParam("cudnn_off", cudnn_off)
.SetParam("layout", Util.EnumToString<ConvolutionV1Layout>(layout,ConvolutionV1LayoutConvert))
.SetInput("data", data)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke(@out);
}
/// <summary>
/// This operator is DEPRECATED. Apply convolution to input then add a bias.
/// </summary>
/// <param name="data">Input data to the ConvolutionV1Op.</param>
/// <param name="kernel">convolution kernel size: (h, w) or (d, h, w)</param>
/// <param name="num_filter">convolution filter(channel) number</param>
/// <param name="weight">Weight matrix.</param>
/// <param name="bias">Bias parameter.</param>
/// <param name="stride">convolution stride: (h, w) or (d, h, w)</param>
/// <param name="dilate">convolution dilate: (h, w) or (d, h, w)</param>
/// <param name="pad">pad for convolution: (h, w) or (d, h, w)</param>
/// <param name="num_group">Number of group partitions. Equivalent to slicing input into num_group    partitions, apply convolution on each, then concatenate the results</param>
/// <param name="workspace">Maximum tmp workspace allowed for convolution (MB).</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="cudnn_tune">Whether to pick convolution algo by running performance test.    Leads to higher startup time but may give faster speed. Options are:    'off': no tuning    'limited_workspace': run test and pick the fastest algorithm that doesn't exceed workspace limit.    'fastest': pick the fastest algorithm and ignore workspace limit.    If set to None (default), behavior is determined by environment    variable MXNET_CUDNN_AUTOTUNE_DEFAULT: 0 for off,    1 for limited workspace (default), 2 for fastest.</param>
/// <param name="cudnn_off">Turn off cudnn for this layer.</param>
/// <param name="layout">Set layout for input, output and weight. Empty for    default layout: NCHW for 2d and NCDHW for 3d.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ConvolutionV1(NdArray data,
Shape kernel,
uint num_filter,
NdArray weight=null,
NdArray bias=null,
Shape stride=null,
Shape dilate=null,
Shape pad=null,
uint num_group=1,
ulong workspace=1024,
bool no_bias=false,
ConvolutionV1CudnnTune? cudnn_tune=null,
bool cudnn_off=false,
ConvolutionV1Layout? layout=null)
{
return new Operator("Convolution_v1")
.SetParam("kernel", kernel)
.SetParam("stride", stride)
.SetParam("dilate", dilate)
.SetParam("pad", pad)
.SetParam("num_filter", num_filter)
.SetParam("num_group", num_group)
.SetParam("workspace", workspace)
.SetParam("no_bias", no_bias)
.SetParam("cudnn_tune", Util.EnumToString<ConvolutionV1CudnnTune>(cudnn_tune,ConvolutionV1CudnnTuneConvert))
.SetParam("cudnn_off", cudnn_off)
.SetParam("layout", Util.EnumToString<ConvolutionV1Layout>(layout,ConvolutionV1LayoutConvert))
.SetInput("data", data)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardConvolutionV1(NdArray @out)
{
return new Operator("_backward_Convolution_v1")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardConvolutionV1()
{
return new Operator("_backward_Convolution_v1")
.Invoke();
}
/// <summary>
/// Applies correlation to inputs.The correlation layer performs multiplicative patch comparisons between two feature maps.Given two multi-channel feature maps :math:`f_{1}, f_{2}`, with :math:`w`, :math:`h`, and :math:`c` being their width, height, and number of channels,the correlation layer lets the network compare each patch from :math:`f_{1}` with each patch from :math:`f_{2}`.For now we consider only a single comparison of two patches. The 'correlation' of two patches centered at :math:`x_{1}` in the first map and:math:`x_{2}` in the second map is then defined as:.. math::   c(x_{1}, x_{2}) = \sum_{o \in [-k,k] \times [-k,k]} <f_{1}(x_{1} + o), f_{2}(x_{2} + o)>for a square patch of size :math:`K:=2k+1`.Note that the equation above is identical to one step of a convolution in neural networks, but instead of convolving data with a filter, it convolves data with otherdata. For this reason, it has no training weights.Computing :math:`c(x_{1}, x_{2})` involves :math:`c * K^{2}` multiplications. Comparing all patch combinations involves :math:`w^{2}*h^{2}` such computations.Given a maximum displacement :math:`d`, for each location :math:`x_{1}` it computes correlations :math:`c(x_{1}, x_{2})` only in a neighborhood of size :math:`D:=2d+1`,by limiting the range of :math:`x_{2}`. We use strides :math:`s_{1}, s_{2}`, to quantize :math:`x_{1}` globally and to quantize :math:`x_{2}` within the neighborhoodcentered around :math:`x_{1}`.The final output is defined by the following expression:.. math::  out[n, q, i, j] = c(x_{i, j}, x_{q})where :math:`i` and :math:`j` enumerate spatial locations in :math:`f_{1}`, and :math:`q` denotes the :math:`q^{th}` neighborhood of :math:`x_{i,j}`.Defined in I:\workspace\incubator-mxnet\src\operator\correlation.cc:L192
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data1">Input data1 to the correlation.</param>
/// <param name="data2">Input data2 to the correlation.</param>
/// <param name="kernel_size">kernel size for Correlation must be an odd number</param>
/// <param name="max_displacement">Max displacement of Correlation </param>
/// <param name="stride1">stride1 quantize data1 globally</param>
/// <param name="stride2">stride2 quantize data2 within the neighborhood centered around data1</param>
/// <param name="pad_size">pad for Correlation</param>
/// <param name="is_multiply">operation type is either multiplication or subduction</param>
 /// <returns>returns new symbol</returns>
public static NdArray Correlation(NdArray @out,
NdArray data1,
NdArray data2,
uint kernel_size=1,
uint max_displacement=1,
uint stride1=1,
uint stride2=1,
uint pad_size=0,
bool is_multiply=true)
{
return new Operator("Correlation")
.SetParam("kernel_size", kernel_size)
.SetParam("max_displacement", max_displacement)
.SetParam("stride1", stride1)
.SetParam("stride2", stride2)
.SetParam("pad_size", pad_size)
.SetParam("is_multiply", is_multiply)
.SetInput("data1", data1)
.SetInput("data2", data2)
.Invoke(@out);
}
/// <summary>
/// Applies correlation to inputs.The correlation layer performs multiplicative patch comparisons between two feature maps.Given two multi-channel feature maps :math:`f_{1}, f_{2}`, with :math:`w`, :math:`h`, and :math:`c` being their width, height, and number of channels,the correlation layer lets the network compare each patch from :math:`f_{1}` with each patch from :math:`f_{2}`.For now we consider only a single comparison of two patches. The 'correlation' of two patches centered at :math:`x_{1}` in the first map and:math:`x_{2}` in the second map is then defined as:.. math::   c(x_{1}, x_{2}) = \sum_{o \in [-k,k] \times [-k,k]} <f_{1}(x_{1} + o), f_{2}(x_{2} + o)>for a square patch of size :math:`K:=2k+1`.Note that the equation above is identical to one step of a convolution in neural networks, but instead of convolving data with a filter, it convolves data with otherdata. For this reason, it has no training weights.Computing :math:`c(x_{1}, x_{2})` involves :math:`c * K^{2}` multiplications. Comparing all patch combinations involves :math:`w^{2}*h^{2}` such computations.Given a maximum displacement :math:`d`, for each location :math:`x_{1}` it computes correlations :math:`c(x_{1}, x_{2})` only in a neighborhood of size :math:`D:=2d+1`,by limiting the range of :math:`x_{2}`. We use strides :math:`s_{1}, s_{2}`, to quantize :math:`x_{1}` globally and to quantize :math:`x_{2}` within the neighborhoodcentered around :math:`x_{1}`.The final output is defined by the following expression:.. math::  out[n, q, i, j] = c(x_{i, j}, x_{q})where :math:`i` and :math:`j` enumerate spatial locations in :math:`f_{1}`, and :math:`q` denotes the :math:`q^{th}` neighborhood of :math:`x_{i,j}`.Defined in I:\workspace\incubator-mxnet\src\operator\correlation.cc:L192
/// </summary>
/// <param name="data1">Input data1 to the correlation.</param>
/// <param name="data2">Input data2 to the correlation.</param>
/// <param name="kernel_size">kernel size for Correlation must be an odd number</param>
/// <param name="max_displacement">Max displacement of Correlation </param>
/// <param name="stride1">stride1 quantize data1 globally</param>
/// <param name="stride2">stride2 quantize data2 within the neighborhood centered around data1</param>
/// <param name="pad_size">pad for Correlation</param>
/// <param name="is_multiply">operation type is either multiplication or subduction</param>
 /// <returns>returns new symbol</returns>
public static NdArray Correlation(NdArray data1,
NdArray data2,
uint kernel_size=1,
uint max_displacement=1,
uint stride1=1,
uint stride2=1,
uint pad_size=0,
bool is_multiply=true)
{
return new Operator("Correlation")
.SetParam("kernel_size", kernel_size)
.SetParam("max_displacement", max_displacement)
.SetParam("stride1", stride1)
.SetParam("stride2", stride2)
.SetParam("pad_size", pad_size)
.SetParam("is_multiply", is_multiply)
.SetInput("data1", data1)
.SetInput("data2", data2)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCorrelation(NdArray @out)
{
return new Operator("_backward_Correlation")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCorrelation()
{
return new Operator("_backward_Correlation")
.Invoke();
}
/// <summary>
/// .. note:: `Crop` is deprecated. Use `slice` instead.Crop the 2nd and 3rd dim of input data, with the corresponding size of h_w orwith width and height of the second input symbol, i.e., with one input, we need h_w tospecify the crop height and width, otherwise the second input symbol's size will be usedDefined in I:\workspace\incubator-mxnet\src\operator\crop.cc:L50
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Tensor or List of Tensors, the second input will be used as crop_like shape reference</param>
/// <param name="num_args">Number of inputs for crop, if equals one, then we will use the h_wfor crop height and width, else if equals two, then we will use the heightand width of the second input symbol, we name crop_like here</param>
/// <param name="offset">crop offset coordinate: (y, x)</param>
/// <param name="h_w">crop height and width: (h, w)</param>
/// <param name="center_crop">If set to true, then it will use be the center_crop,or it will crop using the shape of crop_like</param>
 /// <returns>returns new symbol</returns>
public static NdArray Crop(NdArray @out,
List<Symbol> data,
int num_args,
Shape offset=null,
Shape h_w=null,
bool center_crop=false)
{
return new Operator("Crop")
.SetParam("data", data)
.SetParam("num_args", num_args)
.SetParam("offset", offset)
.SetParam("h_w", h_w)
.SetParam("center_crop", center_crop)
.Invoke(@out);
}
/// <summary>
/// .. note:: `Crop` is deprecated. Use `slice` instead.Crop the 2nd and 3rd dim of input data, with the corresponding size of h_w orwith width and height of the second input symbol, i.e., with one input, we need h_w tospecify the crop height and width, otherwise the second input symbol's size will be usedDefined in I:\workspace\incubator-mxnet\src\operator\crop.cc:L50
/// </summary>
/// <param name="data">Tensor or List of Tensors, the second input will be used as crop_like shape reference</param>
/// <param name="num_args">Number of inputs for crop, if equals one, then we will use the h_wfor crop height and width, else if equals two, then we will use the heightand width of the second input symbol, we name crop_like here</param>
/// <param name="offset">crop offset coordinate: (y, x)</param>
/// <param name="h_w">crop height and width: (h, w)</param>
/// <param name="center_crop">If set to true, then it will use be the center_crop,or it will crop using the shape of crop_like</param>
 /// <returns>returns new symbol</returns>
public static NdArray Crop(List<Symbol> data,
int num_args,
Shape offset=null,
Shape h_w=null,
bool center_crop=false)
{
return new Operator("Crop")
.SetParam("data", data)
.SetParam("num_args", num_args)
.SetParam("offset", offset)
.SetParam("h_w", h_w)
.SetParam("center_crop", center_crop)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCrop(NdArray @out)
{
return new Operator("_backward_Crop")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardCrop()
{
return new Operator("_backward_Crop")
.Invoke();
}
/// <summary>
/// Stub for implementing an operator implemented in native frontend language.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data for the custom operator.</param>
/// <param name="info"></param>
/// <param name="need_top_grad">Whether this layer needs out grad for backward. Should be false for loss layers.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Native(NdArray @out,
NdArray[] data,
IntPtr info,
bool need_top_grad=true)
{
return new Operator("_Native")
.SetParam("info", info)
.SetParam("need_top_grad", need_top_grad)
.AddInput(data)
.Invoke(@out);
}
/// <summary>
/// Stub for implementing an operator implemented in native frontend language.
/// </summary>
/// <param name="data">Input data for the custom operator.</param>
/// <param name="info"></param>
/// <param name="need_top_grad">Whether this layer needs out grad for backward. Should be false for loss layers.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Native(NdArray[] data,
IntPtr info,
bool need_top_grad=true)
{
return new Operator("_Native")
.SetParam("info", info)
.SetParam("need_top_grad", need_top_grad)
.AddInput(data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardNative(NdArray @out)
{
return new Operator("_backward__Native")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardNative()
{
return new Operator("_backward__Native")
.Invoke();
}
private static readonly List<string> GridgeneratorTransformTypeConvert = new List<string>(){"affine","warp"};
/// <summary>
/// Generates 2D sampling grid for bilinear sampling.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the function.</param>
/// <param name="transform_type">The type of transformation. For `affine`, input data should be an affine matrix of size (batch, 6). For `warp`, input data should be an optical flow of size (batch, 2, h, w).</param>
/// <param name="target_shape">Specifies the output shape (H, W). This is required if transformation type is `affine`. If transformation type is `warp`, this parameter is ignored.</param>
 /// <returns>returns new symbol</returns>
public static NdArray GridGenerator(NdArray @out,
NdArray data,
GridgeneratorTransformType transform_type,
Shape target_shape=null)
{
return new Operator("GridGenerator")
.SetParam("transform_type", Util.EnumToString<GridgeneratorTransformType>(transform_type,GridgeneratorTransformTypeConvert))
.SetParam("target_shape", target_shape)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Generates 2D sampling grid for bilinear sampling.
/// </summary>
/// <param name="data">Input data to the function.</param>
/// <param name="transform_type">The type of transformation. For `affine`, input data should be an affine matrix of size (batch, 6). For `warp`, input data should be an optical flow of size (batch, 2, h, w).</param>
/// <param name="target_shape">Specifies the output shape (H, W). This is required if transformation type is `affine`. If transformation type is `warp`, this parameter is ignored.</param>
 /// <returns>returns new symbol</returns>
public static NdArray GridGenerator(NdArray data,
GridgeneratorTransformType transform_type,
Shape target_shape=null)
{
return new Operator("GridGenerator")
.SetParam("transform_type", Util.EnumToString<GridgeneratorTransformType>(transform_type,GridgeneratorTransformTypeConvert))
.SetParam("target_shape", target_shape)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardGridGenerator(NdArray @out)
{
return new Operator("_backward_GridGenerator")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardGridGenerator()
{
return new Operator("_backward_GridGenerator")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardIdentityAttachKLSparseReg(NdArray @out)
{
return new Operator("_backward_IdentityAttachKLSparseReg")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardIdentityAttachKLSparseReg()
{
return new Operator("_backward_IdentityAttachKLSparseReg")
.Invoke();
}
/// <summary>
/// Applies instance normalization to the n-dimensional input array.This operator takes an n-dimensional input array where (n>2) and normalizesthe input using the following formula:.. math::  out = \frac{x - mean[data]}{ \sqrt{Var[data]} + \epsilon} * gamma + betaThis layer is similar to batch normalization layer (`BatchNorm`)with two differences: first, the normalization iscarried out per example (instance), not over a batch. Second, thesame normalization is applied both at test and train time. Thisoperation is also known as `contrast normalization`.If the input data is of shape [batch, channel, spacial_dim1, spacial_dim2, ...],`gamma` and `beta` parameters must be vectors of shape [channel].This implementation is based on paper:.. [1] Instance Normalization: The Missing Ingredient for Fast Stylization,   D. Ulyanov, A. Vedaldi, V. Lempitsky, 2016 (arXiv:1607.08022v2).Examples::  // Input of shape (2,1,2)  x = [[[ 1.1,  2.2]],       [[ 3.3,  4.4]]]  // gamma parameter of length 1  gamma = [1.5]  // beta parameter of length 1  beta = [0.5]  // Instance normalization is calculated with the above formula  InstanceNorm(x,gamma,beta) = [[[-0.997527  ,  1.99752665]],                                [[-0.99752653,  1.99752724]]]Defined in I:\workspace\incubator-mxnet\src\operator\instance_norm.cc:L95
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">An n-dimensional input array (n > 2) of the form [batch, channel, spatial_dim1, spatial_dim2, ...].</param>
/// <param name="gamma">A vector of length 'channel', which multiplies the normalized input.</param>
/// <param name="beta">A vector of length 'channel', which is added to the product of the normalized input and the weight.</param>
/// <param name="eps">An `epsilon` parameter to prevent division by 0.</param>
 /// <returns>returns new symbol</returns>
public static NdArray InstanceNorm(NdArray @out,
NdArray data,
NdArray gamma,
NdArray beta,
float eps=0.001f)
{
return new Operator("InstanceNorm")
.SetParam("eps", eps)
.SetInput("data", data)
.SetInput("gamma", gamma)
.SetInput("beta", beta)
.Invoke(@out);
}
/// <summary>
/// Applies instance normalization to the n-dimensional input array.This operator takes an n-dimensional input array where (n>2) and normalizesthe input using the following formula:.. math::  out = \frac{x - mean[data]}{ \sqrt{Var[data]} + \epsilon} * gamma + betaThis layer is similar to batch normalization layer (`BatchNorm`)with two differences: first, the normalization iscarried out per example (instance), not over a batch. Second, thesame normalization is applied both at test and train time. Thisoperation is also known as `contrast normalization`.If the input data is of shape [batch, channel, spacial_dim1, spacial_dim2, ...],`gamma` and `beta` parameters must be vectors of shape [channel].This implementation is based on paper:.. [1] Instance Normalization: The Missing Ingredient for Fast Stylization,   D. Ulyanov, A. Vedaldi, V. Lempitsky, 2016 (arXiv:1607.08022v2).Examples::  // Input of shape (2,1,2)  x = [[[ 1.1,  2.2]],       [[ 3.3,  4.4]]]  // gamma parameter of length 1  gamma = [1.5]  // beta parameter of length 1  beta = [0.5]  // Instance normalization is calculated with the above formula  InstanceNorm(x,gamma,beta) = [[[-0.997527  ,  1.99752665]],                                [[-0.99752653,  1.99752724]]]Defined in I:\workspace\incubator-mxnet\src\operator\instance_norm.cc:L95
/// </summary>
/// <param name="data">An n-dimensional input array (n > 2) of the form [batch, channel, spatial_dim1, spatial_dim2, ...].</param>
/// <param name="gamma">A vector of length 'channel', which multiplies the normalized input.</param>
/// <param name="beta">A vector of length 'channel', which is added to the product of the normalized input and the weight.</param>
/// <param name="eps">An `epsilon` parameter to prevent division by 0.</param>
 /// <returns>returns new symbol</returns>
public static NdArray InstanceNorm(NdArray data,
NdArray gamma,
NdArray beta,
float eps=0.001f)
{
return new Operator("InstanceNorm")
.SetParam("eps", eps)
.SetInput("data", data)
.SetInput("gamma", gamma)
.SetInput("beta", beta)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardInstanceNorm(NdArray @out)
{
return new Operator("_backward_InstanceNorm")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardInstanceNorm()
{
return new Operator("_backward_InstanceNorm")
.Invoke();
}
private static readonly List<string> L2normalizationModeConvert = new List<string>(){"channel","instance","spatial"};
/// <summary>
/// Normalize the input array using the L2 norm.For 1-D NDArray, it computes::  out = data / sqrt(sum(data ** 2) + eps)For N-D NDArray, if the input array has shape (N, N, ..., N),with ``mode`` = ``instance``, it normalizes each instance in the multidimensionalarray by its L2 norm.::  for i in 0...N    out[i,:,:,...,:] = data[i,:,:,...,:] / sqrt(sum(data[i,:,:,...,:] ** 2) + eps)with ``mode`` = ``channel``, it normalizes each channel in the array by its L2 norm.::  for i in 0...N    out[:,i,:,...,:] = data[:,i,:,...,:] / sqrt(sum(data[:,i,:,...,:] ** 2) + eps)with ``mode`` = ``spatial``, it normalizes the cross channel norm for each positionin the array by its L2 norm.::  for dim in 2...N    for i in 0...N      out[.....,i,...] = take(out, indices=i, axis=dim) / sqrt(sum(take(out, indices=i, axis=dim) ** 2) + eps)          -dim-Example::  x = [[[1,2],        [3,4]],       [[2,2],        [5,6]]]  L2Normalization(x, mode='instance')  =[[[ 0.18257418  0.36514837]     [ 0.54772252  0.73029673]]    [[ 0.24077171  0.24077171]     [ 0.60192931  0.72231513]]]  L2Normalization(x, mode='channel')  =[[[ 0.31622776  0.44721359]     [ 0.94868326  0.89442718]]    [[ 0.37139067  0.31622776]     [ 0.92847669  0.94868326]]]  L2Normalization(x, mode='spatial')  =[[[ 0.44721359  0.89442718]     [ 0.60000002  0.80000001]]    [[ 0.70710677  0.70710677]     [ 0.6401844   0.76822126]]]Defined in I:\workspace\incubator-mxnet\src\operator\l2_normalization.cc:L93
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array to normalize.</param>
/// <param name="eps">A small constant for numerical stability.</param>
/// <param name="mode">Specify the dimension along which to compute L2 norm.</param>
 /// <returns>returns new symbol</returns>
public static NdArray L2Normalization(NdArray @out,
NdArray data,
float eps=1e-10f,
L2normalizationMode mode=L2normalizationMode.Instance)
{
return new Operator("L2Normalization")
.SetParam("eps", eps)
.SetParam("mode", Util.EnumToString<L2normalizationMode>(mode,L2normalizationModeConvert))
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Normalize the input array using the L2 norm.For 1-D NDArray, it computes::  out = data / sqrt(sum(data ** 2) + eps)For N-D NDArray, if the input array has shape (N, N, ..., N),with ``mode`` = ``instance``, it normalizes each instance in the multidimensionalarray by its L2 norm.::  for i in 0...N    out[i,:,:,...,:] = data[i,:,:,...,:] / sqrt(sum(data[i,:,:,...,:] ** 2) + eps)with ``mode`` = ``channel``, it normalizes each channel in the array by its L2 norm.::  for i in 0...N    out[:,i,:,...,:] = data[:,i,:,...,:] / sqrt(sum(data[:,i,:,...,:] ** 2) + eps)with ``mode`` = ``spatial``, it normalizes the cross channel norm for each positionin the array by its L2 norm.::  for dim in 2...N    for i in 0...N      out[.....,i,...] = take(out, indices=i, axis=dim) / sqrt(sum(take(out, indices=i, axis=dim) ** 2) + eps)          -dim-Example::  x = [[[1,2],        [3,4]],       [[2,2],        [5,6]]]  L2Normalization(x, mode='instance')  =[[[ 0.18257418  0.36514837]     [ 0.54772252  0.73029673]]    [[ 0.24077171  0.24077171]     [ 0.60192931  0.72231513]]]  L2Normalization(x, mode='channel')  =[[[ 0.31622776  0.44721359]     [ 0.94868326  0.89442718]]    [[ 0.37139067  0.31622776]     [ 0.92847669  0.94868326]]]  L2Normalization(x, mode='spatial')  =[[[ 0.44721359  0.89442718]     [ 0.60000002  0.80000001]]    [[ 0.70710677  0.70710677]     [ 0.6401844   0.76822126]]]Defined in I:\workspace\incubator-mxnet\src\operator\l2_normalization.cc:L93
/// </summary>
/// <param name="data">Input array to normalize.</param>
/// <param name="eps">A small constant for numerical stability.</param>
/// <param name="mode">Specify the dimension along which to compute L2 norm.</param>
 /// <returns>returns new symbol</returns>
public static NdArray L2Normalization(NdArray data,
float eps=1e-10f,
L2normalizationMode mode=L2normalizationMode.Instance)
{
return new Operator("L2Normalization")
.SetParam("eps", eps)
.SetParam("mode", Util.EnumToString<L2normalizationMode>(mode,L2normalizationModeConvert))
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardL2Normalization(NdArray @out)
{
return new Operator("_backward_L2Normalization")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardL2Normalization()
{
return new Operator("_backward_L2Normalization")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLeakyReLU(NdArray @out)
{
return new Operator("_backward_LeakyReLU")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLeakyReLU()
{
return new Operator("_backward_LeakyReLU")
.Invoke();
}
/// <summary>
/// Applies local response normalization to the input.The local response normalization layer performs "lateral inhibition" by normalizingover local input regions.If :math:`a_{x,y}^{i}` is the activity of a neuron computed by applying kernel :math:`i` at position:math:`(x, y)` and then applying the ReLU nonlinearity, the response-normalizedactivity :math:`b_{x,y}^{i}` is given by the expression:.. math::   b_{x,y}^{i} = \frac{a_{x,y}^{i}}{\Bigg({k + \alpha \sum_{j=max(0, i-\frac{n}{2})}^{min(N-1, i+\frac{n}{2})} (a_{x,y}^{j})^{2}}\Bigg)^{\beta}}where the sum runs over :math:`n` "adjacent" kernel maps at the same spatial position, and :math:`N` is the totalnumber of kernels in the layer.Defined in I:\workspace\incubator-mxnet\src\operator\lrn.cc:L73
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data.</param>
/// <param name="nsize">normalization window width in elements.</param>
/// <param name="alpha">The variance scaling parameter :math:`lpha` in the LRN expression.</param>
/// <param name="beta">The power parameter :math:`eta` in the LRN expression.</param>
/// <param name="knorm">The parameter :math:`k` in the LRN expression.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LRN(NdArray @out,
NdArray data,
uint nsize,
float alpha=0.0001f,
float beta=0.75f,
float knorm=2f)
{
return new Operator("LRN")
.SetParam("alpha", alpha)
.SetParam("beta", beta)
.SetParam("knorm", knorm)
.SetParam("nsize", nsize)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Applies local response normalization to the input.The local response normalization layer performs "lateral inhibition" by normalizingover local input regions.If :math:`a_{x,y}^{i}` is the activity of a neuron computed by applying kernel :math:`i` at position:math:`(x, y)` and then applying the ReLU nonlinearity, the response-normalizedactivity :math:`b_{x,y}^{i}` is given by the expression:.. math::   b_{x,y}^{i} = \frac{a_{x,y}^{i}}{\Bigg({k + \alpha \sum_{j=max(0, i-\frac{n}{2})}^{min(N-1, i+\frac{n}{2})} (a_{x,y}^{j})^{2}}\Bigg)^{\beta}}where the sum runs over :math:`n` "adjacent" kernel maps at the same spatial position, and :math:`N` is the totalnumber of kernels in the layer.Defined in I:\workspace\incubator-mxnet\src\operator\lrn.cc:L73
/// </summary>
/// <param name="data">Input data.</param>
/// <param name="nsize">normalization window width in elements.</param>
/// <param name="alpha">The variance scaling parameter :math:`lpha` in the LRN expression.</param>
/// <param name="beta">The power parameter :math:`eta` in the LRN expression.</param>
/// <param name="knorm">The parameter :math:`k` in the LRN expression.</param>
 /// <returns>returns new symbol</returns>
public static NdArray LRN(NdArray data,
uint nsize,
float alpha=0.0001f,
float beta=0.75f,
float knorm=2f)
{
return new Operator("LRN")
.SetParam("alpha", alpha)
.SetParam("beta", beta)
.SetParam("knorm", knorm)
.SetParam("nsize", nsize)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLRN(NdArray @out)
{
return new Operator("_backward_LRN")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLRN()
{
return new Operator("_backward_LRN")
.Invoke();
}
private static readonly List<string> MakelossNormalizationConvert = new List<string>(){"batch","null","valid"};
/// <summary>
/// Make your own loss function in network construction.This operator accepts a customized loss function symbol as a terminal loss andthe symbol should be an operator with no backward dependency.The output of this function is the gradient of loss with respect to the input data.For example, if you are a making a cross entropy loss function. Assume ``out`` is thepredicted output and ``label`` is the true label, then the cross entropy can be defined as::  cross_entropy = label * log(out) + (1 - label) * log(1 - out)  loss = MakeLoss(cross_entropy)We will need to use ``MakeLoss`` when we are creating our own loss function or we want tocombine multiple loss functions. Also we may want to stop some variables' gradientsfrom backpropagation. See more detail in ``BlockGrad`` or ``stop_gradient``.In addition, we can give a scale to the loss by setting ``grad_scale``,so that the gradient of the loss will be rescaled in the backpropagation... note:: This operator should be used as a Symbol instead of NDArray.Defined in I:\workspace\incubator-mxnet\src\operator\make_loss.cc:L71
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array.</param>
/// <param name="grad_scale">Gradient scale as a supplement to unary and binary operators</param>
/// <param name="valid_thresh">clip each element in the array to 0 when it is less than ``valid_thresh``. This is used when ``normalization`` is set to ``'valid'``.</param>
/// <param name="normalization">If this is set to null, the output gradient will not be normalized. If this is set to batch, the output gradient will be divided by the batch size. If this is set to valid, the output gradient will be divided by the number of valid input elements.</param>
 /// <returns>returns new symbol</returns>
public static NdArray MakeLoss(NdArray @out,
NdArray data,
float grad_scale=1f,
float valid_thresh=0f,
MakelossNormalization normalization=MakelossNormalization.Null)
{
return new Operator("MakeLoss")
.SetParam("grad_scale", grad_scale)
.SetParam("valid_thresh", valid_thresh)
.SetParam("normalization", Util.EnumToString<MakelossNormalization>(normalization,MakelossNormalizationConvert))
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Make your own loss function in network construction.This operator accepts a customized loss function symbol as a terminal loss andthe symbol should be an operator with no backward dependency.The output of this function is the gradient of loss with respect to the input data.For example, if you are a making a cross entropy loss function. Assume ``out`` is thepredicted output and ``label`` is the true label, then the cross entropy can be defined as::  cross_entropy = label * log(out) + (1 - label) * log(1 - out)  loss = MakeLoss(cross_entropy)We will need to use ``MakeLoss`` when we are creating our own loss function or we want tocombine multiple loss functions. Also we may want to stop some variables' gradientsfrom backpropagation. See more detail in ``BlockGrad`` or ``stop_gradient``.In addition, we can give a scale to the loss by setting ``grad_scale``,so that the gradient of the loss will be rescaled in the backpropagation... note:: This operator should be used as a Symbol instead of NDArray.Defined in I:\workspace\incubator-mxnet\src\operator\make_loss.cc:L71
/// </summary>
/// <param name="data">Input array.</param>
/// <param name="grad_scale">Gradient scale as a supplement to unary and binary operators</param>
/// <param name="valid_thresh">clip each element in the array to 0 when it is less than ``valid_thresh``. This is used when ``normalization`` is set to ``'valid'``.</param>
/// <param name="normalization">If this is set to null, the output gradient will not be normalized. If this is set to batch, the output gradient will be divided by the batch size. If this is set to valid, the output gradient will be divided by the number of valid input elements.</param>
 /// <returns>returns new symbol</returns>
public static NdArray MakeLoss(NdArray data,
float grad_scale=1f,
float valid_thresh=0f,
MakelossNormalization normalization=MakelossNormalization.Null)
{
return new Operator("MakeLoss")
.SetParam("grad_scale", grad_scale)
.SetParam("valid_thresh", valid_thresh)
.SetParam("normalization", Util.EnumToString<MakelossNormalization>(normalization,MakelossNormalizationConvert))
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMakeLoss(NdArray @out)
{
return new Operator("_backward_MakeLoss")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMakeLoss()
{
return new Operator("_backward_MakeLoss")
.Invoke();
}
private static readonly List<string> ActivationActTypeConvert = new List<string>(){"relu","sigmoid","softrelu","tanh"};
/// <summary>
/// Applies an activation function element-wise to the input.The following activation functions are supported:- `relu`: Rectified Linear Unit, :math:`y = max(x, 0)`- `sigmoid`: :math:`y = \frac{1}{1 + exp(-x)}`- `tanh`: Hyperbolic tangent, :math:`y = \frac{exp(x) - exp(-x)}{exp(x) + exp(-x)}`- `softrelu`: Soft ReLU, or SoftPlus, :math:`y = log(1 + exp(x))`Defined in I:\workspace\incubator-mxnet\src\operator\nn\activation.cc:L92
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array to activation function.</param>
/// <param name="act_type">Activation function to be applied.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Activation(NdArray @out,
NdArray data,
ActivationActType act_type)
{
return new Operator("Activation")
.SetParam("act_type", Util.EnumToString<ActivationActType>(act_type,ActivationActTypeConvert))
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Applies an activation function element-wise to the input.The following activation functions are supported:- `relu`: Rectified Linear Unit, :math:`y = max(x, 0)`- `sigmoid`: :math:`y = \frac{1}{1 + exp(-x)}`- `tanh`: Hyperbolic tangent, :math:`y = \frac{exp(x) - exp(-x)}{exp(x) + exp(-x)}`- `softrelu`: Soft ReLU, or SoftPlus, :math:`y = log(1 + exp(x))`Defined in I:\workspace\incubator-mxnet\src\operator\nn\activation.cc:L92
/// </summary>
/// <param name="data">Input array to activation function.</param>
/// <param name="act_type">Activation function to be applied.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Activation(NdArray data,
ActivationActType act_type)
{
return new Operator("Activation")
.SetParam("act_type", Util.EnumToString<ActivationActType>(act_type,ActivationActTypeConvert))
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardActivation(NdArray @out)
{
return new Operator("_backward_Activation")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardActivation()
{
return new Operator("_backward_Activation")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBatchNorm(NdArray @out)
{
return new Operator("_backward_BatchNorm")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardBatchNorm()
{
return new Operator("_backward_BatchNorm")
.Invoke();
}
private static readonly List<string> ConvolutionCudnnTuneConvert = new List<string>(){"fastest","limited_workspace","off"};
private static readonly List<string> ConvolutionLayoutConvert = new List<string>(){"NCDHW","NCHW","NCW","NDHWC","NHWC"};
/// <summary>
/// Compute *N*-D convolution on *(N+2)*-D input.In the 2-D convolution, given input data with shape *(batch_size,channel, height, width)*, the output is computed by.. math::   out[n,i,:,:] = bias[i] + \sum_{j=0}^{channel} data[n,j,:,:] \star   weight[i,j,:,:]where :math:`\star` is the 2-D cross-correlation operator.For general 2-D convolution, the shapes are- **data**: *(batch_size, channel, height, width)*- **weight**: *(num_filter, channel, kernel[0], kernel[1])*- **bias**: *(num_filter,)*- **out**: *(batch_size, num_filter, out_height, out_width)*.Define::  f(x,k,p,s,d) = floor((x+2*p-d*(k-1)-1)/s)+1then we have::  out_height=f(height, kernel[0], pad[0], stride[0], dilate[0])  out_width=f(width, kernel[1], pad[1], stride[1], dilate[1])If ``no_bias`` is set to be true, then the ``bias`` term is ignored.The default data ``layout`` is *NCHW*, namely *(batch_size, channel, height,width)*. We can choose other layouts such as *NHWC*.If ``num_group`` is larger than 1, denoted by *g*, then split the input ``data``evenly into *g* parts along the channel axis, and also evenly split ``weight``along the first dimension. Next compute the convolution on the *i*-th part ofthe data with the *i*-th weight part. The output is obtained by concatenating allthe *g* results.1-D convolution does not have *height* dimension but only *width* in space.- **data**: *(batch_size, channel, width)*- **weight**: *(num_filter, channel, kernel[0])*- **bias**: *(num_filter,)*- **out**: *(batch_size, num_filter, out_width)*.3-D convolution adds an additional *depth* dimension besides *height* and*width*. The shapes are- **data**: *(batch_size, channel, depth, height, width)*- **weight**: *(num_filter, channel, kernel[0], kernel[1], kernel[2])*- **bias**: *(num_filter,)*- **out**: *(batch_size, num_filter, out_depth, out_height, out_width)*.Both ``weight`` and ``bias`` are learnable parameters.There are other options to tune the performance.- **cudnn_tune**: enable this option leads to higher startup time but may give  faster speed. Options are  - **off**: no tuning  - **limited_workspace**:run test and pick the fastest algorithm that doesn't    exceed workspace limit.  - **fastest**: pick the fastest algorithm and ignore workspace limit.  - **None** (default): the behavior is determined by environment variable    ``MXNET_CUDNN_AUTOTUNE_DEFAULT``. 0 for off, 1 for limited workspace    (default), 2 for fastest.- **workspace**: A large number leads to more (GPU) memory usage but may improve  the performance.Defined in I:\workspace\incubator-mxnet\src\operator\nn\convolution.cc:L170
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the ConvolutionOp.</param>
/// <param name="kernel">Convolution kernel size: (w,), (h, w) or (d, h, w)</param>
/// <param name="num_filter">Convolution filter(channel) number</param>
/// <param name="weight">Weight matrix.</param>
/// <param name="bias">Bias parameter.</param>
/// <param name="stride">Convolution stride: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="dilate">Convolution dilate: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="pad">Zero pad for convolution: (w,), (h, w) or (d, h, w). Defaults to no padding.</param>
/// <param name="num_group">Number of group partitions.</param>
/// <param name="workspace">Maximum temporary workspace allowed for convolution (MB).</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="cudnn_tune">Whether to pick convolution algo by running performance test.</param>
/// <param name="cudnn_off">Turn off cudnn for this layer.</param>
/// <param name="layout">Set layout for input, output and weight. Empty for    default layout: NCW for 1d, NCHW for 2d and NCDHW for 3d.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Convolution(NdArray @out,
NdArray data,
Shape kernel,
uint num_filter,
NdArray weight=null,
NdArray bias=null,
Shape stride=null,
Shape dilate=null,
Shape pad=null,
uint num_group=1,
ulong workspace=1024,
bool no_bias=false,
ConvolutionCudnnTune? cudnn_tune=null,
bool cudnn_off=false,
ConvolutionLayout? layout=null)
{
return new Operator("Convolution")
.SetParam("kernel", kernel)
.SetParam("stride", stride)
.SetParam("dilate", dilate)
.SetParam("pad", pad)
.SetParam("num_filter", num_filter)
.SetParam("num_group", num_group)
.SetParam("workspace", workspace)
.SetParam("no_bias", no_bias)
.SetParam("cudnn_tune", Util.EnumToString<ConvolutionCudnnTune>(cudnn_tune,ConvolutionCudnnTuneConvert))
.SetParam("cudnn_off", cudnn_off)
.SetParam("layout", Util.EnumToString<ConvolutionLayout>(layout,ConvolutionLayoutConvert))
.SetInput("data", data)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke(@out);
}
/// <summary>
/// Compute *N*-D convolution on *(N+2)*-D input.In the 2-D convolution, given input data with shape *(batch_size,channel, height, width)*, the output is computed by.. math::   out[n,i,:,:] = bias[i] + \sum_{j=0}^{channel} data[n,j,:,:] \star   weight[i,j,:,:]where :math:`\star` is the 2-D cross-correlation operator.For general 2-D convolution, the shapes are- **data**: *(batch_size, channel, height, width)*- **weight**: *(num_filter, channel, kernel[0], kernel[1])*- **bias**: *(num_filter,)*- **out**: *(batch_size, num_filter, out_height, out_width)*.Define::  f(x,k,p,s,d) = floor((x+2*p-d*(k-1)-1)/s)+1then we have::  out_height=f(height, kernel[0], pad[0], stride[0], dilate[0])  out_width=f(width, kernel[1], pad[1], stride[1], dilate[1])If ``no_bias`` is set to be true, then the ``bias`` term is ignored.The default data ``layout`` is *NCHW*, namely *(batch_size, channel, height,width)*. We can choose other layouts such as *NHWC*.If ``num_group`` is larger than 1, denoted by *g*, then split the input ``data``evenly into *g* parts along the channel axis, and also evenly split ``weight``along the first dimension. Next compute the convolution on the *i*-th part ofthe data with the *i*-th weight part. The output is obtained by concatenating allthe *g* results.1-D convolution does not have *height* dimension but only *width* in space.- **data**: *(batch_size, channel, width)*- **weight**: *(num_filter, channel, kernel[0])*- **bias**: *(num_filter,)*- **out**: *(batch_size, num_filter, out_width)*.3-D convolution adds an additional *depth* dimension besides *height* and*width*. The shapes are- **data**: *(batch_size, channel, depth, height, width)*- **weight**: *(num_filter, channel, kernel[0], kernel[1], kernel[2])*- **bias**: *(num_filter,)*- **out**: *(batch_size, num_filter, out_depth, out_height, out_width)*.Both ``weight`` and ``bias`` are learnable parameters.There are other options to tune the performance.- **cudnn_tune**: enable this option leads to higher startup time but may give  faster speed. Options are  - **off**: no tuning  - **limited_workspace**:run test and pick the fastest algorithm that doesn't    exceed workspace limit.  - **fastest**: pick the fastest algorithm and ignore workspace limit.  - **None** (default): the behavior is determined by environment variable    ``MXNET_CUDNN_AUTOTUNE_DEFAULT``. 0 for off, 1 for limited workspace    (default), 2 for fastest.- **workspace**: A large number leads to more (GPU) memory usage but may improve  the performance.Defined in I:\workspace\incubator-mxnet\src\operator\nn\convolution.cc:L170
/// </summary>
/// <param name="data">Input data to the ConvolutionOp.</param>
/// <param name="kernel">Convolution kernel size: (w,), (h, w) or (d, h, w)</param>
/// <param name="num_filter">Convolution filter(channel) number</param>
/// <param name="weight">Weight matrix.</param>
/// <param name="bias">Bias parameter.</param>
/// <param name="stride">Convolution stride: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="dilate">Convolution dilate: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="pad">Zero pad for convolution: (w,), (h, w) or (d, h, w). Defaults to no padding.</param>
/// <param name="num_group">Number of group partitions.</param>
/// <param name="workspace">Maximum temporary workspace allowed for convolution (MB).</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="cudnn_tune">Whether to pick convolution algo by running performance test.</param>
/// <param name="cudnn_off">Turn off cudnn for this layer.</param>
/// <param name="layout">Set layout for input, output and weight. Empty for    default layout: NCW for 1d, NCHW for 2d and NCDHW for 3d.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Convolution(NdArray data,
Shape kernel,
uint num_filter,
NdArray weight=null,
NdArray bias=null,
Shape stride=null,
Shape dilate=null,
Shape pad=null,
uint num_group=1,
ulong workspace=1024,
bool no_bias=false,
ConvolutionCudnnTune? cudnn_tune=null,
bool cudnn_off=false,
ConvolutionLayout? layout=null)
{
return new Operator("Convolution")
.SetParam("kernel", kernel)
.SetParam("stride", stride)
.SetParam("dilate", dilate)
.SetParam("pad", pad)
.SetParam("num_filter", num_filter)
.SetParam("num_group", num_group)
.SetParam("workspace", workspace)
.SetParam("no_bias", no_bias)
.SetParam("cudnn_tune", Util.EnumToString<ConvolutionCudnnTune>(cudnn_tune,ConvolutionCudnnTuneConvert))
.SetParam("cudnn_off", cudnn_off)
.SetParam("layout", Util.EnumToString<ConvolutionLayout>(layout,ConvolutionLayoutConvert))
.SetInput("data", data)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardConvolution(NdArray @out)
{
return new Operator("_backward_Convolution")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardConvolution()
{
return new Operator("_backward_Convolution")
.Invoke();
}
private static readonly List<string> DeconvolutionCudnnTuneConvert = new List<string>(){"fastest","limited_workspace","off"};
private static readonly List<string> DeconvolutionLayoutConvert = new List<string>(){"NCDHW","NCHW","NCW","NDHWC","NHWC"};
/// <summary>
/// Computes 1D or 2D transposed convolution (aka fractionally strided convolution) of the input tensor. This operation can be seen as the gradient of Convolution operation with respect to its input. Convolution usually reduces the size of the input. Transposed convolution works the other way, going from a smaller input to a larger output while preserving the connectivity pattern.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input tensor to the deconvolution operation.</param>
/// <param name="kernel">Deconvolution kernel size: (w,), (h, w) or (d, h, w). This is same as the kernel size used for the corresponding convolution</param>
/// <param name="num_filter">Number of output filters.</param>
/// <param name="weight">Weights representing the kernel.</param>
/// <param name="bias">Bias added to the result after the deconvolution operation.</param>
/// <param name="stride">The stride used for the corresponding convolution: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="dilate">Dilation factor for each dimension of the input: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="pad">The amount of implicit zero padding added during convolution for each dimension of the input: (w,), (h, w) or (d, h, w). ``(kernel-1)/2`` is usually a good choice. If `target_shape` is set, `pad` will be ignored and a padding that will generate the target shape will be used. Defaults to no padding.</param>
/// <param name="adj">Adjustment for output shape: (w,), (h, w) or (d, h, w). If `target_shape` is set, `adj` will be ignored and computed accordingly.</param>
/// <param name="target_shape">Shape of the output tensor: (w,), (h, w) or (d, h, w).</param>
/// <param name="num_group">Number of groups partition.</param>
/// <param name="workspace">Maximum temporal workspace allowed for deconvolution (MB).</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="cudnn_tune">Whether to pick convolution algorithm by running performance test.</param>
/// <param name="cudnn_off">Turn off cudnn for this layer.</param>
/// <param name="layout">Set layout for input, output and weight. Empty for default layout, NCW for 1d, NCHW for 2d and NCDHW for 3d.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Deconvolution(NdArray @out,
NdArray data,
Shape kernel,
uint num_filter,
NdArray weight=null,
NdArray bias=null,
Shape stride=null,
Shape dilate=null,
Shape pad=null,
Shape adj=null,
Shape target_shape=null,
uint num_group=1,
ulong workspace=512,
bool no_bias=true,
DeconvolutionCudnnTune? cudnn_tune=null,
bool cudnn_off=false,
DeconvolutionLayout? layout=null)
{
return new Operator("Deconvolution")
.SetParam("kernel", kernel)
.SetParam("stride", stride)
.SetParam("dilate", dilate)
.SetParam("pad", pad)
.SetParam("adj", adj)
.SetParam("target_shape", target_shape)
.SetParam("num_filter", num_filter)
.SetParam("num_group", num_group)
.SetParam("workspace", workspace)
.SetParam("no_bias", no_bias)
.SetParam("cudnn_tune", Util.EnumToString<DeconvolutionCudnnTune>(cudnn_tune,DeconvolutionCudnnTuneConvert))
.SetParam("cudnn_off", cudnn_off)
.SetParam("layout", Util.EnumToString<DeconvolutionLayout>(layout,DeconvolutionLayoutConvert))
.SetInput("data", data)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke(@out);
}
/// <summary>
/// Computes 1D or 2D transposed convolution (aka fractionally strided convolution) of the input tensor. This operation can be seen as the gradient of Convolution operation with respect to its input. Convolution usually reduces the size of the input. Transposed convolution works the other way, going from a smaller input to a larger output while preserving the connectivity pattern.
/// </summary>
/// <param name="data">Input tensor to the deconvolution operation.</param>
/// <param name="kernel">Deconvolution kernel size: (w,), (h, w) or (d, h, w). This is same as the kernel size used for the corresponding convolution</param>
/// <param name="num_filter">Number of output filters.</param>
/// <param name="weight">Weights representing the kernel.</param>
/// <param name="bias">Bias added to the result after the deconvolution operation.</param>
/// <param name="stride">The stride used for the corresponding convolution: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="dilate">Dilation factor for each dimension of the input: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
/// <param name="pad">The amount of implicit zero padding added during convolution for each dimension of the input: (w,), (h, w) or (d, h, w). ``(kernel-1)/2`` is usually a good choice. If `target_shape` is set, `pad` will be ignored and a padding that will generate the target shape will be used. Defaults to no padding.</param>
/// <param name="adj">Adjustment for output shape: (w,), (h, w) or (d, h, w). If `target_shape` is set, `adj` will be ignored and computed accordingly.</param>
/// <param name="target_shape">Shape of the output tensor: (w,), (h, w) or (d, h, w).</param>
/// <param name="num_group">Number of groups partition.</param>
/// <param name="workspace">Maximum temporal workspace allowed for deconvolution (MB).</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="cudnn_tune">Whether to pick convolution algorithm by running performance test.</param>
/// <param name="cudnn_off">Turn off cudnn for this layer.</param>
/// <param name="layout">Set layout for input, output and weight. Empty for default layout, NCW for 1d, NCHW for 2d and NCDHW for 3d.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Deconvolution(NdArray data,
Shape kernel,
uint num_filter,
NdArray weight=null,
NdArray bias=null,
Shape stride=null,
Shape dilate=null,
Shape pad=null,
Shape adj=null,
Shape target_shape=null,
uint num_group=1,
ulong workspace=512,
bool no_bias=true,
DeconvolutionCudnnTune? cudnn_tune=null,
bool cudnn_off=false,
DeconvolutionLayout? layout=null)
{
return new Operator("Deconvolution")
.SetParam("kernel", kernel)
.SetParam("stride", stride)
.SetParam("dilate", dilate)
.SetParam("pad", pad)
.SetParam("adj", adj)
.SetParam("target_shape", target_shape)
.SetParam("num_filter", num_filter)
.SetParam("num_group", num_group)
.SetParam("workspace", workspace)
.SetParam("no_bias", no_bias)
.SetParam("cudnn_tune", Util.EnumToString<DeconvolutionCudnnTune>(cudnn_tune,DeconvolutionCudnnTuneConvert))
.SetParam("cudnn_off", cudnn_off)
.SetParam("layout", Util.EnumToString<DeconvolutionLayout>(layout,DeconvolutionLayoutConvert))
.SetInput("data", data)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDeconvolution(NdArray @out)
{
return new Operator("_backward_Deconvolution")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDeconvolution()
{
return new Operator("_backward_Deconvolution")
.Invoke();
}
private static readonly List<string> DropoutModeConvert = new List<string>(){"always","training"};
/// <summary>
/// Applies dropout operation to input array.- During training, each element of the input is set to zero with probability p.  The whole array is rescaled by :math:`1/(1-p)` to keep the expected  sum of the input unchanged.- During testing, this operator does not change the input if mode is 'training'.  If mode is 'always', the same computaion as during training will be applied.Example::  random.seed(998)  input_array = array([[3., 0.5,  -0.5,  2., 7.],                      [2., -0.4,   7.,  3., 0.2]])  a = symbol.Variable('a')  dropout = symbol.Dropout(a, p = 0.2)  executor = dropout.simple_bind(a = input_array.shape)  ## If training  executor.forward(is_train = True, a = input_array)  executor.outputs  [[ 3.75   0.625 -0.     2.5    8.75 ]   [ 2.5   -0.5    8.75   3.75   0.   ]]  ## If testing  executor.forward(is_train = False, a = input_array)  executor.outputs  [[ 3.     0.5   -0.5    2.     7.   ]   [ 2.    -0.4    7.     3.     0.2  ]]Defined in I:\workspace\incubator-mxnet\src\operator\nn\dropout.cc:L78
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array to which dropout will be applied.</param>
/// <param name="p">Fraction of the input that gets dropped out during training time.</param>
/// <param name="mode">Whether to only turn on dropout during training or to also turn on for inference.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Dropout(NdArray @out,
NdArray data,
float p=0.5f,
DropoutMode mode=DropoutMode.Training)
{
return new Operator("Dropout")
.SetParam("p", p)
.SetParam("mode", Util.EnumToString<DropoutMode>(mode,DropoutModeConvert))
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Applies dropout operation to input array.- During training, each element of the input is set to zero with probability p.  The whole array is rescaled by :math:`1/(1-p)` to keep the expected  sum of the input unchanged.- During testing, this operator does not change the input if mode is 'training'.  If mode is 'always', the same computaion as during training will be applied.Example::  random.seed(998)  input_array = array([[3., 0.5,  -0.5,  2., 7.],                      [2., -0.4,   7.,  3., 0.2]])  a = symbol.Variable('a')  dropout = symbol.Dropout(a, p = 0.2)  executor = dropout.simple_bind(a = input_array.shape)  ## If training  executor.forward(is_train = True, a = input_array)  executor.outputs  [[ 3.75   0.625 -0.     2.5    8.75 ]   [ 2.5   -0.5    8.75   3.75   0.   ]]  ## If testing  executor.forward(is_train = False, a = input_array)  executor.outputs  [[ 3.     0.5   -0.5    2.     7.   ]   [ 2.    -0.4    7.     3.     0.2  ]]Defined in I:\workspace\incubator-mxnet\src\operator\nn\dropout.cc:L78
/// </summary>
/// <param name="data">Input array to which dropout will be applied.</param>
/// <param name="p">Fraction of the input that gets dropped out during training time.</param>
/// <param name="mode">Whether to only turn on dropout during training or to also turn on for inference.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Dropout(NdArray data,
float p=0.5f,
DropoutMode mode=DropoutMode.Training)
{
return new Operator("Dropout")
.SetParam("p", p)
.SetParam("mode", Util.EnumToString<DropoutMode>(mode,DropoutModeConvert))
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDropout(NdArray @out)
{
return new Operator("_backward_Dropout")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardDropout()
{
return new Operator("_backward_Dropout")
.Invoke();
}
/// <summary>
/// Applies a linear transformation: :math:`Y = XW^T + b`.If ``flatten`` is set to be true, then the shapes are:- **data**: `(batch_size, x1, x2, ..., xn)`- **weight**: `(num_hidden, x1 * x2 * ... * xn)`- **bias**: `(num_hidden,)`- **out**: `(batch_size, num_hidden)`If ``flatten`` is set to be false, then the shapes are:- **data**: `(x1, x2, ..., xn, input_dim)`- **weight**: `(num_hidden, input_dim)`- **bias**: `(num_hidden,)`- **out**: `(x1, x2, ..., xn, num_hidden)`The learnable parameters include both ``weight`` and ``bias``.If ``no_bias`` is set to be true, then the ``bias`` term is ignored.Defined in I:\workspace\incubator-mxnet\src\operator\nn\fully_connected.cc:L98
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data.</param>
/// <param name="num_hidden">Number of hidden nodes of the output.</param>
/// <param name="weight">Weight matrix.</param>
/// <param name="bias">Bias parameter.</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="flatten">Whether to collapse all but the first axis of the input data tensor.</param>
 /// <returns>returns new symbol</returns>
public static NdArray FullyConnected(NdArray @out,
NdArray data,
int num_hidden,
NdArray weight=null,
NdArray bias=null,
bool no_bias=false,
bool flatten=true)
{
return new Operator("FullyConnected")
.SetParam("num_hidden", num_hidden)
.SetParam("no_bias", no_bias)
.SetParam("flatten", flatten)
.SetInput("data", data)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke(@out);
}
/// <summary>
/// Applies a linear transformation: :math:`Y = XW^T + b`.If ``flatten`` is set to be true, then the shapes are:- **data**: `(batch_size, x1, x2, ..., xn)`- **weight**: `(num_hidden, x1 * x2 * ... * xn)`- **bias**: `(num_hidden,)`- **out**: `(batch_size, num_hidden)`If ``flatten`` is set to be false, then the shapes are:- **data**: `(x1, x2, ..., xn, input_dim)`- **weight**: `(num_hidden, input_dim)`- **bias**: `(num_hidden,)`- **out**: `(x1, x2, ..., xn, num_hidden)`The learnable parameters include both ``weight`` and ``bias``.If ``no_bias`` is set to be true, then the ``bias`` term is ignored.Defined in I:\workspace\incubator-mxnet\src\operator\nn\fully_connected.cc:L98
/// </summary>
/// <param name="data">Input data.</param>
/// <param name="num_hidden">Number of hidden nodes of the output.</param>
/// <param name="weight">Weight matrix.</param>
/// <param name="bias">Bias parameter.</param>
/// <param name="no_bias">Whether to disable bias parameter.</param>
/// <param name="flatten">Whether to collapse all but the first axis of the input data tensor.</param>
 /// <returns>returns new symbol</returns>
public static NdArray FullyConnected(NdArray data,
int num_hidden,
NdArray weight=null,
NdArray bias=null,
bool no_bias=false,
bool flatten=true)
{
return new Operator("FullyConnected")
.SetParam("num_hidden", num_hidden)
.SetParam("no_bias", no_bias)
.SetParam("flatten", flatten)
.SetInput("data", data)
.SetInput("weight", weight)
.SetInput("bias", bias)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardFullyConnected(NdArray @out)
{
return new Operator("_backward_FullyConnected")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardFullyConnected()
{
return new Operator("_backward_FullyConnected")
.Invoke();
}
private static readonly List<string> PoolingPoolTypeConvert = new List<string>(){"avg","max","sum"};
private static readonly List<string> PoolingPoolingConventionConvert = new List<string>(){"full","valid"};
/// <summary>
/// Performs pooling on the input.The shapes for 1-D pooling are- **data**: *(batch_size, channel, width)*,- **out**: *(batch_size, num_filter, out_width)*.The shapes for 2-D pooling are- **data**: *(batch_size, channel, height, width)*- **out**: *(batch_size, num_filter, out_height, out_width)*, with::    out_height = f(height, kernel[0], pad[0], stride[0])    out_width = f(width, kernel[1], pad[1], stride[1])The definition of *f* depends on ``pooling_convention``, which has two options:- **valid** (default)::    f(x, k, p, s) = floor((x+2*p-k)/s)+1- **full**, which is compatible with Caffe::    f(x, k, p, s) = ceil((x+2*p-k)/s)+1But ``global_pool`` is set to be true, then do a global pooling, namely reset``kernel=(height, width)``.Three pooling options are supported by ``pool_type``:- **avg**: average pooling- **max**: max pooling- **sum**: sum poolingFor 3-D pooling, an additional *depth* dimension is added before*height*. Namely the input data will have shape *(batch_size, channel, depth,height, width)*.Defined in I:\workspace\incubator-mxnet\src\operator\nn\pooling.cc:L133
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the pooling operator.</param>
/// <param name="kernel">Pooling kernel size: (y, x) or (d, y, x)</param>
/// <param name="pool_type">Pooling type to be applied.</param>
/// <param name="global_pool">Ignore kernel size, do global pooling based on current input feature map. </param>
/// <param name="cudnn_off">Turn off cudnn pooling and use MXNet pooling operator. </param>
/// <param name="pooling_convention">Pooling convention to be applied.</param>
/// <param name="stride">Stride: for pooling (y, x) or (d, y, x). Defaults to 1 for each dimension.</param>
/// <param name="pad">Pad for pooling: (y, x) or (d, y, x). Defaults to no padding.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Pooling(NdArray @out,
NdArray data,
Shape kernel,
PoolingPoolType pool_type,
bool global_pool=false,
bool cudnn_off=false,
PoolingPoolingConvention pooling_convention=PoolingPoolingConvention.Valid,
Shape stride=null,
Shape pad=null)
{
return new Operator("Pooling")
.SetParam("global_pool", global_pool)
.SetParam("cudnn_off", cudnn_off)
.SetParam("kernel", kernel)
.SetParam("pool_type", Util.EnumToString<PoolingPoolType>(pool_type,PoolingPoolTypeConvert))
.SetParam("pooling_convention", Util.EnumToString<PoolingPoolingConvention>(pooling_convention,PoolingPoolingConventionConvert))
.SetParam("stride", stride)
.SetParam("pad", pad)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Performs pooling on the input.The shapes for 1-D pooling are- **data**: *(batch_size, channel, width)*,- **out**: *(batch_size, num_filter, out_width)*.The shapes for 2-D pooling are- **data**: *(batch_size, channel, height, width)*- **out**: *(batch_size, num_filter, out_height, out_width)*, with::    out_height = f(height, kernel[0], pad[0], stride[0])    out_width = f(width, kernel[1], pad[1], stride[1])The definition of *f* depends on ``pooling_convention``, which has two options:- **valid** (default)::    f(x, k, p, s) = floor((x+2*p-k)/s)+1- **full**, which is compatible with Caffe::    f(x, k, p, s) = ceil((x+2*p-k)/s)+1But ``global_pool`` is set to be true, then do a global pooling, namely reset``kernel=(height, width)``.Three pooling options are supported by ``pool_type``:- **avg**: average pooling- **max**: max pooling- **sum**: sum poolingFor 3-D pooling, an additional *depth* dimension is added before*height*. Namely the input data will have shape *(batch_size, channel, depth,height, width)*.Defined in I:\workspace\incubator-mxnet\src\operator\nn\pooling.cc:L133
/// </summary>
/// <param name="data">Input data to the pooling operator.</param>
/// <param name="kernel">Pooling kernel size: (y, x) or (d, y, x)</param>
/// <param name="pool_type">Pooling type to be applied.</param>
/// <param name="global_pool">Ignore kernel size, do global pooling based on current input feature map. </param>
/// <param name="cudnn_off">Turn off cudnn pooling and use MXNet pooling operator. </param>
/// <param name="pooling_convention">Pooling convention to be applied.</param>
/// <param name="stride">Stride: for pooling (y, x) or (d, y, x). Defaults to 1 for each dimension.</param>
/// <param name="pad">Pad for pooling: (y, x) or (d, y, x). Defaults to no padding.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Pooling(NdArray data,
Shape kernel,
PoolingPoolType pool_type,
bool global_pool=false,
bool cudnn_off=false,
PoolingPoolingConvention pooling_convention=PoolingPoolingConvention.Valid,
Shape stride=null,
Shape pad=null)
{
return new Operator("Pooling")
.SetParam("global_pool", global_pool)
.SetParam("cudnn_off", cudnn_off)
.SetParam("kernel", kernel)
.SetParam("pool_type", Util.EnumToString<PoolingPoolType>(pool_type,PoolingPoolTypeConvert))
.SetParam("pooling_convention", Util.EnumToString<PoolingPoolingConvention>(pooling_convention,PoolingPoolingConventionConvert))
.SetParam("stride", stride)
.SetParam("pad", pad)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPooling(NdArray @out)
{
return new Operator("_backward_Pooling")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPooling()
{
return new Operator("_backward_Pooling")
.Invoke();
}
private static readonly List<string> SoftmaxactivationModeConvert = new List<string>(){"channel","instance"};
/// <summary>
/// Applies softmax activation to input. This is intended for internal layers... note::  This operator has been deprecated, please use `softmax`.If `mode` = ``instance``, this operator will compute a softmax for each instance in the batch.This is the default mode.If `mode` = ``channel``, this operator will compute a k-class softmax at each positionof each instance, where `k` = ``num_channel``. This mode can only be used when the input arrayhas at least 3 dimensions.This can be used for `fully convolutional network`, `image segmentation`, etc.Example::  >>> input_array = mx.nd.array([[3., 0.5, -0.5, 2., 7.],  >>>                            [2., -.4, 7.,   3., 0.2]])  >>> softmax_act = mx.nd.SoftmaxActivation(input_array)  >>> print softmax_act.asnumpy()  [[  1.78322066e-02   1.46375655e-03   5.38485940e-04   6.56010211e-03   9.73605454e-01]   [  6.56221947e-03   5.95310994e-04   9.73919690e-01   1.78379621e-02   1.08472735e-03]]Defined in I:\workspace\incubator-mxnet\src\operator\nn\softmax_activation.cc:L67
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array to activation function.</param>
/// <param name="mode">Specifies how to compute the softmax. If set to ``instance``, it computes softmax for each instance. If set to ``channel``, It computes cross channel softmax for each position of each instance.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SoftmaxActivation(NdArray @out,
NdArray data,
SoftmaxactivationMode mode=SoftmaxactivationMode.Instance)
{
return new Operator("SoftmaxActivation")
.SetParam("mode", Util.EnumToString<SoftmaxactivationMode>(mode,SoftmaxactivationModeConvert))
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Applies softmax activation to input. This is intended for internal layers... note::  This operator has been deprecated, please use `softmax`.If `mode` = ``instance``, this operator will compute a softmax for each instance in the batch.This is the default mode.If `mode` = ``channel``, this operator will compute a k-class softmax at each positionof each instance, where `k` = ``num_channel``. This mode can only be used when the input arrayhas at least 3 dimensions.This can be used for `fully convolutional network`, `image segmentation`, etc.Example::  >>> input_array = mx.nd.array([[3., 0.5, -0.5, 2., 7.],  >>>                            [2., -.4, 7.,   3., 0.2]])  >>> softmax_act = mx.nd.SoftmaxActivation(input_array)  >>> print softmax_act.asnumpy()  [[  1.78322066e-02   1.46375655e-03   5.38485940e-04   6.56010211e-03   9.73605454e-01]   [  6.56221947e-03   5.95310994e-04   9.73919690e-01   1.78379621e-02   1.08472735e-03]]Defined in I:\workspace\incubator-mxnet\src\operator\nn\softmax_activation.cc:L67
/// </summary>
/// <param name="data">Input array to activation function.</param>
/// <param name="mode">Specifies how to compute the softmax. If set to ``instance``, it computes softmax for each instance. If set to ``channel``, It computes cross channel softmax for each position of each instance.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SoftmaxActivation(NdArray data,
SoftmaxactivationMode mode=SoftmaxactivationMode.Instance)
{
return new Operator("SoftmaxActivation")
.SetParam("mode", Util.EnumToString<SoftmaxactivationMode>(mode,SoftmaxactivationModeConvert))
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmaxActivation(NdArray @out)
{
return new Operator("_backward_SoftmaxActivation")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmaxActivation()
{
return new Operator("_backward_SoftmaxActivation")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardUpSampling(NdArray @out)
{
return new Operator("_backward_UpSampling")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardUpSampling()
{
return new Operator("_backward_UpSampling")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPad(NdArray @out)
{
return new Operator("_backward_Pad")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPad()
{
return new Operator("_backward_Pad")
.Invoke();
}
private static readonly List<string> PoolingV1PoolTypeConvert = new List<string>(){"avg","max","sum"};
private static readonly List<string> PoolingV1PoolingConventionConvert = new List<string>(){"full","valid"};
/// <summary>
/// This operator is DEPRECATED.Perform pooling on the input.The shapes for 2-D pooling is- **data**: *(batch_size, channel, height, width)*- **out**: *(batch_size, num_filter, out_height, out_width)*, with::    out_height = f(height, kernel[0], pad[0], stride[0])    out_width = f(width, kernel[1], pad[1], stride[1])The definition of *f* depends on ``pooling_convention``, which has two options:- **valid** (default)::    f(x, k, p, s) = floor((x+2*p-k)/s)+1- **full**, which is compatible with Caffe::    f(x, k, p, s) = ceil((x+2*p-k)/s)+1But ``global_pool`` is set to be true, then do a global pooling, namely reset``kernel=(height, width)``.Three pooling options are supported by ``pool_type``:- **avg**: average pooling- **max**: max pooling- **sum**: sum pooling1-D pooling is special case of 2-D pooling with *weight=1* and*kernel[1]=1*.For 3-D pooling, an additional *depth* dimension is added before*height*. Namely the input data will have shape *(batch_size, channel, depth,height, width)*.Defined in I:\workspace\incubator-mxnet\src\operator\pooling_v1.cc:L104
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the pooling operator.</param>
/// <param name="kernel">pooling kernel size: (y, x) or (d, y, x)</param>
/// <param name="pool_type">Pooling type to be applied.</param>
/// <param name="global_pool">Ignore kernel size, do global pooling based on current input feature map. </param>
/// <param name="pooling_convention">Pooling convention to be applied.</param>
/// <param name="stride">stride: for pooling (y, x) or (d, y, x)</param>
/// <param name="pad">pad for pooling: (y, x) or (d, y, x)</param>
 /// <returns>returns new symbol</returns>
public static NdArray PoolingV1(NdArray @out,
NdArray data,
Shape kernel,
PoolingV1PoolType pool_type,
bool global_pool=false,
PoolingV1PoolingConvention pooling_convention=PoolingV1PoolingConvention.Valid,
Shape stride=null,
Shape pad=null)
{
return new Operator("Pooling_v1")
.SetParam("global_pool", global_pool)
.SetParam("kernel", kernel)
.SetParam("pool_type", Util.EnumToString<PoolingV1PoolType>(pool_type,PoolingV1PoolTypeConvert))
.SetParam("pooling_convention", Util.EnumToString<PoolingV1PoolingConvention>(pooling_convention,PoolingV1PoolingConventionConvert))
.SetParam("stride", stride)
.SetParam("pad", pad)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// This operator is DEPRECATED.Perform pooling on the input.The shapes for 2-D pooling is- **data**: *(batch_size, channel, height, width)*- **out**: *(batch_size, num_filter, out_height, out_width)*, with::    out_height = f(height, kernel[0], pad[0], stride[0])    out_width = f(width, kernel[1], pad[1], stride[1])The definition of *f* depends on ``pooling_convention``, which has two options:- **valid** (default)::    f(x, k, p, s) = floor((x+2*p-k)/s)+1- **full**, which is compatible with Caffe::    f(x, k, p, s) = ceil((x+2*p-k)/s)+1But ``global_pool`` is set to be true, then do a global pooling, namely reset``kernel=(height, width)``.Three pooling options are supported by ``pool_type``:- **avg**: average pooling- **max**: max pooling- **sum**: sum pooling1-D pooling is special case of 2-D pooling with *weight=1* and*kernel[1]=1*.For 3-D pooling, an additional *depth* dimension is added before*height*. Namely the input data will have shape *(batch_size, channel, depth,height, width)*.Defined in I:\workspace\incubator-mxnet\src\operator\pooling_v1.cc:L104
/// </summary>
/// <param name="data">Input data to the pooling operator.</param>
/// <param name="kernel">pooling kernel size: (y, x) or (d, y, x)</param>
/// <param name="pool_type">Pooling type to be applied.</param>
/// <param name="global_pool">Ignore kernel size, do global pooling based on current input feature map. </param>
/// <param name="pooling_convention">Pooling convention to be applied.</param>
/// <param name="stride">stride: for pooling (y, x) or (d, y, x)</param>
/// <param name="pad">pad for pooling: (y, x) or (d, y, x)</param>
 /// <returns>returns new symbol</returns>
public static NdArray PoolingV1(NdArray data,
Shape kernel,
PoolingV1PoolType pool_type,
bool global_pool=false,
PoolingV1PoolingConvention pooling_convention=PoolingV1PoolingConvention.Valid,
Shape stride=null,
Shape pad=null)
{
return new Operator("Pooling_v1")
.SetParam("global_pool", global_pool)
.SetParam("kernel", kernel)
.SetParam("pool_type", Util.EnumToString<PoolingV1PoolType>(pool_type,PoolingV1PoolTypeConvert))
.SetParam("pooling_convention", Util.EnumToString<PoolingV1PoolingConvention>(pooling_convention,PoolingV1PoolingConventionConvert))
.SetParam("stride", stride)
.SetParam("pad", pad)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPoolingV1(NdArray @out)
{
return new Operator("_backward_Pooling_v1")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardPoolingV1()
{
return new Operator("_backward_Pooling_v1")
.Invoke();
}
/// <summary>
/// Computes and optimizes for squared loss during backward propagation.Just outputs ``data`` during forward propagation.If :math:`\hat{y}_i` is the predicted value of the i-th sample, and :math:`y_i` is the corresponding target value,then the squared loss estimated over :math:`n` samples is defined as:math:`\text{SquaredLoss}(y, \hat{y} ) = \frac{1}{n} \sum_{i=0}^{n-1} \left( y_i - \hat{y}_i \right)^2`.. note::   Use the LinearRegressionOutput as the final output layer of a net.By default, gradients of this loss function are scaled by factor `1/n`, where n is the number of training examples.The parameter `grad_scale` can be used to change this scale to `grad_scale/n`.Defined in I:\workspace\incubator-mxnet\src\operator\regression_output.cc:L70
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the function.</param>
/// <param name="label">Input label to the function.</param>
/// <param name="grad_scale">Scale the gradient by a float factor</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinearRegressionOutput(NdArray @out,
NdArray data,
NdArray label,
float grad_scale=1f)
{
return new Operator("LinearRegressionOutput")
.SetParam("grad_scale", grad_scale)
.SetInput("data", data)
.SetInput("label", label)
.Invoke(@out);
}
/// <summary>
/// Computes and optimizes for squared loss during backward propagation.Just outputs ``data`` during forward propagation.If :math:`\hat{y}_i` is the predicted value of the i-th sample, and :math:`y_i` is the corresponding target value,then the squared loss estimated over :math:`n` samples is defined as:math:`\text{SquaredLoss}(y, \hat{y} ) = \frac{1}{n} \sum_{i=0}^{n-1} \left( y_i - \hat{y}_i \right)^2`.. note::   Use the LinearRegressionOutput as the final output layer of a net.By default, gradients of this loss function are scaled by factor `1/n`, where n is the number of training examples.The parameter `grad_scale` can be used to change this scale to `grad_scale/n`.Defined in I:\workspace\incubator-mxnet\src\operator\regression_output.cc:L70
/// </summary>
/// <param name="data">Input data to the function.</param>
/// <param name="label">Input label to the function.</param>
/// <param name="grad_scale">Scale the gradient by a float factor</param>
 /// <returns>returns new symbol</returns>
public static NdArray LinearRegressionOutput(NdArray data,
NdArray label,
float grad_scale=1f)
{
return new Operator("LinearRegressionOutput")
.SetParam("grad_scale", grad_scale)
.SetInput("data", data)
.SetInput("label", label)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinearRegressionOutput(NdArray @out)
{
return new Operator("_backward_LinearRegressionOutput")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLinearRegressionOutput()
{
return new Operator("_backward_LinearRegressionOutput")
.Invoke();
}
/// <summary>
/// Computes mean absolute error of the input.MAE is a risk metric corresponding to the expected value of the absolute error.If :math:`\hat{y}_i` is the predicted value of the i-th sample, and :math:`y_i` is the corresponding target value,then the mean absolute error (MAE) estimated over :math:`n` samples is defined as:math:`\text{MAE}(y, \hat{y} ) = \frac{1}{n} \sum_{i=0}^{n-1} \left| y_i - \hat{y}_i \right|`.. note::   Use the MAERegressionOutput as the final output layer of a net.By default, gradients of this loss function are scaled by factor `1/n`, where n is the number of training examples.The parameter `grad_scale` can be used to change this scale to `grad_scale/n`.Defined in I:\workspace\incubator-mxnet\src\operator\regression_output.cc:L91
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the function.</param>
/// <param name="label">Input label to the function.</param>
/// <param name="grad_scale">Scale the gradient by a float factor</param>
 /// <returns>returns new symbol</returns>
public static NdArray MAERegressionOutput(NdArray @out,
NdArray data,
NdArray label,
float grad_scale=1f)
{
return new Operator("MAERegressionOutput")
.SetParam("grad_scale", grad_scale)
.SetInput("data", data)
.SetInput("label", label)
.Invoke(@out);
}
/// <summary>
/// Computes mean absolute error of the input.MAE is a risk metric corresponding to the expected value of the absolute error.If :math:`\hat{y}_i` is the predicted value of the i-th sample, and :math:`y_i` is the corresponding target value,then the mean absolute error (MAE) estimated over :math:`n` samples is defined as:math:`\text{MAE}(y, \hat{y} ) = \frac{1}{n} \sum_{i=0}^{n-1} \left| y_i - \hat{y}_i \right|`.. note::   Use the MAERegressionOutput as the final output layer of a net.By default, gradients of this loss function are scaled by factor `1/n`, where n is the number of training examples.The parameter `grad_scale` can be used to change this scale to `grad_scale/n`.Defined in I:\workspace\incubator-mxnet\src\operator\regression_output.cc:L91
/// </summary>
/// <param name="data">Input data to the function.</param>
/// <param name="label">Input label to the function.</param>
/// <param name="grad_scale">Scale the gradient by a float factor</param>
 /// <returns>returns new symbol</returns>
public static NdArray MAERegressionOutput(NdArray data,
NdArray label,
float grad_scale=1f)
{
return new Operator("MAERegressionOutput")
.SetParam("grad_scale", grad_scale)
.SetInput("data", data)
.SetInput("label", label)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMAERegressionOutput(NdArray @out)
{
return new Operator("_backward_MAERegressionOutput")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardMAERegressionOutput()
{
return new Operator("_backward_MAERegressionOutput")
.Invoke();
}
/// <summary>
/// Applies a logistic function to the input.The logistic function, also known as the sigmoid function, is computed as:math:`\frac{1}{1+exp(-x)}`.Commonly, the sigmoid is used to squash the real-valued output of a linear model:math:wTx+b into the [0,1] range so that it can be interpreted as a probability.It is suitable for binary classification or probability prediction tasks... note::   Use the LogisticRegressionOutput as the final output layer of a net.By default, gradients of this loss function are scaled by factor `1/n`, where n is the number of training examples.The parameter `grad_scale` can be used to change this scale to `grad_scale/n`.Defined in I:\workspace\incubator-mxnet\src\operator\regression_output.cc:L112
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the function.</param>
/// <param name="label">Input label to the function.</param>
/// <param name="grad_scale">Scale the gradient by a float factor</param>
 /// <returns>returns new symbol</returns>
public static NdArray LogisticRegressionOutput(NdArray @out,
NdArray data,
NdArray label,
float grad_scale=1f)
{
return new Operator("LogisticRegressionOutput")
.SetParam("grad_scale", grad_scale)
.SetInput("data", data)
.SetInput("label", label)
.Invoke(@out);
}
/// <summary>
/// Applies a logistic function to the input.The logistic function, also known as the sigmoid function, is computed as:math:`\frac{1}{1+exp(-x)}`.Commonly, the sigmoid is used to squash the real-valued output of a linear model:math:wTx+b into the [0,1] range so that it can be interpreted as a probability.It is suitable for binary classification or probability prediction tasks... note::   Use the LogisticRegressionOutput as the final output layer of a net.By default, gradients of this loss function are scaled by factor `1/n`, where n is the number of training examples.The parameter `grad_scale` can be used to change this scale to `grad_scale/n`.Defined in I:\workspace\incubator-mxnet\src\operator\regression_output.cc:L112
/// </summary>
/// <param name="data">Input data to the function.</param>
/// <param name="label">Input label to the function.</param>
/// <param name="grad_scale">Scale the gradient by a float factor</param>
 /// <returns>returns new symbol</returns>
public static NdArray LogisticRegressionOutput(NdArray data,
NdArray label,
float grad_scale=1f)
{
return new Operator("LogisticRegressionOutput")
.SetParam("grad_scale", grad_scale)
.SetInput("data", data)
.SetInput("label", label)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLogisticRegressionOutput(NdArray @out)
{
return new Operator("_backward_LogisticRegressionOutput")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardLogisticRegressionOutput()
{
return new Operator("_backward_LogisticRegressionOutput")
.Invoke();
}
private static readonly List<string> RNNModeConvert = new List<string>(){"gru","lstm","rnn_relu","rnn_tanh"};
/// <summary>
/// Applies a recurrent layer to input.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to RNN</param>
/// <param name="parameters">Vector of all RNN trainable parameters concatenated</param>
/// <param name="state">initial hidden state of the RNN</param>
/// <param name="state_cell">initial cell state for LSTM networks (only for LSTM)</param>
/// <param name="state_size">size of the state for each layer</param>
/// <param name="num_layers">number of stacked layers</param>
/// <param name="mode">the type of RNN to compute</param>
/// <param name="bidirectional">whether to use bidirectional recurrent layers</param>
/// <param name="p">Dropout probability, fraction of the input that gets dropped out at training time</param>
/// <param name="state_outputs">Whether to have the states as symbol outputs.</param>
 /// <returns>returns new symbol</returns>
public static NdArray RNN(NdArray @out,
NdArray data,
NdArray parameters,
NdArray state,
NdArray state_cell,
uint state_size,
uint num_layers,
RNNMode mode,
bool bidirectional=false,
float p=0f,
bool state_outputs=false)
{
return new Operator("RNN")
.SetParam("state_size", state_size)
.SetParam("num_layers", num_layers)
.SetParam("bidirectional", bidirectional)
.SetParam("mode", Util.EnumToString<RNNMode>(mode,RNNModeConvert))
.SetParam("p", p)
.SetParam("state_outputs", state_outputs)
.SetInput("data", data)
.SetInput("parameters", parameters)
.SetInput("state", state)
.SetInput("state_cell", state_cell)
.Invoke(@out);
}
/// <summary>
/// Applies a recurrent layer to input.
/// </summary>
/// <param name="data">Input data to RNN</param>
/// <param name="parameters">Vector of all RNN trainable parameters concatenated</param>
/// <param name="state">initial hidden state of the RNN</param>
/// <param name="state_cell">initial cell state for LSTM networks (only for LSTM)</param>
/// <param name="state_size">size of the state for each layer</param>
/// <param name="num_layers">number of stacked layers</param>
/// <param name="mode">the type of RNN to compute</param>
/// <param name="bidirectional">whether to use bidirectional recurrent layers</param>
/// <param name="p">Dropout probability, fraction of the input that gets dropped out at training time</param>
/// <param name="state_outputs">Whether to have the states as symbol outputs.</param>
 /// <returns>returns new symbol</returns>
public static NdArray RNN(NdArray data,
NdArray parameters,
NdArray state,
NdArray state_cell,
uint state_size,
uint num_layers,
RNNMode mode,
bool bidirectional=false,
float p=0f,
bool state_outputs=false)
{
return new Operator("RNN")
.SetParam("state_size", state_size)
.SetParam("num_layers", num_layers)
.SetParam("bidirectional", bidirectional)
.SetParam("mode", Util.EnumToString<RNNMode>(mode,RNNModeConvert))
.SetParam("p", p)
.SetParam("state_outputs", state_outputs)
.SetInput("data", data)
.SetInput("parameters", parameters)
.SetInput("state", state)
.SetInput("state_cell", state_cell)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRNN(NdArray @out)
{
return new Operator("_backward_RNN")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardRNN()
{
return new Operator("_backward_RNN")
.Invoke();
}
/// <summary>
/// Performs region of interest(ROI) pooling on the input array.ROI pooling is a variant of a max pooling layer, in which the output size is fixed andregion of interest is a parameter. Its purpose is to perform max pooling on the inputsof non-uniform sizes to obtain fixed-size feature maps. ROI pooling is a neural-netlayer mostly used in training a `Fast R-CNN` network for object detection.This operator takes a 4D feature map as an input array and region proposals as `rois`,then it pools over sub-regions of input and produces a fixed-sized output arrayregardless of the ROI size.To crop the feature map accordingly, you can resize the bounding box coordinatesby changing the parameters `rois` and `spatial_scale`.The cropped feature maps are pooled by standard max pooling operation to a fixed size outputindicated by a `pooled_size` parameter. batch_size will change to the number of regionbounding boxes after `ROIPooling`.The size of each region of interest doesn't have to be perfectly divisible bythe number of pooling sections(`pooled_size`).Example::  x = [[[[  0.,   1.,   2.,   3.,   4.,   5.],         [  6.,   7.,   8.,   9.,  10.,  11.],         [ 12.,  13.,  14.,  15.,  16.,  17.],         [ 18.,  19.,  20.,  21.,  22.,  23.],         [ 24.,  25.,  26.,  27.,  28.,  29.],         [ 30.,  31.,  32.,  33.,  34.,  35.],         [ 36.,  37.,  38.,  39.,  40.,  41.],         [ 42.,  43.,  44.,  45.,  46.,  47.]]]]  // region of interest i.e. bounding box coordinates.  y = [[0,0,0,4,4]]  // returns array of shape (2,2) according to the given roi with max pooling.  ROIPooling(x, y, (2,2), 1.0) = [[[[ 14.,  16.],                                    [ 26.,  28.]]]]  // region of interest is changed due to the change in `spacial_scale` parameter.  ROIPooling(x, y, (2,2), 0.7) = [[[[  7.,   9.],                                    [ 19.,  21.]]]]Defined in I:\workspace\incubator-mxnet\src\operator\roi_pooling.cc:L287
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">The input array to the pooling operator,  a 4D Feature maps </param>
/// <param name="rois">Bounding box coordinates, a 2D array of [[batch_index, x1, y1, x2, y2]], where (x1, y1) and (x2, y2) are top left and bottom right corners of designated region of interest. `batch_index` indicates the index of corresponding image in the input array</param>
/// <param name="pooled_size">ROI pooling output shape (h,w) </param>
/// <param name="spatial_scale">Ratio of input feature map height (or w) to raw image height (or w). Equals the reciprocal of total stride in convolutional layers</param>
 /// <returns>returns new symbol</returns>
public static NdArray ROIPooling(NdArray @out,
NdArray data,
NdArray rois,
Shape pooled_size,
float spatial_scale)
{
return new Operator("ROIPooling")
.SetParam("pooled_size", pooled_size)
.SetParam("spatial_scale", spatial_scale)
.SetInput("data", data)
.SetInput("rois", rois)
.Invoke(@out);
}
/// <summary>
/// Performs region of interest(ROI) pooling on the input array.ROI pooling is a variant of a max pooling layer, in which the output size is fixed andregion of interest is a parameter. Its purpose is to perform max pooling on the inputsof non-uniform sizes to obtain fixed-size feature maps. ROI pooling is a neural-netlayer mostly used in training a `Fast R-CNN` network for object detection.This operator takes a 4D feature map as an input array and region proposals as `rois`,then it pools over sub-regions of input and produces a fixed-sized output arrayregardless of the ROI size.To crop the feature map accordingly, you can resize the bounding box coordinatesby changing the parameters `rois` and `spatial_scale`.The cropped feature maps are pooled by standard max pooling operation to a fixed size outputindicated by a `pooled_size` parameter. batch_size will change to the number of regionbounding boxes after `ROIPooling`.The size of each region of interest doesn't have to be perfectly divisible bythe number of pooling sections(`pooled_size`).Example::  x = [[[[  0.,   1.,   2.,   3.,   4.,   5.],         [  6.,   7.,   8.,   9.,  10.,  11.],         [ 12.,  13.,  14.,  15.,  16.,  17.],         [ 18.,  19.,  20.,  21.,  22.,  23.],         [ 24.,  25.,  26.,  27.,  28.,  29.],         [ 30.,  31.,  32.,  33.,  34.,  35.],         [ 36.,  37.,  38.,  39.,  40.,  41.],         [ 42.,  43.,  44.,  45.,  46.,  47.]]]]  // region of interest i.e. bounding box coordinates.  y = [[0,0,0,4,4]]  // returns array of shape (2,2) according to the given roi with max pooling.  ROIPooling(x, y, (2,2), 1.0) = [[[[ 14.,  16.],                                    [ 26.,  28.]]]]  // region of interest is changed due to the change in `spacial_scale` parameter.  ROIPooling(x, y, (2,2), 0.7) = [[[[  7.,   9.],                                    [ 19.,  21.]]]]Defined in I:\workspace\incubator-mxnet\src\operator\roi_pooling.cc:L287
/// </summary>
/// <param name="data">The input array to the pooling operator,  a 4D Feature maps </param>
/// <param name="rois">Bounding box coordinates, a 2D array of [[batch_index, x1, y1, x2, y2]], where (x1, y1) and (x2, y2) are top left and bottom right corners of designated region of interest. `batch_index` indicates the index of corresponding image in the input array</param>
/// <param name="pooled_size">ROI pooling output shape (h,w) </param>
/// <param name="spatial_scale">Ratio of input feature map height (or w) to raw image height (or w). Equals the reciprocal of total stride in convolutional layers</param>
 /// <returns>returns new symbol</returns>
public static NdArray ROIPooling(NdArray data,
NdArray rois,
Shape pooled_size,
float spatial_scale)
{
return new Operator("ROIPooling")
.SetParam("pooled_size", pooled_size)
.SetParam("spatial_scale", spatial_scale)
.SetInput("data", data)
.SetInput("rois", rois)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardROIPooling(NdArray @out)
{
return new Operator("_backward_ROIPooling")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardROIPooling()
{
return new Operator("_backward_ROIPooling")
.Invoke();
}
/// <summary>
/// Takes the last element of a sequence.This function takes an n-dimensional input array of the form[max_sequence_length, batch_size, other_feature_dims] and returns a (n-1)-dimensional arrayof the form [batch_size, other_feature_dims].Parameter `sequence_length` is used to handle variable-length sequences. `sequence_length` should bean input array of positive ints of dimension [batch_size]. To use this parameter,set `use_sequence_length` to `True`, otherwise each example in the batch is assumedto have the max sequence length... note:: Alternatively, you can also use `take` operator.Example::   x = [[[  1.,   2.,   3.],         [  4.,   5.,   6.],         [  7.,   8.,   9.]],        [[ 10.,   11.,   12.],         [ 13.,   14.,   15.],         [ 16.,   17.,   18.]],        [[  19.,   20.,   21.],         [  22.,   23.,   24.],         [  25.,   26.,   27.]]]   // returns last sequence when sequence_length parameter is not used   SequenceLast(x) = [[  19.,   20.,   21.],                      [  22.,   23.,   24.],                      [  25.,   26.,   27.]]   // sequence_length y is used   SequenceLast(x, y=[1,1,1], use_sequence_length=True) =            [[  1.,   2.,   3.],             [  4.,   5.,   6.],             [  7.,   8.,   9.]]   // sequence_length y is used   SequenceLast(x, y=[1,2,3], use_sequence_length=True) =            [[  1.,    2.,   3.],             [  13.,  14.,  15.],             [  25.,  26.,  27.]]Defined in I:\workspace\incubator-mxnet\src\operator\sequence_last.cc:L92
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">n-dimensional input array of the form [max_sequence_length, batch_size, other_feature_dims] where n>2</param>
/// <param name="sequence_length">vector of sequence lengths of the form [batch_size]</param>
/// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
 /// <returns>returns new symbol</returns>
public static NdArray SequenceLast(NdArray @out,
NdArray data,
NdArray sequence_length,
bool use_sequence_length=false)
{
return new Operator("SequenceLast")
.SetParam("use_sequence_length", use_sequence_length)
.SetInput("data", data)
.SetInput("sequence_length", sequence_length)
.Invoke(@out);
}
/// <summary>
/// Takes the last element of a sequence.This function takes an n-dimensional input array of the form[max_sequence_length, batch_size, other_feature_dims] and returns a (n-1)-dimensional arrayof the form [batch_size, other_feature_dims].Parameter `sequence_length` is used to handle variable-length sequences. `sequence_length` should bean input array of positive ints of dimension [batch_size]. To use this parameter,set `use_sequence_length` to `True`, otherwise each example in the batch is assumedto have the max sequence length... note:: Alternatively, you can also use `take` operator.Example::   x = [[[  1.,   2.,   3.],         [  4.,   5.,   6.],         [  7.,   8.,   9.]],        [[ 10.,   11.,   12.],         [ 13.,   14.,   15.],         [ 16.,   17.,   18.]],        [[  19.,   20.,   21.],         [  22.,   23.,   24.],         [  25.,   26.,   27.]]]   // returns last sequence when sequence_length parameter is not used   SequenceLast(x) = [[  19.,   20.,   21.],                      [  22.,   23.,   24.],                      [  25.,   26.,   27.]]   // sequence_length y is used   SequenceLast(x, y=[1,1,1], use_sequence_length=True) =            [[  1.,   2.,   3.],             [  4.,   5.,   6.],             [  7.,   8.,   9.]]   // sequence_length y is used   SequenceLast(x, y=[1,2,3], use_sequence_length=True) =            [[  1.,    2.,   3.],             [  13.,  14.,  15.],             [  25.,  26.,  27.]]Defined in I:\workspace\incubator-mxnet\src\operator\sequence_last.cc:L92
/// </summary>
/// <param name="data">n-dimensional input array of the form [max_sequence_length, batch_size, other_feature_dims] where n>2</param>
/// <param name="sequence_length">vector of sequence lengths of the form [batch_size]</param>
/// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
 /// <returns>returns new symbol</returns>
public static NdArray SequenceLast(NdArray data,
NdArray sequence_length,
bool use_sequence_length=false)
{
return new Operator("SequenceLast")
.SetParam("use_sequence_length", use_sequence_length)
.SetInput("data", data)
.SetInput("sequence_length", sequence_length)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSequenceLast(NdArray @out)
{
return new Operator("_backward_SequenceLast")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSequenceLast()
{
return new Operator("_backward_SequenceLast")
.Invoke();
}
/// <summary>
/// Sets all elements outside the sequence to a constant value.This function takes an n-dimensional input array of the form[max_sequence_length, batch_size, other_feature_dims] and returns an array of the same shape.Parameter `sequence_length` is used to handle variable-length sequences. `sequence_length`should be an input array of positive ints of dimension [batch_size].To use this parameter, set `use_sequence_length` to `True`,otherwise each example in the batch is assumed to have the max sequence length andthis operator works as the `identity` operator.Example::   x = [[[  1.,   2.,   3.],         [  4.,   5.,   6.]],        [[  7.,   8.,   9.],         [ 10.,  11.,  12.]],        [[ 13.,  14.,   15.],         [ 16.,  17.,   18.]]]   // Batch 1   B1 = [[  1.,   2.,   3.],         [  7.,   8.,   9.],         [ 13.,  14.,  15.]]   // Batch 2   B2 = [[  4.,   5.,   6.],         [ 10.,  11.,  12.],         [ 16.,  17.,  18.]]   // works as identity operator when sequence_length parameter is not used   SequenceMask(x) = [[[  1.,   2.,   3.],                       [  4.,   5.,   6.]],                      [[  7.,   8.,   9.],                       [ 10.,  11.,  12.]],                      [[ 13.,  14.,   15.],                       [ 16.,  17.,   18.]]]   // sequence_length [1,1] means 1 of each batch will be kept   // and other rows are masked with default mask value = 0   SequenceMask(x, y=[1,1], use_sequence_length=True) =                [[[  1.,   2.,   3.],                  [  4.,   5.,   6.]],                 [[  0.,   0.,   0.],                  [  0.,   0.,   0.]],                 [[  0.,   0.,   0.],                  [  0.,   0.,   0.]]]   // sequence_length [2,3] means 2 of batch B1 and 3 of batch B2 will be kept   // and other rows are masked with value = 1   SequenceMask(x, y=[2,3], use_sequence_length=True, value=1) =                [[[  1.,   2.,   3.],                  [  4.,   5.,   6.]],                 [[  7.,   8.,   9.],                  [  10.,  11.,  12.]],                 [[   1.,   1.,   1.],                  [  16.,  17.,  18.]]]Defined in I:\workspace\incubator-mxnet\src\operator\sequence_mask.cc:L114
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">n-dimensional input array of the form [max_sequence_length, batch_size, other_feature_dims] where n>2</param>
/// <param name="sequence_length">vector of sequence lengths of the form [batch_size]</param>
/// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
/// <param name="value">The value to be used as a mask.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SequenceMask(NdArray @out,
NdArray data,
NdArray sequence_length,
bool use_sequence_length=false,
float value=0f)
{
return new Operator("SequenceMask")
.SetParam("use_sequence_length", use_sequence_length)
.SetParam("value", value)
.SetInput("data", data)
.SetInput("sequence_length", sequence_length)
.Invoke(@out);
}
/// <summary>
/// Sets all elements outside the sequence to a constant value.This function takes an n-dimensional input array of the form[max_sequence_length, batch_size, other_feature_dims] and returns an array of the same shape.Parameter `sequence_length` is used to handle variable-length sequences. `sequence_length`should be an input array of positive ints of dimension [batch_size].To use this parameter, set `use_sequence_length` to `True`,otherwise each example in the batch is assumed to have the max sequence length andthis operator works as the `identity` operator.Example::   x = [[[  1.,   2.,   3.],         [  4.,   5.,   6.]],        [[  7.,   8.,   9.],         [ 10.,  11.,  12.]],        [[ 13.,  14.,   15.],         [ 16.,  17.,   18.]]]   // Batch 1   B1 = [[  1.,   2.,   3.],         [  7.,   8.,   9.],         [ 13.,  14.,  15.]]   // Batch 2   B2 = [[  4.,   5.,   6.],         [ 10.,  11.,  12.],         [ 16.,  17.,  18.]]   // works as identity operator when sequence_length parameter is not used   SequenceMask(x) = [[[  1.,   2.,   3.],                       [  4.,   5.,   6.]],                      [[  7.,   8.,   9.],                       [ 10.,  11.,  12.]],                      [[ 13.,  14.,   15.],                       [ 16.,  17.,   18.]]]   // sequence_length [1,1] means 1 of each batch will be kept   // and other rows are masked with default mask value = 0   SequenceMask(x, y=[1,1], use_sequence_length=True) =                [[[  1.,   2.,   3.],                  [  4.,   5.,   6.]],                 [[  0.,   0.,   0.],                  [  0.,   0.,   0.]],                 [[  0.,   0.,   0.],                  [  0.,   0.,   0.]]]   // sequence_length [2,3] means 2 of batch B1 and 3 of batch B2 will be kept   // and other rows are masked with value = 1   SequenceMask(x, y=[2,3], use_sequence_length=True, value=1) =                [[[  1.,   2.,   3.],                  [  4.,   5.,   6.]],                 [[  7.,   8.,   9.],                  [  10.,  11.,  12.]],                 [[   1.,   1.,   1.],                  [  16.,  17.,  18.]]]Defined in I:\workspace\incubator-mxnet\src\operator\sequence_mask.cc:L114
/// </summary>
/// <param name="data">n-dimensional input array of the form [max_sequence_length, batch_size, other_feature_dims] where n>2</param>
/// <param name="sequence_length">vector of sequence lengths of the form [batch_size]</param>
/// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
/// <param name="value">The value to be used as a mask.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SequenceMask(NdArray data,
NdArray sequence_length,
bool use_sequence_length=false,
float value=0f)
{
return new Operator("SequenceMask")
.SetParam("use_sequence_length", use_sequence_length)
.SetParam("value", value)
.SetInput("data", data)
.SetInput("sequence_length", sequence_length)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSequenceMask(NdArray @out)
{
return new Operator("_backward_SequenceMask")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSequenceMask()
{
return new Operator("_backward_SequenceMask")
.Invoke();
}
/// <summary>
/// Reverses the elements of each sequence.This function takes an n-dimensional input array of the form [max_sequence_length, batch_size, other_feature_dims]and returns an array of the same shape.Parameter `sequence_length` is used to handle variable-length sequences.`sequence_length` should be an input array of positive ints of dimension [batch_size].To use this parameter, set `use_sequence_length` to `True`,otherwise each example in the batch is assumed to have the max sequence length.Example::   x = [[[  1.,   2.,   3.],         [  4.,   5.,   6.]],        [[  7.,   8.,   9.],         [ 10.,  11.,  12.]],        [[ 13.,  14.,   15.],         [ 16.,  17.,   18.]]]   // Batch 1   B1 = [[  1.,   2.,   3.],         [  7.,   8.,   9.],         [ 13.,  14.,  15.]]   // Batch 2   B2 = [[  4.,   5.,   6.],         [ 10.,  11.,  12.],         [ 16.,  17.,  18.]]   // returns reverse sequence when sequence_length parameter is not used   SequenceReverse(x) = [[[ 13.,  14.,   15.],                          [ 16.,  17.,   18.]],                         [[  7.,   8.,   9.],                          [ 10.,  11.,  12.]],                         [[  1.,   2.,   3.],                          [  4.,   5.,   6.]]]   // sequence_length [2,2] means 2 rows of   // both batch B1 and B2 will be reversed.   SequenceReverse(x, y=[2,2], use_sequence_length=True) =                     [[[  7.,   8.,   9.],                       [ 10.,  11.,  12.]],                      [[  1.,   2.,   3.],                       [  4.,   5.,   6.]],                      [[ 13.,  14.,   15.],                       [ 16.,  17.,   18.]]]   // sequence_length [2,3] means 2 of batch B2 and 3 of batch B3   // will be reversed.   SequenceReverse(x, y=[2,3], use_sequence_length=True) =                    [[[  7.,   8.,   9.],                      [ 16.,  17.,  18.]],                     [[  1.,   2.,   3.],                      [ 10.,  11.,  12.]],                     [[ 13.,  14,   15.],                      [  4.,   5.,   6.]]]Defined in I:\workspace\incubator-mxnet\src\operator\sequence_reverse.cc:L113
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">n-dimensional input array of the form [max_sequence_length, batch_size, other dims] where n>2 </param>
/// <param name="sequence_length">vector of sequence lengths of the form [batch_size]</param>
/// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
 /// <returns>returns new symbol</returns>
public static NdArray SequenceReverse(NdArray @out,
NdArray data,
NdArray sequence_length,
bool use_sequence_length=false)
{
return new Operator("SequenceReverse")
.SetParam("use_sequence_length", use_sequence_length)
.SetInput("data", data)
.SetInput("sequence_length", sequence_length)
.Invoke(@out);
}
/// <summary>
/// Reverses the elements of each sequence.This function takes an n-dimensional input array of the form [max_sequence_length, batch_size, other_feature_dims]and returns an array of the same shape.Parameter `sequence_length` is used to handle variable-length sequences.`sequence_length` should be an input array of positive ints of dimension [batch_size].To use this parameter, set `use_sequence_length` to `True`,otherwise each example in the batch is assumed to have the max sequence length.Example::   x = [[[  1.,   2.,   3.],         [  4.,   5.,   6.]],        [[  7.,   8.,   9.],         [ 10.,  11.,  12.]],        [[ 13.,  14.,   15.],         [ 16.,  17.,   18.]]]   // Batch 1   B1 = [[  1.,   2.,   3.],         [  7.,   8.,   9.],         [ 13.,  14.,  15.]]   // Batch 2   B2 = [[  4.,   5.,   6.],         [ 10.,  11.,  12.],         [ 16.,  17.,  18.]]   // returns reverse sequence when sequence_length parameter is not used   SequenceReverse(x) = [[[ 13.,  14.,   15.],                          [ 16.,  17.,   18.]],                         [[  7.,   8.,   9.],                          [ 10.,  11.,  12.]],                         [[  1.,   2.,   3.],                          [  4.,   5.,   6.]]]   // sequence_length [2,2] means 2 rows of   // both batch B1 and B2 will be reversed.   SequenceReverse(x, y=[2,2], use_sequence_length=True) =                     [[[  7.,   8.,   9.],                       [ 10.,  11.,  12.]],                      [[  1.,   2.,   3.],                       [  4.,   5.,   6.]],                      [[ 13.,  14.,   15.],                       [ 16.,  17.,   18.]]]   // sequence_length [2,3] means 2 of batch B2 and 3 of batch B3   // will be reversed.   SequenceReverse(x, y=[2,3], use_sequence_length=True) =                    [[[  7.,   8.,   9.],                      [ 16.,  17.,  18.]],                     [[  1.,   2.,   3.],                      [ 10.,  11.,  12.]],                     [[ 13.,  14,   15.],                      [  4.,   5.,   6.]]]Defined in I:\workspace\incubator-mxnet\src\operator\sequence_reverse.cc:L113
/// </summary>
/// <param name="data">n-dimensional input array of the form [max_sequence_length, batch_size, other dims] where n>2 </param>
/// <param name="sequence_length">vector of sequence lengths of the form [batch_size]</param>
/// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
 /// <returns>returns new symbol</returns>
public static NdArray SequenceReverse(NdArray data,
NdArray sequence_length,
bool use_sequence_length=false)
{
return new Operator("SequenceReverse")
.SetParam("use_sequence_length", use_sequence_length)
.SetInput("data", data)
.SetInput("sequence_length", sequence_length)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSequenceReverse(NdArray @out)
{
return new Operator("_backward_SequenceReverse")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSequenceReverse()
{
return new Operator("_backward_SequenceReverse")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSliceChannel(NdArray @out)
{
return new Operator("_backward_SliceChannel")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSliceChannel()
{
return new Operator("_backward_SliceChannel")
.Invoke();
}
private static readonly List<string> SoftmaxoutputNormalizationConvert = new List<string>(){"batch","null","valid"};
/// <summary>
/// Computes the gradient of cross entropy loss with respect to softmax output.- This operator computes the gradient in two steps.  The cross entropy loss does not actually need to be computed.  - Applies softmax function on the input array.  - Computes and returns the gradient of cross entropy loss w.r.t. the softmax output.- The softmax function, cross entropy loss and gradient is given by:  - Softmax Function:    .. math:: \text{softmax}(x)_i = \frac{exp(x_i)}{\sum_j exp(x_j)}  - Cross Entropy Function:    .. math:: \text{CE(label, output)} = - \sum_i \text{label}_i \log(\text{output}_i)  - The gradient of cross entropy loss w.r.t softmax output:    .. math:: \text{gradient} = \text{output} - \text{label}- During forward propagation, the softmax function is computed for each instance in the input array.  For general *N*-D input arrays with shape :math:`(d_1, d_2, ..., d_n)`. The size is  :math:`s=d_1 \cdot d_2 \cdot \cdot \cdot d_n`. We can use the parameters `preserve_shape`  and `multi_output` to specify the way to compute softmax:  - By default, `preserve_shape` is ``false``. This operator will reshape the input array    into a 2-D array with shape :math:`(d_1, \frac{s}{d_1})` and then compute the softmax function for    each row in the reshaped array, and afterwards reshape it back to the original shape    :math:`(d_1, d_2, ..., d_n)`.  - If `preserve_shape` is ``true``, the softmax function will be computed along    the last axis (`axis` = ``-1``).  - If `multi_output` is ``true``, the softmax function will be computed along    the second axis (`axis` = ``1``).- During backward propagation, the gradient of cross-entropy loss w.r.t softmax output array is computed.  The provided label can be a one-hot label array or a probability label array.  - If the parameter `use_ignore` is ``true``, `ignore_label` can specify input instances    with a particular label to be ignored during backward propagation. **This has no effect when    softmax `output` has same shape as `label`**.    Example::      data = [[1,2,3,4],[2,2,2,2],[3,3,3,3],[4,4,4,4]]      label = [1,0,2,3]      ignore_label = 1      SoftmaxOutput(data=data, label = label,\                    multi_output=true, use_ignore=true,\                    ignore_label=ignore_label)      ## forward softmax output      [[ 0.0320586   0.08714432  0.23688284  0.64391428]       [ 0.25        0.25        0.25        0.25      ]       [ 0.25        0.25        0.25        0.25      ]       [ 0.25        0.25        0.25        0.25      ]]      ## backward gradient output      [[ 0.    0.    0.    0.  ]       [-0.75  0.25  0.25  0.25]       [ 0.25  0.25 -0.75  0.25]       [ 0.25  0.25  0.25 -0.75]]      ## notice that the first row is all 0 because label[0] is 1, which is equal to ignore_label.  - The parameter `grad_scale` can be used to rescale the gradient, which is often used to    give each loss function different weights.  - This operator also supports various ways to normalize the gradient by `normalization`,    The `normalization` is applied if softmax output has different shape than the labels.    The `normalization` mode can be set to the followings:    - ``'null'``: do nothing.    - ``'batch'``: divide the gradient by the batch size.    - ``'valid'``: divide the gradient by the number of instances which are not ignored.Defined in I:\workspace\incubator-mxnet\src\operator\softmax_output.cc:L123
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array.</param>
/// <param name="label">Ground truth label.</param>
/// <param name="grad_scale">Scales the gradient by a float factor.</param>
/// <param name="ignore_label">The instances whose `labels` == `ignore_label` will be ignored during backward, if `use_ignore` is set to ``true``).</param>
/// <param name="multi_output">If set to ``true``, the softmax function will be computed along axis ``1``. This is applied when the shape of input array differs from the shape of label array.</param>
/// <param name="use_ignore">If set to ``true``, the `ignore_label` value will not contribute to the backward gradient.</param>
/// <param name="preserve_shape">If set to ``true``, the softmax function will be computed along the last axis (``-1``).</param>
/// <param name="normalization">Normalizes the gradient.</param>
/// <param name="out_grad">Multiplies gradient with output gradient element-wise.</param>
/// <param name="smooth_alpha">Constant for computing a label smoothed version of cross-entropyfor the backwards pass.  This constant gets subtracted from theone-hot encoding of the gold label and distributed uniformly toall other labels.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SoftmaxOutput(NdArray @out,
NdArray data,
NdArray label,
float grad_scale=1f,
float ignore_label=-1f,
bool multi_output=false,
bool use_ignore=false,
bool preserve_shape=false,
SoftmaxoutputNormalization normalization=SoftmaxoutputNormalization.Null,
bool out_grad=false,
float smooth_alpha=0f)
{
return new Operator("SoftmaxOutput")
.SetParam("grad_scale", grad_scale)
.SetParam("ignore_label", ignore_label)
.SetParam("multi_output", multi_output)
.SetParam("use_ignore", use_ignore)
.SetParam("preserve_shape", preserve_shape)
.SetParam("normalization", Util.EnumToString<SoftmaxoutputNormalization>(normalization,SoftmaxoutputNormalizationConvert))
.SetParam("out_grad", out_grad)
.SetParam("smooth_alpha", smooth_alpha)
.SetInput("data", data)
.SetInput("label", label)
.Invoke(@out);
}
/// <summary>
/// Computes the gradient of cross entropy loss with respect to softmax output.- This operator computes the gradient in two steps.  The cross entropy loss does not actually need to be computed.  - Applies softmax function on the input array.  - Computes and returns the gradient of cross entropy loss w.r.t. the softmax output.- The softmax function, cross entropy loss and gradient is given by:  - Softmax Function:    .. math:: \text{softmax}(x)_i = \frac{exp(x_i)}{\sum_j exp(x_j)}  - Cross Entropy Function:    .. math:: \text{CE(label, output)} = - \sum_i \text{label}_i \log(\text{output}_i)  - The gradient of cross entropy loss w.r.t softmax output:    .. math:: \text{gradient} = \text{output} - \text{label}- During forward propagation, the softmax function is computed for each instance in the input array.  For general *N*-D input arrays with shape :math:`(d_1, d_2, ..., d_n)`. The size is  :math:`s=d_1 \cdot d_2 \cdot \cdot \cdot d_n`. We can use the parameters `preserve_shape`  and `multi_output` to specify the way to compute softmax:  - By default, `preserve_shape` is ``false``. This operator will reshape the input array    into a 2-D array with shape :math:`(d_1, \frac{s}{d_1})` and then compute the softmax function for    each row in the reshaped array, and afterwards reshape it back to the original shape    :math:`(d_1, d_2, ..., d_n)`.  - If `preserve_shape` is ``true``, the softmax function will be computed along    the last axis (`axis` = ``-1``).  - If `multi_output` is ``true``, the softmax function will be computed along    the second axis (`axis` = ``1``).- During backward propagation, the gradient of cross-entropy loss w.r.t softmax output array is computed.  The provided label can be a one-hot label array or a probability label array.  - If the parameter `use_ignore` is ``true``, `ignore_label` can specify input instances    with a particular label to be ignored during backward propagation. **This has no effect when    softmax `output` has same shape as `label`**.    Example::      data = [[1,2,3,4],[2,2,2,2],[3,3,3,3],[4,4,4,4]]      label = [1,0,2,3]      ignore_label = 1      SoftmaxOutput(data=data, label = label,\                    multi_output=true, use_ignore=true,\                    ignore_label=ignore_label)      ## forward softmax output      [[ 0.0320586   0.08714432  0.23688284  0.64391428]       [ 0.25        0.25        0.25        0.25      ]       [ 0.25        0.25        0.25        0.25      ]       [ 0.25        0.25        0.25        0.25      ]]      ## backward gradient output      [[ 0.    0.    0.    0.  ]       [-0.75  0.25  0.25  0.25]       [ 0.25  0.25 -0.75  0.25]       [ 0.25  0.25  0.25 -0.75]]      ## notice that the first row is all 0 because label[0] is 1, which is equal to ignore_label.  - The parameter `grad_scale` can be used to rescale the gradient, which is often used to    give each loss function different weights.  - This operator also supports various ways to normalize the gradient by `normalization`,    The `normalization` is applied if softmax output has different shape than the labels.    The `normalization` mode can be set to the followings:    - ``'null'``: do nothing.    - ``'batch'``: divide the gradient by the batch size.    - ``'valid'``: divide the gradient by the number of instances which are not ignored.Defined in I:\workspace\incubator-mxnet\src\operator\softmax_output.cc:L123
/// </summary>
/// <param name="data">Input array.</param>
/// <param name="label">Ground truth label.</param>
/// <param name="grad_scale">Scales the gradient by a float factor.</param>
/// <param name="ignore_label">The instances whose `labels` == `ignore_label` will be ignored during backward, if `use_ignore` is set to ``true``).</param>
/// <param name="multi_output">If set to ``true``, the softmax function will be computed along axis ``1``. This is applied when the shape of input array differs from the shape of label array.</param>
/// <param name="use_ignore">If set to ``true``, the `ignore_label` value will not contribute to the backward gradient.</param>
/// <param name="preserve_shape">If set to ``true``, the softmax function will be computed along the last axis (``-1``).</param>
/// <param name="normalization">Normalizes the gradient.</param>
/// <param name="out_grad">Multiplies gradient with output gradient element-wise.</param>
/// <param name="smooth_alpha">Constant for computing a label smoothed version of cross-entropyfor the backwards pass.  This constant gets subtracted from theone-hot encoding of the gold label and distributed uniformly toall other labels.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SoftmaxOutput(NdArray data,
NdArray label,
float grad_scale=1f,
float ignore_label=-1f,
bool multi_output=false,
bool use_ignore=false,
bool preserve_shape=false,
SoftmaxoutputNormalization normalization=SoftmaxoutputNormalization.Null,
bool out_grad=false,
float smooth_alpha=0f)
{
return new Operator("SoftmaxOutput")
.SetParam("grad_scale", grad_scale)
.SetParam("ignore_label", ignore_label)
.SetParam("multi_output", multi_output)
.SetParam("use_ignore", use_ignore)
.SetParam("preserve_shape", preserve_shape)
.SetParam("normalization", Util.EnumToString<SoftmaxoutputNormalization>(normalization,SoftmaxoutputNormalizationConvert))
.SetParam("out_grad", out_grad)
.SetParam("smooth_alpha", smooth_alpha)
.SetInput("data", data)
.SetInput("label", label)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmaxOutput(NdArray @out)
{
return new Operator("_backward_SoftmaxOutput")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmaxOutput()
{
return new Operator("_backward_SoftmaxOutput")
.Invoke();
}
private static readonly List<string> SoftmaxNormalizationConvert = new List<string>(){"batch","null","valid"};
/// <summary>
/// Please use `SoftmaxOutput`... note::  This operator has been renamed to `SoftmaxOutput`, which  computes the gradient of cross-entropy loss w.r.t softmax output.  To just compute softmax output, use the `softmax` operator.Defined in I:\workspace\incubator-mxnet\src\operator\softmax_output.cc:L138
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input array.</param>
/// <param name="grad_scale">Scales the gradient by a float factor.</param>
/// <param name="ignore_label">The instances whose `labels` == `ignore_label` will be ignored during backward, if `use_ignore` is set to ``true``).</param>
/// <param name="multi_output">If set to ``true``, the softmax function will be computed along axis ``1``. This is applied when the shape of input array differs from the shape of label array.</param>
/// <param name="use_ignore">If set to ``true``, the `ignore_label` value will not contribute to the backward gradient.</param>
/// <param name="preserve_shape">If set to ``true``, the softmax function will be computed along the last axis (``-1``).</param>
/// <param name="normalization">Normalizes the gradient.</param>
/// <param name="out_grad">Multiplies gradient with output gradient element-wise.</param>
/// <param name="smooth_alpha">Constant for computing a label smoothed version of cross-entropyfor the backwards pass.  This constant gets subtracted from theone-hot encoding of the gold label and distributed uniformly toall other labels.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Softmax(NdArray @out,
NdArray data,
float grad_scale=1f,
float ignore_label=-1f,
bool multi_output=false,
bool use_ignore=false,
bool preserve_shape=false,
SoftmaxNormalization normalization=SoftmaxNormalization.Null,
bool out_grad=false,
float smooth_alpha=0f)
{
return new Operator("Softmax")
.SetParam("grad_scale", grad_scale)
.SetParam("ignore_label", ignore_label)
.SetParam("multi_output", multi_output)
.SetParam("use_ignore", use_ignore)
.SetParam("preserve_shape", preserve_shape)
.SetParam("normalization", Util.EnumToString<SoftmaxNormalization>(normalization,SoftmaxNormalizationConvert))
.SetParam("out_grad", out_grad)
.SetParam("smooth_alpha", smooth_alpha)
.SetInput("data", data)
.Invoke(@out);
}
/// <summary>
/// Please use `SoftmaxOutput`... note::  This operator has been renamed to `SoftmaxOutput`, which  computes the gradient of cross-entropy loss w.r.t softmax output.  To just compute softmax output, use the `softmax` operator.Defined in I:\workspace\incubator-mxnet\src\operator\softmax_output.cc:L138
/// </summary>
/// <param name="data">Input array.</param>
/// <param name="grad_scale">Scales the gradient by a float factor.</param>
/// <param name="ignore_label">The instances whose `labels` == `ignore_label` will be ignored during backward, if `use_ignore` is set to ``true``).</param>
/// <param name="multi_output">If set to ``true``, the softmax function will be computed along axis ``1``. This is applied when the shape of input array differs from the shape of label array.</param>
/// <param name="use_ignore">If set to ``true``, the `ignore_label` value will not contribute to the backward gradient.</param>
/// <param name="preserve_shape">If set to ``true``, the softmax function will be computed along the last axis (``-1``).</param>
/// <param name="normalization">Normalizes the gradient.</param>
/// <param name="out_grad">Multiplies gradient with output gradient element-wise.</param>
/// <param name="smooth_alpha">Constant for computing a label smoothed version of cross-entropyfor the backwards pass.  This constant gets subtracted from theone-hot encoding of the gold label and distributed uniformly toall other labels.</param>
 /// <returns>returns new symbol</returns>
public static NdArray Softmax(NdArray data,
float grad_scale=1f,
float ignore_label=-1f,
bool multi_output=false,
bool use_ignore=false,
bool preserve_shape=false,
SoftmaxNormalization normalization=SoftmaxNormalization.Null,
bool out_grad=false,
float smooth_alpha=0f)
{
return new Operator("Softmax")
.SetParam("grad_scale", grad_scale)
.SetParam("ignore_label", ignore_label)
.SetParam("multi_output", multi_output)
.SetParam("use_ignore", use_ignore)
.SetParam("preserve_shape", preserve_shape)
.SetParam("normalization", Util.EnumToString<SoftmaxNormalization>(normalization,SoftmaxNormalizationConvert))
.SetParam("out_grad", out_grad)
.SetParam("smooth_alpha", smooth_alpha)
.SetInput("data", data)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmax(NdArray @out)
{
return new Operator("_backward_Softmax")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSoftmax()
{
return new Operator("_backward_Softmax")
.Invoke();
}
private static readonly List<string> SpatialtransformerTransformTypeConvert = new List<string>(){"affine"};
private static readonly List<string> SpatialtransformerSamplerTypeConvert = new List<string>(){"bilinear"};
/// <summary>
/// Applies a spatial transformer to input feature map.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data to the SpatialTransformerOp.</param>
/// <param name="loc">localisation net, the output dim should be 6 when transform_type is affine. You shold initialize the weight and bias with identity tranform.</param>
/// <param name="transform_type">transformation type</param>
/// <param name="sampler_type">sampling type</param>
/// <param name="target_shape">output shape(h, w) of spatial transformer: (y, x)</param>
 /// <returns>returns new symbol</returns>
public static NdArray SpatialTransformer(NdArray @out,
NdArray data,
NdArray loc,
SpatialtransformerTransformType transform_type,
SpatialtransformerSamplerType sampler_type,
Shape target_shape=null)
{
return new Operator("SpatialTransformer")
.SetParam("target_shape", target_shape)
.SetParam("transform_type", Util.EnumToString<SpatialtransformerTransformType>(transform_type,SpatialtransformerTransformTypeConvert))
.SetParam("sampler_type", Util.EnumToString<SpatialtransformerSamplerType>(sampler_type,SpatialtransformerSamplerTypeConvert))
.SetInput("data", data)
.SetInput("loc", loc)
.Invoke(@out);
}
/// <summary>
/// Applies a spatial transformer to input feature map.
/// </summary>
/// <param name="data">Input data to the SpatialTransformerOp.</param>
/// <param name="loc">localisation net, the output dim should be 6 when transform_type is affine. You shold initialize the weight and bias with identity tranform.</param>
/// <param name="transform_type">transformation type</param>
/// <param name="sampler_type">sampling type</param>
/// <param name="target_shape">output shape(h, w) of spatial transformer: (y, x)</param>
 /// <returns>returns new symbol</returns>
public static NdArray SpatialTransformer(NdArray data,
NdArray loc,
SpatialtransformerTransformType transform_type,
SpatialtransformerSamplerType sampler_type,
Shape target_shape=null)
{
return new Operator("SpatialTransformer")
.SetParam("target_shape", target_shape)
.SetParam("transform_type", Util.EnumToString<SpatialtransformerTransformType>(transform_type,SpatialtransformerTransformTypeConvert))
.SetParam("sampler_type", Util.EnumToString<SpatialtransformerSamplerType>(sampler_type,SpatialtransformerSamplerTypeConvert))
.SetInput("data", data)
.SetInput("loc", loc)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSpatialTransformer(NdArray @out)
{
return new Operator("_backward_SpatialTransformer")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSpatialTransformer()
{
return new Operator("_backward_SpatialTransformer")
.Invoke();
}
/// <summary>
/// Computes support vector machine based transformation of the input.This tutorial demonstrates using SVM as output layer for classification instead of softmax:https://github.com/dmlc/mxnet/tree/master/example/svm_mnist.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="data">Input data for SVM transformation.</param>
/// <param name="label">Class label for the input data.</param>
/// <param name="margin">The loss function penalizes outputs that lie outside this margin. Default margin is 1.</param>
/// <param name="regularization_coefficient">Regularization parameter for the SVM. This balances the tradeoff between coefficient size and error.</param>
/// <param name="use_linear">Whether to use L1-SVM objective. L2-SVM objective is used by default.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SVMOutput(NdArray @out,
NdArray data,
NdArray label,
float margin=1f,
float regularization_coefficient=1f,
bool use_linear=false)
{
return new Operator("SVMOutput")
.SetParam("margin", margin)
.SetParam("regularization_coefficient", regularization_coefficient)
.SetParam("use_linear", use_linear)
.SetInput("data", data)
.SetInput("label", label)
.Invoke(@out);
}
/// <summary>
/// Computes support vector machine based transformation of the input.This tutorial demonstrates using SVM as output layer for classification instead of softmax:https://github.com/dmlc/mxnet/tree/master/example/svm_mnist.
/// </summary>
/// <param name="data">Input data for SVM transformation.</param>
/// <param name="label">Class label for the input data.</param>
/// <param name="margin">The loss function penalizes outputs that lie outside this margin. Default margin is 1.</param>
/// <param name="regularization_coefficient">Regularization parameter for the SVM. This balances the tradeoff between coefficient size and error.</param>
/// <param name="use_linear">Whether to use L1-SVM objective. L2-SVM objective is used by default.</param>
 /// <returns>returns new symbol</returns>
public static NdArray SVMOutput(NdArray data,
NdArray label,
float margin=1f,
float regularization_coefficient=1f,
bool use_linear=false)
{
return new Operator("SVMOutput")
.SetParam("margin", margin)
.SetParam("regularization_coefficient", regularization_coefficient)
.SetParam("use_linear", use_linear)
.SetInput("data", data)
.SetInput("label", label)
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSVMOutput(NdArray @out)
{
return new Operator("_backward_SVMOutput")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSVMOutput()
{
return new Operator("_backward_SVMOutput")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSwapAxis(NdArray @out)
{
return new Operator("_backward_SwapAxis")
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
 /// <returns>returns new symbol</returns>
public static NdArray BackwardSwapAxis()
{
return new Operator("_backward_SwapAxis")
.Invoke();
}
/// <summary>
/// 
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">Left operand to the function.</param>
/// <param name="rhs">Right operand to the function.</param>
 /// <returns>returns new symbol</returns>
public static NdArray OnehotEncode(NdArray @out,
Symbol lhs,
Symbol rhs)
{
return new Operator("_onehot_encode")
.SetParam("lhs", lhs)
.SetParam("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// 
/// </summary>
/// <param name="lhs">Left operand to the function.</param>
/// <param name="rhs">Right operand to the function.</param>
 /// <returns>returns new symbol</returns>
public static NdArray OnehotEncode(Symbol lhs,
Symbol rhs)
{
return new Operator("_onehot_encode")
.SetParam("lhs", lhs)
.SetParam("rhs", rhs)
.Invoke();
}
/// <summary>
/// Choose one element from each line(row for python, column for R/Julia) in lhs according to index indicated by rhs. This function assume rhs uses 0-based index.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">Left operand to the function.</param>
/// <param name="rhs">Right operand to the function.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ChooseElement0Index(NdArray @out,
Symbol lhs,
Symbol rhs)
{
return new Operator("choose_element_0index")
.SetParam("lhs", lhs)
.SetParam("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Choose one element from each line(row for python, column for R/Julia) in lhs according to index indicated by rhs. This function assume rhs uses 0-based index.
/// </summary>
/// <param name="lhs">Left operand to the function.</param>
/// <param name="rhs">Right operand to the function.</param>
 /// <returns>returns new symbol</returns>
public static NdArray ChooseElement0Index(Symbol lhs,
Symbol rhs)
{
return new Operator("choose_element_0index")
.SetParam("lhs", lhs)
.SetParam("rhs", rhs)
.Invoke();
}
/// <summary>
/// Fill one element of each line(row for python, column for R/Julia) in lhs according to index indicated by rhs and values indicated by mhs. This function assume rhs uses 0-based index.
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="lhs">Left operand to the function.</param>
/// <param name="mhs">Middle operand to the function.</param>
/// <param name="rhs">Right operand to the function.</param>
 /// <returns>returns new symbol</returns>
public static NdArray FillElement0Index(NdArray @out,
Symbol lhs,
Symbol mhs,
Symbol rhs)
{
return new Operator("fill_element_0index")
.SetParam("lhs", lhs)
.SetParam("mhs", mhs)
.SetParam("rhs", rhs)
.Invoke(@out);
}
/// <summary>
/// Fill one element of each line(row for python, column for R/Julia) in lhs according to index indicated by rhs and values indicated by mhs. This function assume rhs uses 0-based index.
/// </summary>
/// <param name="lhs">Left operand to the function.</param>
/// <param name="mhs">Middle operand to the function.</param>
/// <param name="rhs">Right operand to the function.</param>
 /// <returns>returns new symbol</returns>
public static NdArray FillElement0Index(Symbol lhs,
Symbol mhs,
Symbol rhs)
{
return new Operator("fill_element_0index")
.SetParam("lhs", lhs)
.SetParam("mhs", mhs)
.SetParam("rhs", rhs)
.Invoke();
}
/// <summary>
/// Decode an image, clip to (x0, y0, x1, y1), subtract mean, and write to buffer
/// </summary>
/// <param name="@out">output Ndarray</param>
/// <param name="mean">image mean</param>
/// <param name="index">buffer position for output</param>
/// <param name="x0">x0</param>
/// <param name="y0">y0</param>
/// <param name="x1">x1</param>
/// <param name="y1">y1</param>
/// <param name="c">channel</param>
/// <param name="size">length of str_img</param>
 /// <returns>returns new symbol</returns>
public static NdArray Imdecode(NdArray @out,
NdArray mean,
int index,
int x0,
int y0,
int x1,
int y1,
int c,
int size)
{
return new Operator("_imdecode")
.SetParam("index", index)
.SetParam("x0", x0)
.SetParam("y0", y0)
.SetParam("x1", x1)
.SetParam("y1", y1)
.SetParam("c", c)
.SetParam("size", size)
.SetInput("mean", mean)
.Invoke(@out);
}
/// <summary>
/// Decode an image, clip to (x0, y0, x1, y1), subtract mean, and write to buffer
/// </summary>
/// <param name="mean">image mean</param>
/// <param name="index">buffer position for output</param>
/// <param name="x0">x0</param>
/// <param name="y0">y0</param>
/// <param name="x1">x1</param>
/// <param name="y1">y1</param>
/// <param name="c">channel</param>
/// <param name="size">length of str_img</param>
 /// <returns>returns new symbol</returns>
public static NdArray Imdecode(NdArray mean,
int index,
int x0,
int y0,
int x1,
int y1,
int c,
int size)
{
return new Operator("_imdecode")
.SetParam("index", index)
.SetParam("x0", x0)
.SetParam("y0", y0)
.SetParam("x1", x1)
.SetParam("y1", y1)
.SetParam("c", c)
.SetParam("size", size)
.SetInput("mean", mean)
.Invoke();
}
}
}
