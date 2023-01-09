namespace Lou.Services {
    public class BugReport {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Labels { get; set; }
        
        public bool Confidential { get; set; }

        public bool IsValid() {
            return (!string.IsNullOrEmpty(Title)
                    && !string.IsNullOrEmpty(Description));
        }

        public override string ToString() {
            return $"[Title={Title}, Description={Description}, Labels={Labels}, Confidential={Confidential}]";
        }
    }
}