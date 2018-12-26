﻿using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    /// <summary>
    /// Controller class for self posts.
    /// </summary>
    public class SelfPost : Post
    {
        public string SelfText;
        public string SelfTextHTML;

        /// <summary>
        /// Create new SelfPost instance from Reddit API listing.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="listing">Listing returned by Reddit API.</param>
        public SelfPost(ref Dispatch dispatch, RedditThings.Post listing) : base(ref dispatch, listing)
        {
            SelfText = listing.SelfText;
            SelfTextHTML = listing.SelfTextHTML;
        }

        /// <summary>
        /// Create a new SelfPost instance and populate manually.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="subreddit">The subreddit the post belongs to.</param>
        /// <param name="title">Post title.</param>
        /// <param name="author">Reddit user who authored the post.</param>
        /// <param name="selfText">The post body.</param>
        /// <param name="selfTextHtml">The HTML-formateed post body.</param>
        /// <param name="id">Post ID.</param>
        /// <param name="fullname">Post fullname.</param>
        /// <param name="permalink">Permalink of post.</param>
        /// <param name="created">When the post was created.</param>
        /// <param name="edited">When the post was last edited.</param>
        /// <param name="score">Net vote score.</param>
        /// <param name="upVotes">Number of upvotes.</param>
        /// <param name="downVotes">Number of downvotes.</param>
        /// <param name="removed">Whether the post was removed.</param>
        /// <param name="spam">Whether the post was marked as spam.</param>
        public SelfPost(ref Dispatch dispatch, string subreddit, string title, string author, string selfText, string selfTextHtml,
            string id = null, string fullname = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
            : base(ref dispatch, subreddit, title, author, id, fullname, permalink, created, edited, score, upVotes, downVotes,
                  removed, spam)
        {
            SelfText = selfText;
            SelfTextHTML = selfTextHtml;

            Listing = new RedditThings.Post(this);
        }

        /// <summary>
        /// Create a new SelfPost instance populated only with its Fullname.
        /// Useful for About() queries (e.g. new SelfPost("t3_whatever").About() will retrieve a new SelfPost by its fullname).
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="fullname">fullname of a thing</param>
        public SelfPost(ref Dispatch dispatch, string fullname) : base(ref dispatch, fullname) { }

        /// <summary>
        /// Create a new SelfPost instance populated with its Fullname and Subreddit.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="fullname">fullname of a thing</param>
        /// <param name="subreddit">The subreddit where the post exists</param>
        public SelfPost(ref Dispatch dispatch, string fullname, string subreddit) : base(ref dispatch, fullname, subreddit) { }

        /// <summary>
        /// Create a new SelfPost instance populated with its Subreddit, an ID/Fullname returned by the API, and other specified values.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        /// <param name="postResultShortData">Data returned by the Reddit API when creating a new post</param>
        /// <param name="selfPost">The SelfPost instance that executed the submission</param>
        public SelfPost(ref Dispatch dispatch, RedditThings.PostResultShortData postResultShortData, SelfPost selfPost)
            : base(ref dispatch, selfPost.Subreddit, selfPost.Title, selfPost.Author, postResultShortData.Id, postResultShortData.Name,
                  selfPost.Permalink, selfPost.Created, selfPost.Edited, selfPost.Score, selfPost.UpVotes, selfPost.DownVotes,
                  selfPost.Removed, selfPost.Spam, selfPost.NSFW)
        {
            SelfText = selfPost.SelfText;
            SelfTextHTML = selfPost.SelfTextHTML;

            Listing = new RedditThings.Post(this);
        }

        /// <summary>
        /// Create an empty SelfPost instance.
        /// </summary>
        /// <param name="dispatch">An instance of the Dispatch controller</param>
        public SelfPost(ref Dispatch dispatch) : base(ref dispatch) { }

        /// <summary>
        /// Submit this self post to Reddit.
        /// </summary>
        /// <param name="ad">boolean value</param>
        /// <param name="app"></param>
        /// <param name="extension">extension used for redirects</param>
        /// <param name="flairId">a string no longer than 36 characters</param>
        /// <param name="flairText">a string no longer than 64 characters</param>
        /// <param name="gRecapthaResponse"></param>
        /// <param name="sendReplies">boolean value</param>
        /// <param name="spoiler">boolean value</param>
        /// <param name="videoPosterUrl">a valid URL</param>
        /// <returns>A copy of this instance populated with the ID and Fullname returned by the API.</returns>
        public SelfPost Submit( bool ad = false, string app = "", string extension = "",
            string flairId = "", string flairText = "", string gRecapthaResponse = "", bool sendReplies = true, bool spoiler = false,
            string videoPosterUrl = "")
        {
            return new SelfPost(ref Dispatch, Validate(Dispatch.LinksAndComments.Submit(ad, app, extension, flairId, flairText,
                gRecapthaResponse, "self", NSFW, false, null, sendReplies, spoiler, Subreddit, SelfText, Title, null, videoPosterUrl)).JSON.Data, this);
        }

        /// <summary>
        /// Submit this self post to Reddit asynchronously.  This instance will automatically be updated with the resulting fullname/id.
        /// </summary>
        /// <param name="ad">boolean value</param>
        /// <param name="app"></param>
        /// <param name="extension">extension used for redirects</param>
        /// <param name="flairId">a string no longer than 36 characters</param>
        /// <param name="flairText">a string no longer than 64 characters</param>
        /// <param name="gRecapthaResponse"></param>
        /// <param name="sendReplies">boolean value</param>
        /// <param name="spoiler">boolean value</param>
        /// <param name="videoPosterUrl">a valid URL</param>
        public async Task SubmitAsync(bool ad = false, string app = "", string extension = "",
            string flairId = "", string flairText = "", string gRecapthaResponse = "", bool sendReplies = true, bool spoiler = false,
            string videoPosterUrl = "")
        {
            await Task.Run(() =>
            {
                Submit(ad, app, extension, flairId, flairText, gRecapthaResponse, sendReplies, spoiler, videoPosterUrl);
            });
        }

        /// <summary>
        /// Edit the body text of this self post.  This instance will be automatically updated with the return data.
        /// </summary>
        /// <param name="text">raw markdown text</param>
        /// <returns>This instance populated with the modified post data returned by the API.</returns>
        public SelfPost Edit(string text)
        {
            Import(Validate(Dispatch.LinksAndComments.EditUserText(false, null, text, Fullname)).JSON.Data.Things[0].Data);

            return this;
        }

        /// <summary>
        /// Edit the body text of this self post asynchronously.  This instance will be automatically updated with the return data.
        /// </summary>
        /// <param name="text">raw markdown text</param>
        public async Task EditAsync(string text)
        {
            await Task.Run(() =>
            {
                Edit(text);
            });
        }

        /// <summary>
        /// Return information about the current SelfPost instance.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public new SelfPost About()
        {
            RedditThings.Info info = Validate(Dispatch.LinksAndComments.Info(Fullname, Subreddit));
            if (info == null
                || info.Posts == null
                || info.Posts.Count == 0
                || !Fullname.Equals(info.Posts[0].Name))
            {
                throw new RedditControllerException("Unable to retrieve post data.");
            }

            return new SelfPost(ref Dispatch, info.Posts[0]);
        }
    }
}