// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    /// <summary>
    ///     The resource bindings for a <see cref="Pipeline" /> to use for upcoming draw commands. Can include one or
    ///     many (up to 8) vertex <see cref="Buffer" /> resources, zero or one vertex-index <see cref="Buffer" />
    ///     resource, and zero to many (up to 12) <see cref="Image" /> resources to use in a <see cref="Shader" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         To apply a <see cref="ResourceBindings" />, call
    ///         <see cref="Pass.Apply(ref ResourceBindings)" /> after a <see cref="Pipeline" /> is created.
    ///     </para>
    ///     <para>
    ///         Use standard struct allocation and initialization techniques to create
    ///         a <see cref="ResourceBindings" />.
    ///     </para>
    ///     <para>
    ///         <see cref="ResourceBindings" /> is blittable to the C `sg_bindings` struct found in `sokol_gfx`.
    ///     </para>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 176, Pack = 4)]
    public unsafe struct ResourceBindings
    {
        /// <summary>
        ///     The vertex-index <see cref="Buffer" /> to use. Default is <c>default(Buffer)</c> which indicates to not
        ///     use indexing.
        /// </summary>
        [FieldOffset(68)]
        public Buffer IndexBuffer;

        /// <summary>
        ///     The zero-based offset into the <see cref="IndexBuffer" /> which to start using vertex-index data.
        ///     Default is <c>0</c>.
        /// </summary>
        [FieldOffset(72)]
        public int IndexBufferOffset;

        [FieldOffset(76)]
        internal fixed uint _vsImages[sokol_gfx.SG_MAX_SHADERSTAGE_IMAGES];

        [FieldOffset(124)]
        internal fixed uint _fsImages[sokol_gfx.SG_MAX_SHADERSTAGE_IMAGES];

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(0)]
        internal uint _startCanary;

        /// <summary>
        ///     A guard against garbage data; used to know if the structure has been initialized correctly.
        /// </summary>
        [FieldOffset(172)]
        internal uint _endCanary;

        [FieldOffset(4)]
        internal fixed uint _vertexBuffers[sokol_gfx.SG_MAX_SHADERSTAGE_BUFFERS];

        [FieldOffset(36)]
        internal fixed int _vertexBufferOffsets[sokol_gfx.SG_MAX_SHADERSTAGE_BUFFERS];

        /// <summary>
        ///     Gets the vertex <see cref="Buffer" />, by reference, to use given a specified slot or index.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index indicating what slot to use to get the vertex <see cref="Buffer" />. Can't be
        ///     negative. Must be less than or equal to <c>8</c>. Default is <c>0</c>.
        /// </param>
        /// <returns>A vertex <see cref="Buffer" /> by reference.</returns>
        public ref Buffer VertexBuffer(int index = 0)
        {
            fixed (ResourceBindings* bindings = &this)
            {
                var ptr = (Buffer*)&bindings->_vertexBuffers[0];
                return ref *(ptr + index);
            }
        }

        /// <summary>
        ///     Gets the zero-based offset, by reference, into the <see cref="VertexBuffer" /> (specified by a slot or
        ///     index) which to start using vertex data.
        /// </summary>
        /// <param name="index">The zero-based index indicating what vertex <see cref="Buffer" /> slot the offset applies to.</param>
        /// <returns>A zero-based offset.</returns>
        public ref int VertexBufferOffset(int index)
        {
            return ref _vertexBufferOffsets[index];
        }

        /// <summary>
        ///     Gets the <see cref="Image" /> to use, by reference, for the "per-vertex processing" stage of a
        ///     <see cref="Shader" />.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index indicating what slot to use to get the <see cref="Image" />.
        /// </param>
        /// <returns>A <see cref="Image" />.</returns>
        public ref Image VertexStageImage(int index)
        {
            fixed (ResourceBindings* bindings = &this)
            {
                var ptr = (Image*)&bindings->_vsImages[0];
                return ref *(ptr + index);
            }
        }

        /// <summary>
        ///     Gets the <see cref="Image" /> to use, by reference, for the "per-fragment processing" stage of a
        ///     <see cref="Shader" />.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index indicating what slot to use to get the <see cref="Image" />.
        /// </param>
        /// <returns>A <see cref="Image" />.</returns>
        public ref Image FragmentStageImage(int index)
        {
            fixed (ResourceBindings* bindings = &this)
            {
                var ptr = (Image*)&bindings->_fsImages[0];
                return ref *(ptr + index);
            }
        }
    }
}