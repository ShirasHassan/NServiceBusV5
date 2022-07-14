---
title: Generating secure random strong encryption keys
summary: Options for generating secure random strong encryption keys
tags:
 - Encryption
 - Security
reviewed: 2018-10-18
related:
 - nservicebus/security
 - nservicebus/security/property-encryption
---

There are multiple ways of generating an encryption key. Most implementations rely on a *random* object. All examples mentioned here use a secure cryptographic randomizer.


### PowerShell


#### Base64

snippet: Base64-Powershell


#### Hex

snippet: Hex-Powershell


### C&#35;

The code snippets below can be run from [LINQPad](https://www.linqpad.net/) or by copying the following code into a new project and referencing `System.Security`.


#### Base64

snippet: Base64-CSharp


#### Hex

snippet: Hex-CSharp


### OpenSSL

[OpenSSL](https://www.openssl.org/) is well known for its ability to generate certificates but it can also be used to generate random data.


#### Base64

Generates 32 random bytes (256bits) in a base64 encoded output:

```dos
openssl rand -base64 32
```


#### Plaintext

Generates 32 random characters (256bits):

```dos
openssl rand 32
```

NOTE: Be aware that strings parsed by NServiceBus do not use extended ASCII which limits the key range to 7 bits per character.


### CryptoKeyGenerator

A key generator exists in ParticularLabs that uses the .NET framework crypto provider to generate a key.

Download the [CryptoKeyGenerator](https://github.com/ParticularLabs/CryptoKeyGenerator) labs project, run it, and copy the random key in its correct format.


The tool generates one key and outputs this key in multiple formats.

Output

```
Strip 8th bit: True
Strip control: True
Key bit length: 256

Base64:
        KzpSTk1pezg5eTJRNmhWJmoxdFo6UDk2WlhaOyQ5N0U=
        |---------||---------||---------||---------|

Hex:
        2b3a524e4d697b3839793251366856266a31745a3a5039365a585a3b24393745
        |--------------||--------------||--------------||--------------|

ASCII:
        +:RNMi{89y2Q6hV&j1tZ:P96ZXZ;$97E     xml escape: +:RNMi{89y2Q6hV&amp;j1tZ:P96ZXZ;$97E
        |------||------||------||------|

ASCII-EX:
        +:RNMi{89y2Q6hV&j1tZ:P96ZXZ;$97E     xml escape: +:RNMi{89y2Q6hV&amp;j1tZ:P96ZXZ;$97E
        |------||------||------||------|
```