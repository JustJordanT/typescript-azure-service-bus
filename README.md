# .NET AWS SNS/SQS Example

```mermaid
graph TD;
    A[.NET Service A] -->|Publish Message| B[AWS SNS Topic]
    B -->|Subscribe| D[AWS SQS Queue 1]
    B -->|Subscribe| E[AWS SQS Queue 2]
    D -->|Consume| G[.NET Service B]
    E -->|Consume| H[.NET Service C]
```

## Overview

This project demonstrates a minimal setup to use AWS SNS and SQS with .NET. It includes examples for publishing messages to an SNS topic, subscribing an SQS queue to that topic, and consuming messages from the queue.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Configuration](#configuration)
4. [Usage](#usage)
5. [Contributing](#contributing)
6. [License](#license)

## Prerequisites

- .NET Core 3.1 or higher
- AWS CLI configured with appropriate permissions
- AWS SDK for .NET (`AWSSDK.Core`, `AWSSDK.SNS`, `AWSSDK.SQS`)

## Installation

1. Clone the repository:

    ```bash
    git clone git@github.com:JustJordanT/communication-dotnet-aws-sns-sqs.git
    ```

2. Navigate to the project directory:

    ```bash
    cd dotnet-aws-sns-sqs-example
    ```

3. Restore the project:

    ```bash
    dotnet restore
    ```

## Configuration

1. Rename the `appsettings.json.example` file to `appsettings.json` and fill in the required AWS credentials and configurations.

    ```json
    {
      "AWS": {
        "Region": "your-aws-region",
        "AccessKeyId": "your-access-key-id",
        "SecretAccessKey": "your-secret-access-key"
      },
      "SNS": {
        "TopicARN": "your-sns-topic-arn"
      },
      "SQS": {
        "QueueURL": "your-sqs-queue-url"
      }
    }
    ```

## Usage

### Publish to SNS Topic

Run the following command to publish a message to the SNS topic:

```bash
dotnet run --project ./src/Publish
```

### Consume from SQS Queue

Run the following command to start consuming messages from the SQS queue:

```bash
dotnet run --project ./src/Consume
```

## Contributing

1. Fork the repository.
2. Create a new feature branch.
3. Make your changes.
4. Submit a pull request.

For more details, see [CONTRIBUTING.md](CONTRIBUTING.md).

## License

This project is licensed under the MIT License. See [LICENSE.md](LICENSE.md) for details.

---

Feel free to adapt this README to better suit your project's specific needs.
