namespace Implementation
{
    public static class Constantes
    {
        public const string LOG_FILE_PATH = "application.log";
        public const int WIDTH_IMAGE = 1000;
        public const int HEIGHT_IMAGE = 1000;
        public const int SQUARE_SIZE = 5;   //5px per 5px
        
        // Error messages for parameter validation
        public const string ERROR_DATA_REQUIRED = "The data array cannot be empty.";
        public const string ERROR_IMAGE_REQUIRED = "The image cannot be null.";
        public const string ERROR_SIZE_SQUARE = "The image dimensions must be a multiple of the square size.";
        public const string ERROR_INVALID_SQUARE_SIZE = "The square size must be greater than zero.";
        public const string ERROR_INVALID_PATH = "The file path is invalid.";
    }
}
