# SimpleStorageService
Client
   |
   |---[Upload File]---> StorageController
   |                        |
   |                        |---[HandleUploadAsync]---> StorageHandler
   |                                                         |
   |                                                         |---[UploadFileAsync]---> IStorage
   |                                                                                      |
   |                                                                                      |--> [S3 | Database | LocalFS | FTP]
   |
   |---[Download File]---> StorageController
                            |
                            |---[DownloadFileAsync]---> IStorage
                                                        |
                                                        |--> [S3 | Database | LocalFS | FTP]
                                                        
