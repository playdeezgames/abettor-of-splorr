Public Interface ICommandHandler(Of TCommand)
    Sub HandleCommand(command As TCommand)
End Interface
