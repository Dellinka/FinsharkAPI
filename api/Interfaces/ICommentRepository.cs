using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        public Task<Comment?> GetByIdAsync(int id);
        public Task<Comment?> CreateAsync(Comment commentModel);
        public Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto);
        public Task<Comment?> DeleteAsync(int id);
    }
}