# Connection Class

**Pass in connection string**

`var cnn = new SqlConnection(cnnString);`

**Open connection**

`cnn.Open();`

**Close when done**

`cnn.Close();`

**Dispose of unmanaged resources**

`cnn.Dispose();`

# Command Class

**Submit query to database**

```csharp
var sql = "INSERT INTO .......";
var cmd = new SqlCommand(sql,cnn);
```

**Call appropriate method**

```csharp
cmd.ExecuteNonQuery();
cmd.ExecuteScalar();
```

# Parameter Class

**Pass argument(s) to SQL statement**

```csharp
var sql = "... VALUES(@ProductName ..."
cmd.Parameters.Add(new SqlParameter("@ProductName","New Product"));
```

**Supports OUPUT Parameters**

```csharp
cmd.Parameters.Add(new
 SqlParameter{
     ParameterName = "@ProductID",
     Value = 1,
     DbType = DbType.Int32,
     Direction = ParameterDerection.Output
 });
```

# Transactions

- More than one statement to submit to database
- All statements must succeed (all or nothing)
- if error it rolls back (Commit or Rollback)

# DataReader

- Forward-only cursor
- Fastest method of reading data
- Be sure to close (use using block)

# GetfieldValue

- `GetFieldValue<>()` (Automatic conversion)
- still need to call GerOrdinal
- does not handle nullable fields

# Multiple Result Sets
- Pass in two or more SELECT statements
- Return two or more SELECT statemetns from stored procedure
- Use `NextResult()` method
- Avoid mulitple calls to database