namespace Cave.Data.SQLite
{
    /// <summary>
    /// Provides access to a subversion entry.
    /// </summary>
    public class SubversionEntry
    {
        #region Private Fields

        readonly string[] Data;

        #endregion Private Fields

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SubversionEntry"/> class.
        /// </summary>
        /// <param name="data">Lines.</param>
        /// <param name="version">Version code.</param>
        internal SubversionEntry(string[] data, int version)
        {
            Version = version;
            this.Data = data;
            IsValid = (Version >= 8) || (Version <= 10);
        }

        #endregion Internal Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether the entry was deleted or not.
        /// </summary>
        public bool Deleted
        {
            get
            {
                return Version switch
                {
                    8 or 9 or 10 => Data[5] == "delete",
                    _ => false,
                };
            }
        }

        /// <summary>
        /// Gets a value indicating whether the entry is valid or not.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Gets the name of the <see cref="SubversionEntry"/>.
        /// </summary>
        public string Name => Data[0];

        /// <summary>
        /// Gets the type of the <see cref="SubversionEntry"/>.
        /// </summary>
        public SubversionEntryType Type
        {
            get
            {
                return (Data[1]) switch
                {
                    "dir" => SubversionEntryType.Directory,
                    "file" => SubversionEntryType.File,
                    _ => SubversionEntryType.Unknown,
                };
            }
        }

        /// <summary>
        /// Gets the subversion entry version.
        /// </summary>
        public int Version { get; }

        #endregion Public Properties

        #region Public Methods

        /// <inheritdoc/>
        public override int GetHashCode() => ToString().GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Name + " " + Type;

        #endregion Public Methods
    }
}
